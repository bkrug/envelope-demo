using MusicXmlParser.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using PitchEnum = MusicXmlParser.Enums.Pitch;

namespace MusicXmlParser.Models
{
    internal class ParsedMusic
    {
        public string Divisions { get; set; }
        public List<Part> Parts { get; set; } = new List<Part>();
        public Credits Credits { get; set; } = new Credits();
    }

    internal class Part
    {
        public List<Measure> Measures { get; set; } = new List<Measure>();
    }

    internal class Measure
    {
        public bool HasVoltaBracket { get; set; }
        public int VoltaNumber { get; set; }
        public bool HasBackwardRepeat { get; set; }
        public bool HasForwardRepeat { get; set; }
        public Dictionary<string, Voice> Voices { get; set; } = new Dictionary<string, Voice>();
    }

    internal class Voice
    {
        public List<Chord> Chords { get; set; } = new List<Chord>();
    }

    internal class Chord
    {
        public List<Note> Notes { get; set; } = new List<Note>();
    }

    internal struct Note
    {
        public string Step { get; set; }
        public string Alter { get; set; }
        public string Octave { get; set; }
        public bool IsRest { get; set; }
        public bool IsDotted { get; set; }
        public bool IsTripplet { get; set; }
        public bool IsGraceNote { get; set; }
        public string Duration { get; set; }
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
        public bool IsGraceNote { get; set; }
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