using MuseScoreParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MuseScoreParser
{
    internal class NoteParser
    {
        internal static List<List<IAsmSymbol>> GetNotes(XDocument xml, string shortLabel, out List<RepeatLocations> repeatLabels)
        {
            var asmSymbols = new List<List<IAsmSymbol>>
            {
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>()
            };
            repeatLabels = new List<RepeatLocations> {
                new RepeatLocations() { MostRecentForward = shortLabel + "1" },
                new RepeatLocations() { MostRecentForward = shortLabel + "2" },
                new RepeatLocations() { MostRecentForward = shortLabel + "3" }
            };
            var part = xml.Root.Descendants("part");
            var measures = part?.Descendants("measure");
            var measureNumber = 0;
            var repeatSuffix = 'A';
            foreach (var measure in measures)
            {
                ++measureNumber;
                var voices = GroupNotesByVoice(measureNumber, measure);
                var repeat = measure.Descendants("barline")?.Descendants("repeat")?.FirstOrDefault();
                var voltaBracket = measure.Descendants("barline")?.Descendants("ending")?.FirstOrDefault();
                AddNotesOfOneMeasure(shortLabel + "1", repeatSuffix, asmSymbols[0], voices[0], repeatLabels[0], measureNumber, repeat, voltaBracket);
                AddNotesOfOneMeasure(shortLabel + "2", repeatSuffix, asmSymbols[1], voices[1], repeatLabels[1], measureNumber, repeat, voltaBracket);
                AddNotesOfOneMeasure(shortLabel + "3", repeatSuffix, asmSymbols[2], voices[2], repeatLabels[2], measureNumber, repeat, voltaBracket);
                if (repeat?.Attribute("direction")?.Value == "backward" && voltaBracket != null)
                    repeatSuffix += (char)2;
                else if (repeat != null || voltaBracket != null)
                    ++repeatSuffix;
            }
            return MergeSymbols(asmSymbols).ToList();
        }

        private static List<List<INote>> GroupNotesByVoice(int measureNumber, XElement measure)
        {
            var foundVoices = GetVoicesInMeasure(measure);
            var durationOfMeasure = foundVoices.First().Sum(n => n.Notes.First().Duration);
            var measureOfRests = new List<INote> { new Rest { Duration = durationOfMeasure } };

            var voice1 = foundVoices.Any() ? GetSingleNoteInChord(foundVoices.First(), 0) : measureOfRests;
            var voice3 = foundVoices.Count > 1 ? GetSingleNoteInChord(foundVoices.Last(), 0) : measureOfRests;
            var voiceWithChords = foundVoices.FirstOrDefault(v => v.Any(c => c.Notes.Count > 1));
            var voice2 = voiceWithChords != null ? GetSingleNoteInChord(voiceWithChords, 1) : measureOfRests;

            if (voice1.Sum(n => n.Duration) != durationOfMeasure
                || voice2.Sum(n => n.Duration) != durationOfMeasure
                || voice3.Sum(n => n.Duration) != durationOfMeasure)
                throw new Exception($"Unequal duration in measure {measureNumber}");

            return new List<List<INote>> { voice1, voice2, voice3 };
        }

        private static void AddNotesOfOneMeasure(string label, char repeatSuffix, List<IAsmSymbol> asmSymbols, List<INote> notes, RepeatLocations repeatLabels, int measureNumber, XElement repeat, XElement voltaBracket)
        {
            asmSymbols.Add(new Measure(measureNumber));
            if (voltaBracket != null)
            {
                asmSymbols.Add(new Label { LabelName = label + repeatSuffix });
                if (voltaBracket.Attribute("number").Value == "1")
                {
                    repeatLabels.VoltaBracketOne = label + repeatSuffix;
                }
                else
                {
                    repeatLabels.Labels.Add(repeatLabels.VoltaBracketOne);
                    repeatLabels.Labels.Add(label + repeatSuffix);
                }
            }
            if (repeat?.Attribute("direction")?.Value == "forward")
            {
                asmSymbols.Add(new Label { LabelName = label + repeatSuffix });
                repeatLabels.MostRecentForward = label + repeatSuffix;
            }
            asmSymbols.AddRange(notes);
            if (repeat?.Attribute("direction")?.Value == "backward")
            {
                var backwardRepeatSuffix = voltaBracket == null ? repeatSuffix : (char)(repeatSuffix + 1);
                asmSymbols.Add(new Label { LabelName = label + backwardRepeatSuffix });
                repeatLabels.Labels.Add(label + backwardRepeatSuffix);
                repeatLabels.Labels.Add(repeatLabels.MostRecentForward);
            }
        }

        private static List<INote> GetSingleNoteInChord(List<Chord> voice, int noteIndex)
        {
            var notes = new List<INote>();
            foreach(var chord in voice)
            {
                var primeNoteOfChord = chord.Notes.First();
                var newNote = chord.Notes.Count <= noteIndex
                    ? new Rest
                    {
                        Voice = primeNoteOfChord.Voice,
                        Duration = primeNoteOfChord.Duration
                    }
                    : chord.Notes[noteIndex];

                if (notes.LastOrDefault() is Rest rest && newNote is Rest && rest.Duration + newNote.Duration < 0x100)
                    notes[notes.Count-1] = new Rest { 
                        Voice = rest.Voice,
                        Duration = rest.Duration + newNote.Duration
                    };
                else
                    notes.Add(newNote);
            }
            return notes;
        }

        private static List<List<Chord>> GetVoicesInMeasure(XElement measure)
        {
            var voices = measure.Elements("note").Select(e => GetNote(e)).GroupBy(n => n.Voice).OrderBy(g => g.Key);
            return voices.Select(voice =>
            {
                var chords = new List<Chord>();
                foreach (var note in voice)
                {
                    if (note.IsInChord && chords.Any())
                        chords.Last().Notes.Add(note);
                    else
                        chords.Add(new Chord { Notes = new List<INote> { note } });
                }
                return chords;
            })
            .ToList();
        }

        private static INote GetNote(XElement note)
        {
            int.TryParse(note.Element("duration")?.Value, out var duration);
            var pitch = note.Element("pitch");
            var rest = note.Element("rest");
            var voice = int.TryParse(note.Element("voice")?.Value, out var v) ? v : 0;
            if (pitch != null)
            {
                var step = pitch.Element("step")?.Value ?? string.Empty;
                int.TryParse(pitch.Element("octave")?.Value, out var octave);
                return new Note
                {
                    Duration = duration,
                    Key = step + GetAccidental(pitch),
                    XmlOctive = octave,
                    Voice = voice,
                    IsInChord = note.Element("chord") != null
                };
            }
            else if (rest != null)
            {
                return new Rest
                {
                    Duration = duration,
                    Voice = voice,
                    IsInChord = note.Element("chord") != null
                };
            }
            else
            {
                throw new NotImplementedException("Unknown Note Type");
            }
        }

        private static string GetAccidental(XElement pitch)
        {
            int.TryParse(pitch.Element("alter")?.Value, out var alter);
            if (alter < -1 || alter > 1)
                throw new NotImplementedException($"Cannot support accidentals of type {alter}");
            if (alter == -1)
                return "b";
            if (alter == 1)
                return "s";
            return string.Empty;
        }

        private static IEnumerable<List<IAsmSymbol>> MergeSymbols(List<List<IAsmSymbol>> asmSymbols)
        {
            foreach (var soundGenerator in asmSymbols)
            {
                var newList = new List<IAsmSymbol>();
                Rest prevRest = null;
                Measure prevRestMeasure = null;
                Measure currentMeasure = null;
                foreach (var asmSymbol in soundGenerator)
                {
                    if (asmSymbol is Rest currentRest)
                    {
                        if (prevRest != null && prevRest.Duration + currentRest.Duration < 0x100)
                        {
                            prevRest.Duration += currentRest.Duration;
                            prevRestMeasure.EndingNumber = currentMeasure.Number;
                            newList.Remove(currentMeasure);
                        }
                        else
                        {
                            prevRest = currentRest;
                            prevRestMeasure = currentMeasure;
                            newList.Add(asmSymbol);
                        }
                    }
                    else
                    {
                        if (asmSymbol is Measure)
                        {
                            currentMeasure = asmSymbol as Measure;
                        }
                        else
                        {
                            prevRest = null;
                            prevRestMeasure = null;
                        }
                        newList.Add(asmSymbol);
                    }
                }
                yield return newList;
            }
        }
    }
}
