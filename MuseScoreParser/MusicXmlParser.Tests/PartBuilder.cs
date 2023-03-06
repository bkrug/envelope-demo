using MuseScoreParser.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.Tests
{
    class PartBuilder
    {
        private Dictionary<(string part, string voice), List<NewVoice>> _voices = new Dictionary<(string part, string voice), List<NewVoice>>();

        public PartBuilder AddPartAndVoice(string part, string voice)
        {
            _voices.Add((part, voice), new List<NewVoice>());
            return this;
        }

        public PartBuilder AddMeasureOfOneNoteChords(string part, string voice)
        {
            _voices[(part, voice)].Add(new NewVoice
            {
                Chords = new List<NewChord>
                {
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                Step = "G",
                                Alter = "-1",
                                Octave = "3",
                                Type = "eighth"
                            }
                        }
                    },
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                Step = "D",
                                Alter = "1",
                                Octave = "3",
                                Type = "quarter"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureOfThreeNoteChords(string part, string voice)
        {
            _voices[(part, voice)].Add(new NewVoice
            {
                Chords = new List<NewChord>
                {
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                Step = "A",
                                Alter = string.Empty,
                                Octave = "2",
                                Type = "16th"
                            },
                            new NewNote
                            {
                                Step = "C",
                                Alter = string.Empty,
                                Octave = "2",
                                Type = "16th"
                            },
                            new NewNote
                            {
                                Step = "E",
                                Alter = "-1",
                                Octave = "2",
                                Type = "16th"
                            }
                        }
                    },
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                Step = "B",
                                Alter = string.Empty,
                                Octave = "2",
                                Type = "eighth"
                            },
                            new NewNote
                            {
                                Step = "D",
                                Alter = string.Empty,
                                Octave = "2",
                                Type = "eighth"
                            },
                            new NewNote
                            {
                                Step = "F",
                                Alter = "1",
                                Octave = "2",
                                Type = "eighth"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureOfRests(string part, string voice)
        {
            _voices[(part, voice)].Add(new NewVoice
            {
                Chords = new List<NewChord>
                {
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                IsRest = true,
                                Type = "quarter"
                            }
                        }
                    },
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                IsRest = true,
                                Type = "eighth"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public List<NewPart> Build()
        {
            var parts = new List<NewPart>();
            var measureCount = _voices.First().Value.Count;
            Assert.That(_voices.All(v => v.Value.Count == measureCount), "All voices must have the same number of measures");
            foreach (var partAndVoices in _voices.Keys.GroupBy(k => k.part))
            {
                var part = new NewPart();
                for (var m = 0; m < measureCount; ++m)
                {
                    part.Measures.Add(new NewMeasure
                    {
                        Voices = _voices
                            .Where(v => partAndVoices.Contains(v.Key))
                            .ToDictionary(
                                avftp => avftp.Key.voice,
                                avftp => avftp.Value[m]
                            )
                    });
                }
                parts.Add(part);
            }
            return parts;
        }
    }
}