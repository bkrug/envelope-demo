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
            var measures = parsedParts.Single().Measures.Select(m => m.Voices.Single().Value).ToList();
            var generatorNotes = new List<GeneratorNote>();
            for (var measureNumber = 1; measureNumber <= measures.Count; ++ measureNumber)
            {
                var notesInMeasure = measures[measureNumber - 1].Chords
                    .Select(c => c.Notes.Single())
                    .Select(n => new GeneratorNote
                     {
                         StartMeasure = measureNumber,
                         EndMeasure = measureNumber,
                         Pitch = GetPitch(n)
                     });
                generatorNotes.AddRange(notesInMeasure);
            }
            return new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = generatorNotes.ToList()
                }
            };
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
    }
}