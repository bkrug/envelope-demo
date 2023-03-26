using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.SN76489Generation
{
    internal class SN76489NoteGenerator
    {
        internal List<ToneGenerator> GetToneGenerators(ParsedMusic parsedMusic, string labelPrefix, Options options)
        {
            var toneGenerators = NoteToGeneratorGrouper.AssignNotesToToneGenerators(parsedMusic);
            RepeatPopulator.PopulateRepeatLabels(parsedMusic.Parts, labelPrefix, options, ref toneGenerators);
            MergeRests(ref toneGenerators);

            if (toneGenerators.Count > 1)
            {
                var expectedDuration = toneGenerators.First().GeneratorNotes.Sum(n => (int)n.Duration);
                if (!toneGenerators.Skip(1).All(tg => tg.GeneratorNotes.Sum(n => (int)n.Duration) == expectedDuration))
                    throw new Exception("All voices must have the same duration");
            }

            return toneGenerators;
        }

        private static void MergeRests(ref List<ToneGenerator> toneGenerators)
        {
            foreach (var toneGenerator in toneGenerators)
            {
                for (var i = toneGenerator.GeneratorNotes.Count - 2; i >= 0; --i)
                {
                    var currentNote = toneGenerator.GeneratorNotes[i];
                    var nextNote = toneGenerator.GeneratorNotes[i + 1];
                    if (currentNote.Pitch != nameof(Pitch.REST) || nextNote.Pitch != nameof(Pitch.REST))
                        continue;
                    if ((int)currentNote.Duration + (int)nextNote.Duration > byte.MaxValue)
                        continue;
                    if (!string.IsNullOrWhiteSpace(currentNote.LabelAtEnd) || !string.IsNullOrWhiteSpace(nextNote.Label))
                        continue;
                    currentNote.Duration += (int)nextNote.Duration;
                    currentNote.EndMeasure = nextNote.EndMeasure;
                    currentNote.LabelAtEnd = nextNote.LabelAtEnd;
                    toneGenerator.GeneratorNotes[i] = currentNote;
                    toneGenerator.GeneratorNotes.RemoveAt(i + 1);
                }
            }
        }
    }
}