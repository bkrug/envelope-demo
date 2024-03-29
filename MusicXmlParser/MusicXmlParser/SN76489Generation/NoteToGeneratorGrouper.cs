﻿using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using Newtonsoft.Json;
using System;
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
            MergeTies(ref toneGenerators);
            toneGenerators = PrioritizeMeasuresOfNonRests(toneGenerators);
            return toneGenerators;
        }

        private static List<ToneGenerator> GroupNotesByToneGenerators(ParsedMusic parsedMusic, ILogger logger)
        {
            var notesByPartAndVoice = new Dictionary<(int, int, string), ToneGenerator>();
            for (var chordIndex = 0; chordIndex < TOTAL_GENERATORS_IN_SN76489; ++chordIndex)
            {
                for (var partIndex = 0; partIndex < parsedMusic.Parts.Count; ++partIndex)
                {
                    var parsedPart = parsedMusic.Parts[partIndex];
                    var divisions = int.TryParse(parsedPart.Divisions, out var parseResult) ? parseResult : 0;
                    for (var currentMeasure = 1; currentMeasure <= parsedPart.Measures.Count; currentMeasure++)
                    {
                        foreach (var keyAndVoice in parsedPart.Measures[currentMeasure - 1].Voices)
                        {
                            var notesInMeasure = GetNotesForOneToneGenerator(keyAndVoice.Value, divisions, currentMeasure, chordIndex, logger).ToList();
                            var dictionaryKey = (chordIndex, partIndex, keyAndVoice.Key);
                            if (!notesByPartAndVoice.ContainsKey(dictionaryKey))
                            {
                                notesByPartAndVoice[dictionaryKey] = GetGenerator(notesByPartAndVoice, currentMeasure);
                            }
                            notesByPartAndVoice[dictionaryKey].GeneratorNotes.AddRange(notesInMeasure);
                        }
                        FillInEmptyVoices(currentMeasure, ref notesByPartAndVoice);
                    }
                }
            }
            return notesByPartAndVoice
                .OrderBy(kvp => kvp.Key.Item1)
                .ThenBy(kvp => kvp.Key.Item2)
                .ThenBy(kvp => kvp.Key.Item3)
                .Select(kvp => kvp.Value).ToList();
        }

        private static ToneGenerator GetGenerator(Dictionary<(int, int, string), ToneGenerator> generatorsInMeasure, int currentMeasure)
        {
            var newGenerator = new ToneGenerator();
            for (var measureToFill = 1; measureToFill < currentMeasure; ++measureToFill)
            {
                //If this voice was not present in earlier measures, fill it with implied rests
                var durationToFill = generatorsInMeasure.Max(kvp => kvp.Value.GeneratorNotes.Where(n => n.StartMeasure == measureToFill).Sum(n => (int)n.Duration));
                newGenerator.GeneratorNotes.Add(new GeneratorNote
                {
                    Pitch = nameof(Pitch.REST),
                    Duration = (Duration)durationToFill,
                    StartMeasure = measureToFill,
                    EndMeasure = measureToFill
                });
            }
            return newGenerator;
        }

        //If this voice was not present in this measure, fill it with implied rests
        private static void FillInEmptyVoices(int currentMeasure, ref Dictionary<(int, int, string), ToneGenerator> notesByPartAndVoice)
        {
            var durationToFill = notesByPartAndVoice.Max(kvp => kvp.Value.GeneratorNotes.Where(n => n.StartMeasure == currentMeasure).Sum(n => (int)n.Duration));
            foreach (var noteGroup in notesByPartAndVoice)
            {
                if (noteGroup.Value.GeneratorNotes.Any(n => n.StartMeasure == currentMeasure))
                    continue;
                noteGroup.Value.GeneratorNotes.Add(new GeneratorNote
                {
                    Pitch = nameof(Pitch.REST),
                    Duration = (Duration)durationToFill,
                    StartMeasure = currentMeasure,
                    EndMeasure = currentMeasure
                });
            }
        }

        private static IEnumerable<GeneratorNote> GetNotesForOneToneGenerator(Voice measure, int lengthOfQuarter, int currentMeasure, int chordIndex, ILogger logger)
        {
            foreach (var chord in measure.Chords)
            {
                var noteFromChord = chordIndex < chord.Notes.Count
                    ? chord.Notes.ElementAt(chordIndex)
                    : new Note
                    {
                        IsRest = true,
                        Duration = chord.Notes.First().Duration
                    };
                if (!PitchParser.TryParse(noteFromChord, out var pitch) && !noteFromChord.IsRest && !noteFromChord.IsGraceNote)
                    logger.WriteError($"Could not parse pitch in measure {currentMeasure}: " + JsonConvert.SerializeObject(noteFromChord));
                if (!int.TryParse(noteFromChord.Duration, out var duration) && !noteFromChord.IsRest && !noteFromChord.IsGraceNote)
                    logger.WriteError($"Could not parse duration in measure {currentMeasure}: \"{noteFromChord.Duration}\"");
                yield return new GeneratorNote
                {
                    StartMeasure = currentMeasure,
                    EndMeasure = currentMeasure,
                    Pitch = pitch,
                    Duration = (Duration)((double)Duration.N4 / lengthOfQuarter * duration),
                    IsGraceNote = noteFromChord.IsGraceNote,
                    Tie = noteFromChord.Tie
                };
            }
        }

        private static void MergeTies(ref List<ToneGenerator> toneGenerators)
        {
            foreach (var toneGenerator in toneGenerators)
            {
                for (var i = toneGenerator.GeneratorNotes.Count - 2; i >= 0; --i)
                {
                    var currentNote = toneGenerator.GeneratorNotes[i];
                    var nextNote = toneGenerator.GeneratorNotes[i + 1];
                    if (currentNote.Tie != Ties.Start && currentNote.Tie != Ties.End)
                        continue;
                    if ((int)currentNote.Duration + (int)nextNote.Duration > byte.MaxValue)
                        continue;
                    currentNote.Duration += (int)nextNote.Duration;
                    currentNote.EndMeasure = nextNote.EndMeasure;
                    currentNote.LabelAtEnd = nextNote.LabelAtEnd;
                    toneGenerator.GeneratorNotes[i] = currentNote;
                    toneGenerator.GeneratorNotes.RemoveAt(i + 1);
                }
            }
        }

        private static List<ToneGenerator> PrioritizeMeasuresOfNonRests(List<ToneGenerator> oldToneGenerators)
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