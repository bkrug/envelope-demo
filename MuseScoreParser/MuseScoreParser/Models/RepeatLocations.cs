using System;
using System.Collections.Generic;
using System.Text;

namespace MuseScoreParser.Models
{
    class RepeatLocations
    {
        internal string VoltaBracketOne { get; set; }
        internal string MostRecentForward { get; set; }
        internal List<string> Labels { get; set; } = new List<string>();
    }
}
