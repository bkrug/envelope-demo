namespace MuseScoreParser.Models
{
    class Options
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string AsmLabel { get; set; }
        public string Ratio60Hz { get; set; }
        public string Ratio50Hz { get; set; }
        public string ShortLabel => AsmLabel.Substring(0, 4);
        public string Label6Char => (AsmLabel + "      ").Substring(0, 6);
    }
}
