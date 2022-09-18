using MuseScoreParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MuseScoreParser
{
    internal class NoteParser
    {
        internal static List<List<IAsmSymbol>> GetNotes(XDocument xml, string shortLabel)
        {
            var part = xml.Root.Descendants("part");
            var measures = part?.Descendants("measure");
            var allNotes = new List<List<IAsmSymbol>>()
            {
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>()
            };
            var measureNumber = 0;
            var repeatSuffix = 'A';
            foreach (var measure in measures)
            {
                GroupNotesByVoice(measureNumber, measure, out var voices, out var repeat);
                ++measureNumber;
                AddNotesOfOneMeasure(shortLabel + "1" + repeatSuffix, allNotes[0], measureNumber, voices[0], repeat);
                AddNotesOfOneMeasure(shortLabel + "2" + repeatSuffix, allNotes[1], measureNumber, voices[1], repeat);
                AddNotesOfOneMeasure(shortLabel + "3" + repeatSuffix, allNotes[2], measureNumber, voices[2], repeat);
                if (repeat != null)
                    ++repeatSuffix;
            }
            return allNotes;
        }

        private static void GroupNotesByVoice(int measureNumber, XElement measure, out List<List<INote>> notesByVoice, out XElement repeat)
        {
            var foundVoices = GetVoicesInMeasure(measure);
            var durationOfMeasure = foundVoices.First().Sum(n => n.Notes.First().Duration);
            var measureOfRests = new List<INote> { new Rest { Duration = durationOfMeasure } };

            var voice1 = foundVoices.Any() ? GetSingleNoteInChord(foundVoices.First(), 0) : measureOfRests;
            var voice3 = foundVoices.Count > 1 ? GetSingleNoteInChord(foundVoices.Last(), 0) : measureOfRests;
            var voiceWithChords = foundVoices.FirstOrDefault(v => v.Any(c => c.Notes.Count > 1));
            var voice2 = voiceWithChords != null ? GetSingleNoteInChord(voiceWithChords, 1) : measureOfRests;
            notesByVoice = new List<List<INote>> { voice1, voice2, voice3 };

            if (voice1.Sum(n => n.Duration) != durationOfMeasure
                || voice2.Sum(n => n.Duration) != durationOfMeasure
                || voice3.Sum(n => n.Duration) != durationOfMeasure)
                throw new Exception($"Unequal duration in measure {measureNumber}");

            repeat = measure.Descendants("barline")?.Descendants("repeat")?.FirstOrDefault();
        }

        private static void AddNotesOfOneMeasure(string label, List<IAsmSymbol> voice, int measureNumber, List<INote> notes, XElement repeat)
        {
            voice.Add(new Measure(measureNumber));
            if (repeat?.Attribute("direction")?.Value == "forward")
                voice.Add(new Label { LabelName = label });
            voice.AddRange(notes);
            if (repeat?.Attribute("direction")?.Value == "backward")
                voice.Add(new Label { LabelName = label });
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
    }
}
