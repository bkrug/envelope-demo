namespace MusicXmlParser.Models
{
    interface INote : IAsmSymbol
    {
        int Duration { get; set; }
        int Voice { get; set; }
        bool IsInChord { get; set; }
    }
}