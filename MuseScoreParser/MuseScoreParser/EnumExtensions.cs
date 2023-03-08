using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MuseScoreParser
{
    public static class DurationParser
    {
        private static ReadOnlyDictionary<string, Duration> _durations = new ReadOnlyDictionary<string, Duration>(
            new Dictionary<string, Duration>
            {
                { "whole", Duration.N1 },
                { "half", Duration.N2 },
                { "quarter", Duration.N4 },
                { "4th", Duration.N4 },
                { "eighth", Duration.N8 },
                { "8th", Duration.N8 },
                { "sixteenth", Duration.N16 },
                { "16th", Duration.N16 },
                { "thirtysecond", Duration.N32 },
                { "thirty-second", Duration.N32 },
                { "32nd", Duration.N32 },
            });

        internal static bool TryParse(NewNote noteWithDuration, out Duration durationParsed)
        {
            durationParsed = default;
            var lowerCase = noteWithDuration.Type.ToLower();

            if ((lowerCase == "64th" || lowerCase == "sixty-fourth" || lowerCase == "sixtyfourth") && noteWithDuration.IsTripplet)
            {
                durationParsed = Duration.N64TRP;
                return true;
            }
            if (!_durations.ContainsKey(lowerCase))
                return false;

            durationParsed = _durations[lowerCase];
            if (noteWithDuration.IsDotted)
            {
                if ((int)durationParsed % 2 != 0)
                    return false;
                durationParsed += (int)durationParsed / 2;
            }
            else if (noteWithDuration.IsTripplet)
            {
                if ((int)durationParsed * 2 % 3 != 0)
                    return false;
                durationParsed = (Duration)((int)durationParsed * 2 / 3);
            }
            return true;
        }
    }
}
