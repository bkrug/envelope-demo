﻿using MuseScoreParser.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.Tests
{
    public class ToneGeneratorGroupingTests
    {
        //TODO: Add failure case when measure counts don't match

        [Test]
        public void GroupByGenerator_OnePartOneVoiceNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_TwoPartsNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_TwoVoicesNoChords_Success()
        {

        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_NoneEmpty_OneVoiceIsIgnored()
        {
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_DifferentVoicesAreAllRestsInDifferentMeasures_SkipAllRestVoicesForGivenMeasure()
        {
        }

        [Test]
        public void GroupByGenerator_OneVoicesWithChords_IncludeLowerNotesOfChordsInSecondAndThirdGenerators()
        {
        }

        [Test]
        public void GroupByGenerator_TwoVoicesWithChords_IncludeLowerNotesOfChordsInThirdGenerator()
        {
        }

        [Test]
        public void GroupByGenerator_ThreeVoicesWithChords_IgnoreLowerNotesOfChords()
        {
        }
    }

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
                                Duration = "6"
                            }
                        }
                    },
                    new NewChord
                    {
                        Notes = new List<NewNote>
                        {
                            new NewNote
                            {
                                Step = "F",
                                Alter = "1",
                                Octave = "3",
                                Duration = "12"
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
                                Duration = "6"
                            },
                            new NewNote
                            {
                                Step = "C",
                                Alter = string.Empty,
                                Octave = "2",
                                Duration = "6"
                            },
                            new NewNote
                            {
                                Step = "E",
                                Alter = "1",
                                Octave = "2",
                                Duration = "6"
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
                                Duration = "12"
                            },
                            new NewNote
                            {
                                Step = "D",
                                Alter = string.Empty,
                                Octave = "2",
                                Duration = "12"
                            },
                            new NewNote
                            {
                                Step = "F",
                                Alter = "1",
                                Octave = "2",
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
                                Duration = "6"
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
                                Duration = "12"
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
            Assert.IsTrue(_voices.All(v => v.Value.Count == measureCount), "All voices must have the same number of measures");
            foreach (var partAndVoices in _voices.Keys.GroupBy(k => k.part))
            {
                var part = new NewPart();
                foreach (var partAndVoice in partAndVoices)
                {
                    for(var m = 0; m <= measureCount; ++m)
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
                }
                parts.Add(part);
            }
            return parts;
        }
    }
}