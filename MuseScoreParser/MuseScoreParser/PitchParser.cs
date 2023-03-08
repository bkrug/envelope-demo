using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MuseScoreParser
{
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
            if (!int.TryParse(givenNote.Octave, out var musicXmlOctave)
                || !int.TryParse(givenNote.Alter, out var alterInt) && !string.IsNullOrEmpty(givenNote.Alter)
                || !_notesWithinOctave.ContainsKey(givenNote.Step))
            {
                return false;
            }

            if (string.IsNullOrEmpty(givenNote.Alter))
                alterInt = 0;

            const int notesPerOctave = 12;
            const int adjustToSN76489_octaves = 2 * notesPerOctave;
            const int notesInOctave0 = 3;
            var sn76489Octave = (musicXmlOctave - 1) * notesPerOctave - adjustToSN76489_octaves + notesInOctave0;
            var withinOctaveInt = _notesWithinOctave[givenNote.Step.ToUpper()] + alterInt;
            pitchParsed = (Pitch)(sn76489Octave + withinOctaveInt);
            return true;
        }
    }
}
