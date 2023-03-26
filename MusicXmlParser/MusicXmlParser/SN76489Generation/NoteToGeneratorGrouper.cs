using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.SN76489Generation
{
    internal static class NoteToGeneratorGrouper
    {
        private const int TOTAL_GENERATORS_IN_SN76489 = 3;

        internal static List<ToneGenerator> AssignNotesToToneGenerators(ParsedMusic parsedMusic, ILogger logger)
        {
            var parsedParts = parsedMusic.Parts;
            var toneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator(),
                new ToneGenerator(),
                new ToneGenerator()
            };
            for (var currentMeasure = 1; currentMeasure <= parsedParts.First().Measures.Count; currentMeasure++)
            {
                var generatorsInMeasure = GroupNotesByToneGenerators(parsedMusic, currentMeasure, logger);
                RemoveExtraGenerators(generatorsInMeasure);
                for (var g = 0; g < generatorsInMeasure.Count; ++g)
                {
                    toneGenerators[g].GeneratorNotes.AddRange(generatorsInMeasure[g]);
                }

            }
            toneGenerators = toneGenerators
                .Where(tg => tg.GeneratorNotes.Any() && tg.GeneratorNotes.Any(n => n.Pitch != nameof(Pitch.REST)))
                .ToList();
            return toneGenerators;
        }

        private static List<List<GeneratorNote>> GroupNotesByToneGenerators(ParsedMusic parsedMusic, int currentMeasure, ILogger logger)
        {
            var divisions = int.TryParse(parsedMusic.Divisions, out var parseResult) ? parseResult : 0;
            var generatorsInMeasure = new List<List<GeneratorNote>>();
            for (var chordIndex = 0; chordIndex < TOTAL_GENERATORS_IN_SN76489; ++chordIndex)
            {
                foreach (var parsedPart in parsedMusic.Parts)
                {
                    var voiceKeys = parsedPart.Measures[currentMeasure - 1].Voices.Keys;
                    foreach (var voiceKey in voiceKeys)
                    {
                        var voice = parsedPart.Measures[currentMeasure - 1].Voices[voiceKey];
                        var notesInMeasure = GetNotesForOneToneGenerator(voice, divisions, currentMeasure, chordIndex, logger);
                        generatorsInMeasure.Add(notesInMeasure);
                    }
                }
            }
            return generatorsInMeasure;
        }

        private static List<GeneratorNote> GetNotesForOneToneGenerator(Voice measure, int lengthOfQuarter, int currentMeasure, int chordIndex, ILogger logger)
        {
            var notesInMeasure = measure.Chords
                .Select(c => c.Notes.Count > chordIndex
                    ? c.Notes.ElementAt(chordIndex)
                    : new Note
                    {
                        IsRest = true,
                        Type = c.Notes.First().Type,
                        Duration = c.Notes.First().Duration
                    })
                .Select(n => {
                    if (!PitchParser.TryParse(n, out var pitch))
                        logger.WriteError("Could not parse pitch: " + JsonConvert.SerializeObject(n));
                    if (!int.TryParse(n.Duration, out var duration))
                        logger.WriteError("Could not parse duration: " + JsonConvert.SerializeObject(n));
                    return new GeneratorNote
                    {
                        StartMeasure = currentMeasure,
                        EndMeasure = currentMeasure,
                        Pitch = pitch,
                        Duration = (Duration)(lengthOfQuarter / 24 * duration),
                        IsGraceNote = n.IsGraceNote
                    };
                })
                .ToList();
            return notesInMeasure;
        }

        //If we found more tone generators than exist in the SN76489, then remove the extras.
        private static void RemoveExtraGenerators(List<List<GeneratorNote>> generatorsInMeasure)
        {
            while (generatorsInMeasure.Count > TOTAL_GENERATORS_IN_SN76489)
            {
                var restsOnly = generatorsInMeasure.FirstOrDefault(g => g.All(n => n.Pitch == nameof(Pitch.REST)));
                var generatorToRemove = restsOnly ?? generatorsInMeasure.Last();
                generatorsInMeasure.Remove(generatorToRemove);
            }
        }

    }
}