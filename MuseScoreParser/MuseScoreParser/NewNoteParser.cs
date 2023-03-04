using MuseScoreParser.Models;
using System.Collections.Generic;
using System.Linq;
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
                var measures = partElem.Descendants("measure");
                var newPart = new NewPart();
                foreach (var measureElem in measures)
                {
                    newPart.Measures.Add(new NewMeasure
                    {
                        Voices = CreateVoicesWithinMeasure(measureElem)
                    });
                }
                newParts.Add(newPart);
            }
            return newParts;
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
                Octave = pitchElem.Element("octave")?.Value ?? string.Empty,
                Alter = pitchElem.Element("alter")?.Value ?? string.Empty,
                Step = pitchElem.Element("step")?.Value ?? string.Empty,
                Type = noteElem.Element("type")?.Value ?? string.Empty
            };
        }
    }
}