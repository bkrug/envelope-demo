using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System.Collections.Generic;

namespace MusicXmlParser.SN76489Generation
{
    internal class SN76489NoteGenerator
    {
        internal List<ToneGenerator> GetToneGenerators(List<NewPart> parsedParts)
        {
            var toneGenerators = NoteToGeneratorGrouper.AssignNotesToToneGenerators(parsedParts);
            RepeatPopulator.PopulateRepeatLabels(parsedParts, ref toneGenerators);
            MergeRests(ref toneGenerators);
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
                    if (currentNote.Pitch != Pitch.REST || nextNote.Pitch != Pitch.REST)
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