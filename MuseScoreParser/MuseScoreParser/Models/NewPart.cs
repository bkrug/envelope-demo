using MuseScoreParser.Enums;
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

    internal struct NewNote
    {
        public string Step { get; set; }
        public string Alter { get; set; }
        public string Octave { get; set; }
        public bool IsRest { get; set; }
        public bool IsDotted { get; set; }
        public bool IsTripplet { get; set; }
        public string Type { get; set; }
    }

    internal class ToneGenerator
    {
        public List<GeneratorNote> GeneratorNotes { get; set; } = new List<GeneratorNote>();
    }

    internal struct GeneratorNote
    {
        public Pitch Pitch { get; set; }
        public Duration Duration { get; set; }
        public int StartMeasure { get; set; }
        public int EndMeasure { get; set; }
    }
}