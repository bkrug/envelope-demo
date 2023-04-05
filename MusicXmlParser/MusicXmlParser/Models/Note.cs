namespace MusicXmlParser.Models
{
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
        public Ties Tie { get; set; }
    }

    internal enum Ties
    {
        None, Start, End
    }
}