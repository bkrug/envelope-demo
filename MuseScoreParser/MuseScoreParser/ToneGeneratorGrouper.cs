using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MuseScoreParser
{
    internal class ToneGeneratorGrouper
    {
        private const int TONE_GENERATORS_PER_SN76489 = 3;

        internal List<ToneGenerator> GetToneGenerators(List<NewPart> parsedParts)
        {
            var toneGenerators = new List<ToneGenerator>();
            for(var currentMeasure = 1; currentMeasure <= parsedParts.First().Measures.Count; currentMeasure++)
            {
                var generatorsInMeasure = new List<List<GeneratorNote>>();
                foreach (var parsedPart in parsedParts)
                {
                    var voiceKeys = parsedPart.Measures[currentMeasure - 1].Voices.Keys;
                    foreach (var voiceKey in voiceKeys)
                    {
                        if (generatorsInMeasure.Count >= TONE_GENERATORS_PER_SN76489)
                            break;
                        var voice = parsedPart.Measures[currentMeasure - 1].Voices[voiceKey];
                        var notesInMeasure = GetNotesForOneToneGenerator(voice, currentMeasure);
                        if (notesInMeasure.Any(n => n.Pitch != Pitch.REST))
                        {
                            generatorsInMeasure.Add(notesInMeasure);
                        }
                    }
                }
                for(var g = 0; g < TONE_GENERATORS_PER_SN76489 && g < generatorsInMeasure.Count; ++g)
                {
                    if (toneGenerators.Count == g)
                        toneGenerators.Add(new ToneGenerator());
                    toneGenerators[g].GeneratorNotes.AddRange(generatorsInMeasure[g]);
                }
            }
            return toneGenerators;
        }

        private List<GeneratorNote> GetNotesForOneToneGenerator(NewVoice measure, int currentMeasure)
        {
            var generatorNotes = new List<GeneratorNote>();
            var notesInMeasure = measure.Chords
                .Select(c => c.Notes.Single())  //TODO: This line of code should eventually cause a test to fail.
                .Select(n => new GeneratorNote
                {
                    StartMeasure = currentMeasure,
                    EndMeasure = currentMeasure,
                    Pitch = GetPitch(n),
                    Duration = GetDuration(n)
                });
            generatorNotes.AddRange(notesInMeasure);
            return generatorNotes.ToList();
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
            if(parsedNote.IsRest)
            {
                return Pitch.REST;
            }

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
                _ => throw new System.Exception($"Unrecognized duration type {parsedNote.Type}"),
            };
        }
    }
}