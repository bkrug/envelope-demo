using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MuseScoreParser.Models
{
    internal class NewPart
    {
        public List<NewMeasure> Measures { get; set; }
    }

    internal class NewMeasure
    {
        public Dictionary<string, NewVoice> Voices { get; set; }
    }

    internal class NewVoice
    {
        public List<NewChord> Chords { get; set; }
    }

    internal class NewChord
    {
        public List<NewNote> Notes { get; set; }
    }

    internal class NewNote
    {
        public string Step { get; set; }
        public string Alter { get; set; }
        public string Octave { get; set; }
        public string Duration { get; set; }
    }
}
