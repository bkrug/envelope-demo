using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class ToneGenerator
    {
        public List<(string FromThisLabel, string JumpToThisLabel)> RepeatLabels { get; set; } = new List<(string FromThisLabel, string JumpToThisLabel)>();
        public List<GeneratorNote> GeneratorNotes { get; set; } = new List<GeneratorNote>();
    }
}