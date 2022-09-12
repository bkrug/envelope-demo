namespace MuseScoreParser.Models
{
    class Measure : IAsmSymbol
    {
        public int Number { get; set; }

        public Measure(int measureNumber = 0)
        {
            Number = measureNumber;
        }

        public string ToAsm()
        {
            return $"* Measure {Number}";
        }
    }
}