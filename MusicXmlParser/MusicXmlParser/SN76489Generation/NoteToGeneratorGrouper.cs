﻿using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.SN76489Generation
{
    internal static class NoteToGeneratorGrouper
    {
        private const int TOTAL_GENERATORS_IN_SN76489 = 3;

        internal static List<ToneGenerator> AssignNotesToToneGenerators(List<NewPart> parsedParts)
        {
            var toneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator(),
                new ToneGenerator(),
                new ToneGenerator()
            };
            for (var currentMeasure = 1; currentMeasure <= parsedParts.First().Measures.Count; currentMeasure++)
            {
                var generatorsInMeasure = GroupNotesByToneGenerators(parsedParts, currentMeasure);
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

        private static List<List<GeneratorNote>> GroupNotesByToneGenerators(List<NewPart> parsedParts, int currentMeasure)
        {
            var generatorsInMeasure = new List<List<GeneratorNote>>();
            for (var chordIndex = 0; chordIndex < TOTAL_GENERATORS_IN_SN76489; ++chordIndex)
            {
                foreach (var parsedPart in parsedParts)
                {
                    var voiceKeys = parsedPart.Measures[currentMeasure - 1].Voices.Keys;
                    foreach (var voiceKey in voiceKeys)
                    {
                        var voice = parsedPart.Measures[currentMeasure - 1].Voices[voiceKey];
                        var notesInMeasure = GetNotesForOneToneGenerator(voice, currentMeasure, chordIndex);
                        generatorsInMeasure.Add(notesInMeasure);
                    }
                }
            }
            return generatorsInMeasure;
        }

        private static List<GeneratorNote> GetNotesForOneToneGenerator(NewVoice measure, int currentMeasure, int chordIndex)
        {
            var notesInMeasure = measure.Chords
                .Select(c => c.Notes.Count > chordIndex
                    ? c.Notes.ElementAt(chordIndex)
                    : new NewNote
                    {
                        IsRest = true,
                        Type = c.Notes.First().Type
                    })
                .Select(n => new GeneratorNote
                {
                    StartMeasure = currentMeasure,
                    EndMeasure = currentMeasure,
                    //TODO: Doesn't tell user when the pitch is invalid
                    Pitch = PitchParser.TryParse(n, out var p) ? p : default,
                    //TODO: Doesn't tell user when the duration is invalid
                    Duration = DurationParser.TryParse(n, out var d) ? d : default,
                    IsGraceNote = n.IsGraceNote
                })
                .ToList();
            if (notesInMeasure.All(n => n.Pitch == nameof(Pitch.REST)))
            {
                return new List<GeneratorNote>
                {
                    new GeneratorNote
                    {
                        StartMeasure = currentMeasure,
                        EndMeasure = currentMeasure,
                        Pitch = nameof(Pitch.REST),
                        Duration = (Duration)notesInMeasure.Sum(n => (int)n.Duration)
                    }
                };
            }
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