using System.Collections.Generic;

namespace MuseScoreParser.Models
{
    internal class NewPart
    {
        public List<NewMeasure> Measures { get; set; } = new List<NewMeasure>();
    }

    internal class NewMeasure
    {
        public Dictionary<string, NewVoice> Voices { get; set; } = new Dictionary<string, NewVoice>();
    }

    internal class NewVoice
    {
        public List<NewChord> Chords { get; set; } = new List<NewChord>();
    }

    internal class NewChord
    {
        public List<NewNote> Notes { get; set; } = new List<NewNote>();
    }

    internal class NewNote
    {
        public string Step { get; set; }
        public string Alter { get; set; }
        public string Octave { get; set; }
        public string Duration { get; set; }
    }
}
