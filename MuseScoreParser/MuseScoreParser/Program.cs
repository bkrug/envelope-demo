using MuseScoreParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MuseScoreParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = GetOptions(args);
            var xml = XDocument.Load(options.InputFile);
            var part = xml.Root.Descendants("part");
            var measures = part?.Descendants("measure");
            var notes = GetNotes(measures);
            WriteFile(options, notes);
        }

        private static Options GetOptions(string[] args)
        {
            var options = new Options();
            options.InputFile = args[0];
            options.OutputFile = args[1];
            options.AsmLabel = args[2];
            options.Ratio60Hz = args[3].Replace(":", ",");
            options.Ratio50Hz = args[4].Replace(":", ",");
            return options;
        }

        private static List<List<IAsmSymbol>> GetNotes(IEnumerable<XElement> measures)
        {
            var allNotes = new List<List<IAsmSymbol>>()
            {
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>(),
                new List<IAsmSymbol>()
            };
            var measureNumber = 0;
            foreach (var measure in measures)
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

                ++measureNumber;
                allNotes[0].Add(new Measure(measureNumber));
                allNotes[0].AddRange(voice1);
                allNotes[1].Add(new Measure(measureNumber));
                allNotes[1].AddRange(voice2);
                allNotes[2].Add(new Measure(measureNumber));
                allNotes[2].AddRange(voice3);
            }
            return allNotes;
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

        private static void WriteFile(Options options, List<List<IAsmSymbol>> notes)
        {
            var writer = File.CreateText(options.OutputFile);
            writer.WriteLine($"       DEF  {options.AsmLabel}");
            writer.WriteLine();
            writer.WriteLine("       COPY 'NOTEVAL.asm'");
            writer.WriteLine("       COPY 'CONST.asm'");
            writer.WriteLine();
            writer.WriteLine("*");
            writer.WriteLine("* Song Header");
            writer.WriteLine("*");
            writer.WriteLine($"{options.Label6Char} DATA {options.ShortLabel}1, {options.ShortLabel}2, {options.ShortLabel}3");
            writer.WriteLine("*Duration ratio in 60hz environment");
            writer.WriteLine($"       DATA {options.Ratio60Hz}");
            writer.WriteLine("*Duration ratio in 50hz environment");
            writer.WriteLine($"       DATA {options.Ratio50Hz}");
            writer.WriteLine();
            for (var generator = 1; generator <= 3; ++generator)
            {
                var label = options.ShortLabel + generator;
                writer.WriteLine($"* Generator {generator}");
                writer.WriteLine(label);
                foreach (var symbol in notes[generator - 1])
                {
                    writer.WriteLine(symbol.ToAsm());
                }
                writer.WriteLine($"       DATA REPEAT,{label}");
                writer.WriteLine(string.Empty);
            }
            writer.Close();
        }
    }
}
