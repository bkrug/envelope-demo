namespace MusicXmlParser.Models
{
    class Options
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string AsmLabel { get; set; }
        public string Ratio60Hz { get; set; }
        public string Ratio50Hz { get; set; }
        public string ShortLabel => AsmLabel.Length > 4 ? AsmLabel.Substring(0, 4) : AsmLabel;
        public string Label6Char => (AsmLabel + "      ").Substring(0, 6);
    }
}
