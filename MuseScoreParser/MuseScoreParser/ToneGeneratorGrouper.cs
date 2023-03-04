using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MuseScoreParser
{
    internal class ToneGeneratorGrouper
    {
        internal List<ToneGenerator> GetToneGenerators(List<NewPart> parsedParts)
        {
            var toneGenerators = new List<ToneGenerator>();
            foreach(var parsedPart in parsedParts)
            {
                var measures = parsedPart.Measures.Select(m => m.Voices.Single().Value).ToList();
                toneGenerators.Add(GetNotesForOneToneGenerator(measures));
            }
            return toneGenerators;
        }

        private ToneGenerator GetNotesForOneToneGenerator(List<NewVoice> measures)
        {
            var generatorNotes = new List<GeneratorNote>();
            for (var measureNumber = 1; measureNumber <= measures.Count; ++measureNumber)
            {
                var notesInMeasure = measures[measureNumber - 1].Chords
                    .Select(c => c.Notes.Single())
                    .Select(n => new GeneratorNote
                    {
                        StartMeasure = measureNumber,
                        EndMeasure = measureNumber,
                        Pitch = GetPitch(n),
                        Duration = GetDuration(n)
                    });
                generatorNotes.AddRange(notesInMeasure);
            }
            var toneGenerator = new ToneGenerator
            {
                GeneratorNotes = generatorNotes.ToList()
            };
            return toneGenerator;
        }

        private static ReadOnlyDictionary<string, int> _notesWithinOctive =
            new ReadOnlyDictionary<string, int>(
                new Dictionary<string, int>
                {
                    { "C", 0 },
                    { "D", 2 },
                    { "E", 4 },
                    { "F", 5 },
                    { "G", 7 },
                    { "A", 9 },
                    { "B", 11 },
                }
            );

        //TODO: Add tests for invalid data
        private Pitch GetPitch(NewNote parsedNote)
        {
            var pitchInt = (int.Parse(parsedNote.Octave) - 1) * 12 + 3;
            var alterInt = string.IsNullOrWhiteSpace(parsedNote.Alter) ? 0 : int.Parse(parsedNote.Alter);
            var withinOctiveInt = _notesWithinOctive[parsedNote.Step.ToUpper()] + alterInt;
            return (Pitch)(pitchInt + withinOctiveInt);
        }

        //TODO: What are all the valid duration types?
        private Duration GetDuration(NewNote parsedNote)
        {
            return parsedNote.Type switch
            {
                "16th" => Duration.N16,
                "eigth" => Duration.N8,
                "quarter" => Duration.N4,
                "half" => Duration.N2,
                _ => throw new System.Exception("Unrecognized duration type"),
            };
        }
    }
}