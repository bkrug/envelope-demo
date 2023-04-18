using MusicXmlParser.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.Tests
{
    class PartBuilder
    {
        private Dictionary<(string part, string voice), List<Voice>> _voices = new Dictionary<(string part, string voice), List<Voice>>();

        public PartBuilder AddPartAndVoice(string part, string voice)
        {
            _voices.Add((part, voice), new List<Voice>());
            return this;
        }

        public PartBuilder AddMeasureOfOneNoteChords(string part, string voice)
        {
            _voices[(part, voice)].Add(new Voice
            {
                Chords = new List<Chord>
                {
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "G",
                                Alter = "-1",
                                Octave = "5",
                                Type = "eighth",
                                Duration = "12"
                            }
                        }
                    },
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "D",
                                Alter = "1",
                                Octave = "5",
                                Type = "quarter",
                                Duration = "24"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureOfThreeNoteChords(string part, string voice)
        {
            _voices[(part, voice)].Add(new Voice
            {
                Chords = new List<Chord>
                {
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "A",
                                Alter = string.Empty,
                                Octave = "4",
                                Type = "quarter",
                                Duration = "24"
                            },
                            new Note
                            {
                                Step = "C",
                                Alter = string.Empty,
                                Octave = "4",
                                Type = "quarter",
                                Duration = "24"
                            },
                            new Note
                            {
                                Step = "E",
                                Alter = "-1",
                                Octave = "4",
                                Type = "quarter",
                                Duration = "24"
                            }
                        }
                    },
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "B",
                                Alter = string.Empty,
                                Octave = "4",
                                Type = "eighth",
                                Duration = "12"
                            },
                            new Note
                            {
                                Step = "D",
                                Alter = string.Empty,
                                Octave = "4",
                                Type = "eighth",
                                Duration = "12"
                            },
                            new Note
                            {
                                Step = "F",
                                Alter = "1",
                                Octave = "4",
                                Type = "eighth",
                                Duration = "12"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureEndingInRest(string part, string voice)
        {
            _voices[(part, voice)].Add(new Voice
            {
                Chords = new List<Chord>
                {
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "C",
                                Octave = "4",
                                Type = "quarter",
                                Duration = "24"
                            }
                        }
                    },
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                IsRest = true,
                                Type = "eighth",
                                Duration = "12"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureStartingInRest(string part, string voice)
        {
            _voices[(part, voice)].Add(new Voice
            {
                Chords = new List<Chord>
                {
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                IsRest = true,
                                Type = "quarter",
                                Duration = "24"
                            }
                        }
                    },
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                Step = "C",
                                Octave = "4",
                                Type = "eighth",
                                Duration = "12"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public PartBuilder AddMeasureOfRests(string part, string voice)
        {
            _voices[(part, voice)].Add(new Voice
            {
                Chords = new List<Chord>
                {
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                IsRest = true,
                                Type = "quarter",
                                Duration = "24"
                            }
                        }
                    },
                    new Chord
                    {
                        Notes = new List<Note>
                        {
                            new Note
                            {
                                IsRest = true,
                                Type = "eighth",
                                Duration = "12"
                            }
                        }
                    }
                }
            });
            return this;
        }

        public ParsedMusic Build()
        {
            var parts = new List<Part>();
            var measureCount = _voices.First().Value.Count;
            Assert.That(_voices.All(v => v.Value.Count == measureCount), "All voices must have the same number of measures");
            foreach (var partAndVoices in _voices.Keys.GroupBy(k => k.part))
            {
                var part = new Part()
                {
                    Divisions = "24"
                };
                for (var m = 0; m < measureCount; ++m)
                {
                    part.Measures.Add(new Measure
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
            return new ParsedMusic
            {
                Parts = parts
            };
        }
    }
}