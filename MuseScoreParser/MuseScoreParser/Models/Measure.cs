namespace MuseScoreParser.Models
{
    class Measure : IAsmSymbol
    {
        public int Number { get; set; }
        /// <summary>
        /// If measures had to be merged then "Number" represents the starting measure,
        /// and this property will be non-null
        /// </summary>
        public int? EndingNumber { get; set; }

        public Measure(int measureNumber = 0)
        {
            Number = measureNumber;
        }

        public string ToAsm()
        {
            return EndingNumber.HasValue
                ? $"* Measure {Number} - {EndingNumber}"
                : $"* Measure {Number}";
        }
    }
}