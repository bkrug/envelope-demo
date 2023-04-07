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
            var toneGenerators = GroupNotesByToneGenerators(parsedMusic, logger);
            toneGenerators = SelectMeasuresOfNonRests(toneGenerators);
            return toneGenerators;
        }

        private static List<ToneGenerator> GroupNotesByToneGenerators(ParsedMusic parsedMusic, ILogger logger)
        {
            var divisions = int.TryParse(parsedMusic.Divisions, out var parseResult) ? parseResult : 0;
            var generatorsInMeasure = new Dictionary<(int, int, string), ToneGenerator>();
            for (var chordIndex = 0; chordIndex < TOTAL_GENERATORS_IN_SN76489; ++chordIndex)
            {
                for (var partIndex = 0; partIndex < parsedMusic.Parts.Count; ++partIndex)
                {
                    var parsedPart = parsedMusic.Parts[partIndex];
                    for (var currentMeasure = 1; currentMeasure <= parsedPart.Measures.Count; currentMeasure++)
                    {
                        foreach (var keyAndVoice in parsedPart.Measures[currentMeasure - 1].Voices)
                        {
                            var notesInMeasure = GetNotesForOneToneGenerator(keyAndVoice.Value, divisions, currentMeasure, chordIndex, logger);
                            var dictionaryKey = (chordIndex, partIndex, keyAndVoice.Key);
                            if (!generatorsInMeasure.ContainsKey(dictionaryKey))
                                generatorsInMeasure[dictionaryKey] = new ToneGenerator();
                            generatorsInMeasure[dictionaryKey].GeneratorNotes.AddRange(notesInMeasure);
                        }
                    }
                }
            }
            return generatorsInMeasure
                .OrderBy(kvp => kvp.Key.Item1)
                .ThenBy(kvp => kvp.Key.Item2)
                .ThenBy(kvp => kvp.Key.Item3)
                .Select(kvp => kvp.Value).ToList();
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
                    if (!PitchParser.TryParse(n, out var pitch) && !n.IsRest && !n.IsGraceNote)
                        logger.WriteError($"Could not parse pitch in measure {currentMeasure}: " + JsonConvert.SerializeObject(n));
                    if (!int.TryParse(n.Duration, out var duration) && !n.IsRest && !n.IsGraceNote)
                        logger.WriteError($"Could not parse duration in measure {currentMeasure}: \"{n.Duration}\"");
                    return new GeneratorNote
                    {
                        StartMeasure = currentMeasure,
                        EndMeasure = currentMeasure,
                        Pitch = pitch,
                        Duration = (Duration)((double)Duration.N4 / lengthOfQuarter * duration),
                        IsGraceNote = n.IsGraceNote
                    };
                })
                .ToList();
            return notesInMeasure;
        }

        private static List<ToneGenerator> SelectMeasuresOfNonRests(List<ToneGenerator> oldToneGenerators)
        {
            var maxMeasure = oldToneGenerators.First().GeneratorNotes.Max(gn => gn.EndMeasure);
            var startingMeasure = 1;
            var endingMeasure = 1;
            var newToneGenerators = new List<ToneGenerator>
            {
                new ToneGenerator(),
                new ToneGenerator(),
                new ToneGenerator()
            };
            while (startingMeasure <= maxMeasure)
            {
                foreach (var toneGenerator in oldToneGenerators)
                {
                    var testAgain = true;
                    while (testAgain)
                    {
                        var multiMeasureNote = toneGenerator.GeneratorNotes.FirstOrDefault(gn => gn.StartMeasure == endingMeasure && gn.EndMeasure != endingMeasure);
                        if (multiMeasureNote != null)
                            endingMeasure = multiMeasureNote.EndMeasure;
                        testAgain = multiMeasureNote != null;
                    }
                }

                var newToneGeneratorIndex = 0;
                for (var oldToneGeneratorIndex = 0; oldToneGeneratorIndex < oldToneGenerators.Count; ++oldToneGeneratorIndex)
                {
                    var toneGenerator = oldToneGenerators[oldToneGeneratorIndex];
                    var notesInMeasureGroup = toneGenerator.GeneratorNotes.Where(gn => gn.StartMeasure >= startingMeasure && gn.EndMeasure <= endingMeasure).ToList();
                    var containsNonRest = notesInMeasureGroup.Any(gn => gn.Pitch != nameof(Pitch.REST));
                    var thisIsOneOfLastThreeGenerators = oldToneGenerators.Count - oldToneGeneratorIndex < TOTAL_GENERATORS_IN_SN76489;
                    if (containsNonRest || thisIsOneOfLastThreeGenerators)
                    {
                        newToneGenerators[newToneGeneratorIndex].GeneratorNotes.AddRange(notesInMeasureGroup);
                        ++newToneGeneratorIndex;
                        if (newToneGeneratorIndex >= newToneGenerators.Count)
                            break;
                    }
                }
                ++endingMeasure;
                startingMeasure = endingMeasure;
            }
            return newToneGenerators.Where(tg => tg.GeneratorNotes.Any() && tg.GeneratorNotes.Any(n => n.Pitch != nameof(Pitch.REST))).ToList();
        }
    }
}