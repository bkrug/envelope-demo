namespace MuseScoreParser.Models
{
    class Label : IAsmSymbol
    {
        public string LabelName { get; set; }
        
        public string ToAsm()
        {
            return LabelName;
        }
    }
}