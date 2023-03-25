using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class Chord
    {
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}