using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class Voice
    {
        public List<Chord> Chords { get; set; } = new List<Chord>();
    }
}