using MusicXmlParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser
{
    internal static class ListExtensions
    {
        /// <summary>
        /// Returns notes from one or more measures,
        /// but the first note in each List will be at the start of a measure, the last note will be at the end of a measure.
        /// Some of the notes in the middle are in two measures.
        /// </summary>
        internal static IEnumerable<List<GeneratorNote>> GroupByMeasure(this IEnumerable<GeneratorNote> sourceNotes)
        {
            var currentMeasures = new List<GeneratorNote>();
            foreach (var sourceNote in sourceNotes)
            {
                if (!currentMeasures.Any())
                    currentMeasures.Add(sourceNote);
                else if (sourceNote.StartMeasure == currentMeasures.LastOrDefault()?.EndMeasure)
                    currentMeasures.Add(sourceNote);
                else
                {
                    yield return currentMeasures;
                    currentMeasures = new List<GeneratorNote>
                    {
                        sourceNote
                    };
                }
            }
            yield return currentMeasures;
        }
    }
}
