using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class Part
    {
        public string Divisions { get; set; }
        public List<Measure> Measures { get; set; } = new List<Measure>();
    }
}