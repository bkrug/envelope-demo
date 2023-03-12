using MusicXmlParser.Enums;

namespace MusicXmlParser.Models
{
    class Rest : INote
    {
        public int Duration { get; set; }
        public int Voice { get; set; }
        public bool IsInChord { get; set; }

        public string ToAsm()
        {
            var duration = (Duration)Duration;
            return $"       BYTE {Pitch.REST},{duration}";
        }
    }
}