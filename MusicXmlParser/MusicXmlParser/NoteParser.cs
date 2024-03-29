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
        private readonly ILogger _logger;

        internal NoteParser(ILogger logger)
        {
            _logger = logger;
        }

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

        private static string GetDivision(XElement partElem)
        {
            var division = partElem.Descendants("divisions");
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

        private List<Part> GetMusicalParts(XDocument document)
        {
            var parts = document.Root.Descendants("part");
            var newParts = new List<Part>();
            foreach (var partElem in parts)
            {
                var measures = partElem.Descendants("measure");
                var newPart = new Part()
                {
                    Divisions = GetDivision(partElem)
                };
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

        private Dictionary<string, Voice> CreateVoicesWithinMeasure(XElement measureElem, int measureNumber)
        {
            var voices = new Dictionary<string, Voice>();
            var currentTime = 0;
            var lengthOfRestToInsert = 0;
            var timesByVoice = new Dictionary<string, int>();
            foreach (var currentElem in measureElem.Descendants())
            {
                var noteElem = currentElem.Name.LocalName.Equals("note", StringComparison.OrdinalIgnoreCase) ? currentElem : null;
                var backupElem = currentElem.Name.LocalName.Equals("backup", StringComparison.OrdinalIgnoreCase) ? currentElem : null;

                if (noteElem != null)
                {
                    var voiceLabel = noteElem.Element("voice")?.Value;
                    var isChord = noteElem.Element("chord") != null;

                    if (string.IsNullOrWhiteSpace(voiceLabel))
                    {
                        _logger.WriteError($"Note in measure {measureNumber} missing a 'voice' tag.");
                        continue;
                    }

                    if (!voices.ContainsKey(voiceLabel))
                        voices.Add(voiceLabel, new Voice());

                    if (lengthOfRestToInsert > 0)
                    {
                        voices[voiceLabel].Chords.Add(GetNewRest(lengthOfRestToInsert));
                        currentTime = lengthOfRestToInsert;
                        lengthOfRestToInsert = 0;
                    }

                    var note = CreateNote(noteElem);
                    if (!isChord)
                    {
                        voices[voiceLabel].Chords.Add(GetNewChord(note));
                        if (int.TryParse(note.Duration, out var d))
                            currentTime += d;
                        timesByVoice[voiceLabel] = currentTime;
                    }
                    else
                    {
                        voices[voiceLabel].Chords.Last().Notes.Add(note);
                    }
                }
                else if (backupElem != null)
                {
                    if (int.TryParse(backupElem.Element("duration")?.Value, out var d))
                        currentTime -= d;

                    if (currentTime > 0)
                    {
                        lengthOfRestToInsert = currentTime;
                        currentTime = 0;
                    }
                }
            }

            AddRestsToEqualizeDurationOfVoices(voices, timesByVoice);

            return voices;
        }

        private static Chord GetNewChord(Note note)
        {
            return new Chord
            {
                Notes = new List<Note>
                {
                    note
                }
            };
        }

        private static Chord GetNewRest(int duration)
        {
            return new Chord
            {
                Notes = new List<Note>
                {
                    new Note
                    {
                        IsRest = true,
                        Duration = duration.ToString()
                    }
                }
            };
        }

        private static void AddRestsToEqualizeDurationOfVoices(Dictionary<string, Voice> voices, Dictionary<string, int> timesByVoice)
        {
            var lengthOfMeasure = timesByVoice.Any() ? timesByVoice.Max(kvp => kvp.Value) : 0;
            foreach (var timeByVoice in timesByVoice)
            {
                if (timeByVoice.Value < lengthOfMeasure)
                {
                    voices[timeByVoice.Key].Chords.Add(GetNewRest(lengthOfMeasure - timeByVoice.Value));
                }
            }
        }

        private Note CreateNote(XElement noteElem)
        {
            var pitchElem = noteElem.Element("pitch");
            var note = new Note
            {
                Octave = pitchElem?.Element("octave")?.Value ?? string.Empty,
                Alter = pitchElem?.Element("alter")?.Value ?? string.Empty,
                Step = pitchElem?.Element("step")?.Value ?? string.Empty,
                Duration = noteElem.Element("duration")?.Value ?? string.Empty,
                IsRest = noteElem?.Element("rest") != null,
                IsGraceNote = noteElem?.Element("grace") != null,
                Tie = GetTieType(noteElem)
            };
            return note;
        }

        private static Ties GetTieType(XElement noteElem)
        {
            var typeAttr = noteElem.Element("tie")?.Attribute("type").Value.ToLower();
            return typeAttr switch
            {
                "start" => Ties.Start,
                "end" => Ties.End,
                _ => Ties.None,
            };
        }
    }
}