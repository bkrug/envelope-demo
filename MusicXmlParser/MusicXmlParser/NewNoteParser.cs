using MusicXmlParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("MusicXmlParser.Tests")]
namespace MusicXmlParser
{
    internal class NewNoteParser
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
                Parts = GetMusicalParts(document),
                Credits = GetCredits(document)
            };
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

        private static List<NewPart> GetMusicalParts(XDocument document)
        {
            var parts = document.Root.Descendants("part");
            var newParts = new List<NewPart>();
            foreach (var partElem in parts)
            {
                var measures = partElem.Descendants("measure");
                var newPart = new NewPart();
                foreach (var measureElem in measures)
                {
                    var (endingElem, voltaNumber) = GetEnding(measureElem);
                    newPart.Measures.Add(new NewMeasure
                    {
                        HasBackwardRepeat = HasRepeat(measureElem, "backward"),
                        HasForwardRepeat = HasRepeat(measureElem, "forward"),
                        HasVoltaBracket = endingElem != null,
                        VoltaNumber = voltaNumber,
                        Voices = CreateVoicesWithinMeasure(measureElem)
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

        private static Dictionary<string, NewVoice> CreateVoicesWithinMeasure(XElement measureElem)
        {
            var voices = new Dictionary<string, NewVoice>();
            foreach (var noteElem in measureElem.Descendants("note"))
            {
                var voiceLabel = noteElem.Element("voice").Value;
                var pitchElem = noteElem.Element("pitch");
                var isChord = noteElem.Element("chord") != null;
                if (!voices.ContainsKey(voiceLabel))
                {
                    voices.Add(voiceLabel, new NewVoice());
                }

                if (isChord)
                {
                    voices[voiceLabel].Chords.Last().Notes.Add(CreateNote(noteElem, pitchElem));
                }
                else
                {
                    voices[voiceLabel].Chords.Add(new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            CreateNote(noteElem, pitchElem)
                        }
                    });
                }
            }

            return voices;
        }

        private static NewNote CreateNote(XElement noteElem, XElement pitchElem)
        {
            return new NewNote
            {
                Octave = pitchElem?.Element("octave")?.Value ?? string.Empty,
                Alter = pitchElem?.Element("alter")?.Value ?? string.Empty,
                Step = pitchElem?.Element("step")?.Value ?? string.Empty,
                Type = noteElem.Element("type")?.Value ?? string.Empty,
                IsDotted = noteElem.Elements("dot").Any(),
                IsTripplet = IsTripplet(noteElem)
            };
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