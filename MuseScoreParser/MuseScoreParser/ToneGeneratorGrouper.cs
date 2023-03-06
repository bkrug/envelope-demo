using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MuseScoreParser
{
    internal class ToneGeneratorGrouper
    {
        private const int TOTAL_GENERATORS_IN_SN76489 = 3;

        internal List<ToneGenerator> GetToneGenerators(List<NewPart> parsedParts)
        {
            var toneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator(),
                new ToneGenerator(),
                new ToneGenerator()
            };
            for(var currentMeasure = 1; currentMeasure <= parsedParts.First().Measures.Count; currentMeasure++)
            {
                var generatorsInMeasure = GroupNotesByToneGenerators(parsedParts, currentMeasure);
                RemoveExtraGenerators(generatorsInMeasure);
                for (var g = 0; g < generatorsInMeasure.Count; ++g)
                {
                    toneGenerators[g].GeneratorNotes.AddRange(generatorsInMeasure[g]);
                }
            }
            return toneGenerators
                .Where(tg => tg.GeneratorNotes.Any(n => n.Pitch != Pitch.REST))
                .ToList();
        }

        private List<List<GeneratorNote>> GroupNotesByToneGenerators(List<NewPart> parsedParts, int currentMeasure)
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

        private List<GeneratorNote> GetNotesForOneToneGenerator(NewVoice measure, int currentMeasure, int chordIndex)
        {
            var notesInMeasure = measure.Chords
                .Select(c => c.Notes.Count > chordIndex 
                    ? c.Notes.ElementAt(chordIndex)
                    : new NewNote {
                        IsRest = true,
                        Type = c.Notes.First().Type
                    })
                .Select(n => new GeneratorNote
                {
                    StartMeasure = currentMeasure,
                    EndMeasure = currentMeasure,
                    Pitch = GetPitch(n),
                    //TODO: This isn't handling dotted notes.
                    //TODO: Doesn't tell user when the durration is invalid
                    Duration = DurationParser.TryParse(n.Type, out var d) ? d : default
                })
                .ToList();
            if (notesInMeasure.All(n => n.Pitch == Pitch.REST))
            {
                return new List<GeneratorNote>
                {
                    new GeneratorNote
                    {
                        StartMeasure = currentMeasure,
                        EndMeasure = currentMeasure,
                        Pitch = Pitch.REST,
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
                var restsOnly = generatorsInMeasure.FirstOrDefault(g => g.All(n => n.Pitch == Pitch.REST));
                var generatorToRemove = restsOnly ?? generatorsInMeasure.Last();
                generatorsInMeasure.Remove(generatorToRemove);
            }
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
    }
}