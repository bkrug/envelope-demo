using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class Part
    {
        public List<Measure> Measures { get; set; } = new List<Measure>();
    }
}