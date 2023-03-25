using System.Collections.Generic;

namespace MusicXmlParser.Models
{
    internal class Measure
    {
        public bool HasVoltaBracket { get; set; }
        public int VoltaNumber { get; set; }
        public bool HasBackwardRepeat { get; set; }
        public bool HasForwardRepeat { get; set; }
        public Dictionary<string, Voice> Voices { get; set; } = new Dictionary<string, Voice>();
    }
}