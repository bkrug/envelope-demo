using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System.Collections.Generic;

namespace MusicXmlParser.SN76489Generation
{
    internal class SN76489NoteGenerator
    {
        internal List<ToneGenerator> GetToneGenerators(List<NewPart> parsedParts, string labelPrefix)
        {
            var toneGenerators = NoteToGeneratorGrouper.AssignNotesToToneGenerators(parsedParts);
            RepeatPopulator.PopulateRepeatLabels(parsedParts, labelPrefix, ref toneGenerators);
            MergeRests(ref toneGenerators);
            return toneGenerators;
        }

        //TODO: Write a test to confirm that we can't loose labels as a result of this
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
                    currentNote.Duration += (int)nextNote.Duration;
                    currentNote.EndMeasure = nextNote.EndMeasure;
                    toneGenerator.GeneratorNotes[i] = currentNote;
                    toneGenerator.GeneratorNotes.RemoveAt(i + 1);
                }
            }
        }
    }
}