using MuseScoreParser.Models;
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
            foreach (var part in parts)
            {
                var measures = part.Descendants("measure") ?? new List<XElement>();
                var newPart = new NewPart();
                foreach (var measure in measures)
                {
                    var voices = new Dictionary<string, NewVoice>();
                    foreach (var xmlNote in measure.Descendants("note"))
                    {
                        var voiceLabel = xmlNote.Element("voice").Value;
                        var pitchElement = xmlNote.Element("pitch");
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
                                    Octave = pitchElement.Element("octave")?.Value ?? string.Empty,
                                    Alter = pitchElement.Element("alter")?.Value ?? string.Empty,
                                    Step = pitchElement.Element("step")?.Value ?? string.Empty,
                                    Duration = xmlNote.Element("duration")?.Value ?? string.Empty
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