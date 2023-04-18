using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class ParsedMusic
    {
        public List<Part> Parts { get; set; } = new List<Part>();
        public Credits Credits { get; set; } = new Credits();
    }
}