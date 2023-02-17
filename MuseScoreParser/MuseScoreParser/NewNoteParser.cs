﻿using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("MusicXmlParser.Tests")]
namespace MuseScoreParser
{
    internal class NewNoteParser
    {
        internal List<NewPart> Parse(string sourceXml)
        {
            var xml = XDocument.Parse(sourceXml);
            var parts = xml.Root.Descendants("part");
            var newParts = new List<NewPart>();
            foreach (var partElem in parts)
            {
                var measures = partElem.Descendants("measure") ?? new List<XElement>();
                var newPart = new NewPart();
                foreach (var measureElem in measures)
                {
                    var voices = new Dictionary<string, NewVoice>();
                    foreach (var noteElem in measureElem.Descendants("note"))
                    {
                        var voiceLabel = noteElem.Element("voice").Value;
                        var pitchElem = noteElem.Element("pitch");
                        if (!voices.ContainsKey(voiceLabel))
                        {
                            voices.Add(voiceLabel, new NewVoice());
                        }
                        voices[voiceLabel].Chords.Add(new NewChord
                        {
                            Notes = new List<NewNote>
                            {
                                new NewNote
                                {
                                    Octave = pitchElem.Element("octave")?.Value ?? string.Empty,
                                    Alter = pitchElem.Element("alter")?.Value ?? string.Empty,
                                    Step = pitchElem.Element("step")?.Value ?? string.Empty,
                                    Duration = noteElem.Element("duration")?.Value ?? string.Empty
                                }
                            }
                        });
                    }
                    newPart.Measures.Add(new NewMeasure
                    {
                        Voices = voices
                    });
                }
                newParts.Add(newPart);
            }
            return newParts;
        }
    }
}