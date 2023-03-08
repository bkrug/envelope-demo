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

    public static class PitchParser
    {
        private static ReadOnlyDictionary<string, int> _notesWithinOctave =
            new ReadOnlyDictionary<string, int>(
                new Dictionary<string, int>
                {
                    { "C", 0 },
                    { "D", 2 },
                    { "E", 4 },
                    { "F", 5 },
                    { "G", 7 },
                    { "A", 9 },
                    { "B", 11 },
                }
            );

        internal static bool TryParse(NewNote givenNote, out Pitch pitchParsed)
        {
            pitchParsed = default;
            if (givenNote.IsRest)
            {
                pitchParsed = Pitch.REST;
                return true;
            }
            if (!int.TryParse(givenNote.Octave, out var musicXmlOctave))
            {
                return false;
            }

            const int notesPerOctave = 12;
            const int adjustToSN76489_octaves = 2 * notesPerOctave;
            const int notesInOctave0 = 3;
            var sn76489Octave = (musicXmlOctave - 1) * notesPerOctave - adjustToSN76489_octaves + notesInOctave0;
            var alterInt = string.IsNullOrWhiteSpace(givenNote.Alter) ? 0 : int.Parse(givenNote.Alter);
            var withinOctaveInt = _notesWithinOctave[givenNote.Step.ToUpper()] + alterInt;
            pitchParsed = (Pitch)(sn76489Octave + withinOctaveInt);
            return true;
        }
    }
}
