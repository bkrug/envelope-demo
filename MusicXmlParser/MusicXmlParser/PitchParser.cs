using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicXmlParser
{
    public static class PitchParser
    {
        private static readonly ReadOnlyDictionary<string, int> _notesWithinOctave =
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
        private static readonly ReadOnlyDictionary<int, string> _alterString =
            new ReadOnlyDictionary<int, string>(
                new Dictionary<int, string>
                {
                    { -1, "b" },
                    { 0, string.Empty },
                    { 1, "s" }
                }
            );

        internal static bool TryParse(Note givenNote, out string pitchParsed)
        {
            pitchParsed = default;
            if (givenNote.IsRest)
            {
                pitchParsed = nameof(Pitch.REST);
                return true;
            }
            if (!int.TryParse(givenNote.Octave, out var musicXmlOctave)
                || !int.TryParse(givenNote.Alter, out var alterInt) && !string.IsNullOrEmpty(givenNote.Alter)
                || !_notesWithinOctave.ContainsKey(givenNote.Step.ToUpper()))
            {
                return false;
            }

            if (string.IsNullOrEmpty(givenNote.Alter))
                alterInt = 0;

            pitchParsed = givenNote.Step + _alterString[alterInt] + (musicXmlOctave - 2);
            if (Enum.GetNames(typeof(Pitch)).Contains(pitchParsed))
                return true;

            Pitch parsedEnumValue = GetEnumValue(givenNote, musicXmlOctave, alterInt);
            pitchParsed = parsedEnumValue < 0 || parsedEnumValue > Pitch.F6 ? pitchParsed : parsedEnumValue.ToString();
            return true;
        }

        private static Pitch GetEnumValue(Note givenNote, int musicXmlOctave, int alterInt)
        {
            const int notesPerOctave = 12;
            const int adjustToSN76489_octaves = 2 * notesPerOctave;
            const int notesInOctave0 = 3;
            var sn76489Octave = (musicXmlOctave - 1) * notesPerOctave - adjustToSN76489_octaves + notesInOctave0;
            var withinOctaveInt = _notesWithinOctave[givenNote.Step.ToUpper()] + alterInt;
            return (Pitch)(sn76489Octave + withinOctaveInt);
        }
    }
}
