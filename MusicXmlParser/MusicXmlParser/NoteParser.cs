﻿using MusicXmlParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("MusicXmlParser.Tests")]
namespace MusicXmlParser
{
    internal class NoteParser
    {
        internal ParsedMusic Parse(string sourceXml)
        {
            var document = XDocument.Parse(sourceXml);
            return Parse(document);
        }

        internal ParsedMusic Parse(XDocument document)
        {
            return new ParsedMusic
            {
                Divisions = GetDivision(document),
                Parts = GetMusicalParts(document),
                Credits = GetCredits(document)
            };
        }

        private static string GetDivision(XDocument document)
        {
            var division = document.Root.Descendants("divisions");
            return division.FirstOrDefault()?.Value;
        }

        private static Credits GetCredits(XDocument document)
        {
            var work = document.Root.Descendants("work");
            var identification = document.Root.Descendants("identification");
            var credits = new Credits
            {
                WorkTitle = work?.Descendants("work-title").FirstOrDefault()?.Value,
                Creator = identification?.Descendants("creator").FirstOrDefault()?.Value,
                Source = identification?.Descendants("source").FirstOrDefault()?.Value
            };
            return credits;
        }

        private static List<Part> GetMusicalParts(XDocument document)
        {
            var parts = document.Root.Descendants("part");
            var newParts = new List<Part>();
            foreach (var partElem in parts)
            {
                var measures = partElem.Descendants("measure");
                var newPart = new Part();
                var measureNumber = 0;
                foreach (var measureElem in measures)
                {
                    ++measureNumber;
                    var (endingElem, voltaNumber) = GetEnding(measureElem);
                    newPart.Measures.Add(new Measure
                    {
                        HasBackwardRepeat = HasRepeat(measureElem, "backward"),
                        HasForwardRepeat = HasRepeat(measureElem, "forward"),
                        HasVoltaBracket = endingElem != null,
                        VoltaNumber = voltaNumber,
                        Voices = CreateVoicesWithinMeasure(measureElem, measureNumber)
                    });
                }
                newParts.Add(newPart);
            }

            return newParts;
        }

        private static (XElement, int) GetEnding(XElement measureElem)
        {
            var elem = measureElem.Elements("barline")
                .Select(e => e.Element("ending"))
                .FirstOrDefault();
            int.TryParse(elem?.Attribute("number")?.Value, out var voltaNumber);
            return (elem, voltaNumber);
        }

        private static bool HasRepeat(XElement measureElem, string direction)
        {
            return measureElem.Elements("barline")
                .Select(e => e.Element("repeat")?.Attribute("direction")?.Value)
                .Any(foundDirection => string.Equals(foundDirection, direction, StringComparison.OrdinalIgnoreCase));
        }

        private static Dictionary<string, Voice> CreateVoicesWithinMeasure(XElement measureElem, int measureNumber)
        {
            var voices = new Dictionary<string, Voice>();
            foreach (var noteElem in measureElem.Descendants("note"))
            {
                var voiceLabel = noteElem.Element("voice").Value;
                var pitchElem = noteElem.Element("pitch");
                var isChord = noteElem.Element("chord") != null;
                if (!voices.ContainsKey(voiceLabel))
                {
                    voices.Add(voiceLabel, new Voice());
                }

                var note = CreateNote(noteElem, pitchElem, measureNumber);
                if (isChord)
                {
                    voices[voiceLabel].Chords.Last().Notes.Add(note);
                }
                else
                {
                    voices[voiceLabel].Chords.Add(new Chord
                    {
                        Notes = new List<Note>
                        {
                            note
                        }
                    });
                }
            }

            return voices;
        }

        private static Note CreateNote(XElement noteElem, XElement pitchElem, int measureNumber)
        {
            var note = new Note
            {
                Octave = pitchElem?.Element("octave")?.Value ?? string.Empty,
                Alter = pitchElem?.Element("alter")?.Value ?? string.Empty,
                Step = pitchElem?.Element("step")?.Value ?? string.Empty,
                Type = noteElem.Element("type")?.Value ?? string.Empty,
                Duration = noteElem.Element("duration")?.Value ?? string.Empty,
                IsDotted = noteElem.Elements("dot").Any(),
                IsTripplet = IsTripplet(noteElem),
                IsRest = noteElem?.Element("rest") != null,
                IsGraceNote = IsTrue(noteElem?.Element("grace")?.Attribute("slash")?.Value)
            };
            if (!note.IsRest && string.IsNullOrEmpty(note.Step))
                throw new Exception($"Note in measure {measureNumber} missing a 'step' tag.");
            return note;
        }

        private static bool IsTrue(string text)
        {
            text ??= string.Empty;
            return bool.TryParse(text, out var parseResult)
                ? parseResult
                : text.Equals("yes", StringComparison.OrdinalIgnoreCase) || text.Equals("on", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsTripplet(XElement noteElem)
        {
            var timeModElem = noteElem.Element("time-modification");
            var normalNotesValue = timeModElem?.Element("normal-notes")?.Value;
            var actualNotesValue = timeModElem?.Element("actual-notes")?.Value;
            return normalNotesValue == "2" && actualNotesValue == "3";
        }
    }
}