using System;
using DurationEnum = MuseScoreParser.Models.Duration;

namespace MuseScoreParser.Models
{
    class Note : INote
    {
        public string Key { 
            get { 
                return _key;
            }
            set { 
                _key = value;
                SetPitch();
            }
        }
        public int TiOctive {
            get
            {
                return _tiOctive;
            }
            set
            {
                _tiOctive = value;
                _xmlOctive = value + OCTIVE_DIFFERANCE;
                SetPitch();
            }
        }
        public int XmlOctive
        {
            get
            {
                return _xmlOctive;
            }
            set
            {
                _xmlOctive = value;
                _tiOctive = value - OCTIVE_DIFFERANCE;
                SetPitch();
            }
        }
        public Pitch? Pitch { get; private set; }
        public int Duration { get; set; }
        public int Voice { get; set; }
        public bool IsInChord { get; set; }
        public const int OCTIVE_DIFFERANCE = 2;

        private string _key;
        private int _tiOctive;
        private int _xmlOctive;
        private void SetPitch()
        {
            if (Enum.TryParse<Pitch>(Key + TiOctive, out var pitch))
                Pitch = pitch;
            else
                Pitch = null;
        }

        public string ToAsm()
        {
            var duration = Enum.IsDefined(typeof(DurationEnum), Duration) ? ((DurationEnum)Duration).ToString() : Duration.ToString();
            if (duration == string.Empty || duration == "0")
                return $"*      BYTE {Key + TiOctive},{duration}";
            else if (Pitch != null)
                return $"      BYTE {Key+TiOctive},{duration}";
            else
                return $"      BYTE {Models.Pitch.REST},{duration}      * Invalid: {Key+TiOctive}";
        }
    }
}