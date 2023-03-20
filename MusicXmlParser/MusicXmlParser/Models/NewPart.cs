using MusicXmlParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using PitchEnum = MusicXmlParser.Enums.Pitch;

namespace MusicXmlParser.Models
{
    internal class ParsedMusic
    {
        public List<NewPart> Parts { get; set; } = new List<NewPart>();
        public Credits Credits { get; set; } = new Credits();
    }

    internal class NewPart
    {
        public List<NewMeasure> Measures { get; set; } = new List<NewMeasure>();
    }

    internal class NewMeasure
    {
        public bool HasVoltaBracket { get; set; }
        public int VoltaNumber { get; set; }
        public bool HasBackwardRepeat { get; set; }
        public bool HasForwardRepeat { get; set; }
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
        public bool IsGraceSlash { get; set; }
        public string Type { get; set; }
    }

    internal class ToneGenerator
    {
        public List<(string FromThisLabel, string JumpToThisLabel)> RepeatLabels { get; set; } = new List<(string FromThisLabel, string JumpToThisLabel)>();
        public List<GeneratorNote> GeneratorNotes { get; set; } = new List<GeneratorNote>();
    }

    internal class GeneratorNote
    {
        public string Pitch { get; set; }
        public Duration Duration { get; set; }
        public int StartMeasure { get; set; }
        public int EndMeasure { get; set; }
        public string Label { get; set; }

        /// <summary>
        /// This property should only really be populated for the last note in a song, if that.
        /// </summary>
        public string LabelAtEnd { get; set; }

        public bool IsPitchValid => Enum.GetNames(typeof(PitchEnum)).Contains(Pitch);
    }
}