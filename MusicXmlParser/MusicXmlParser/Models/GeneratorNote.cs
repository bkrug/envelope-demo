using MusicXmlParser.Enums;
using System;
using System.Linq;
using PitchEnum = MusicXmlParser.Enums.Pitch;

namespace MusicXmlParser.Models
{
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