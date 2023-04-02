using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    //A repetition type of "Default" means that the song will not repeat endlessly.
    //Most songs will stop when they reach the end.
    //Some songs end in a backward-repeat bar, end thus get repeated one time.
    //Other repetition types are more self exclamatory.
    public class ToneGeneratorRepeatTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        
        private SN76489NoteGenerator GetGenerator()
        {
            return new SN76489NoteGenerator(_logger.Object);
        }

        [Test]
        [TestCase(RepetitionType.RepeatFromBeginning)]
        //It is strange to use 'RepeatFromFirstJump' in this second scenario,
        //but if it is used, I guess we have to repeat the whole song.
        [TestCase(RepetitionType.RepeatFromFirstJump)]
        public void ToneGenerator_NoRepeatsInSource_RepeatFromBeginningForever(RepetitionType repetitionType)
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "SYM1"),
                        GetGeneratorNote(2, null, "SYM1A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "SYM1A", "SYM1" ),
                        ( "REPEAT", "REPT1" )
                    }
                },
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "SYM2"),
                        GetGeneratorNote(2, null, "SYM2A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "SYM2A", "SYM2" ),
                        ( "REPEAT", "REPT2" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = repetitionType
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "SYM", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_OnlyOneBackwardRepeat_RepeatFromBeginningOnce()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, null, "LBL1A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "LBL1A", "LBL1" ),
                        ( "REPEAT", "STOP" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.StopAtEnd
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "LBL", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_NoRepeats_PlayTheSongStraightThrough()
        {
            var parsedMusic = new ParsedMusic
            {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, null, "LBL1A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "LBL1A", "STOP" ),
                        ( "REPEAT", "STOP" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.StopAtEnd
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "LBL", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_OnlyOneBackwardRepeat_RepeatFromBeginningForever()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, null, "LBL1A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "LBL1A", "LBL1" ),
                        ( "REPEAT", "REPT1" )
                    }
                },
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "LBL2"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, null, "LBL2A")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "LBL2A", "LBL2" ),
                        ( "REPEAT", "REPT2" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.RepeatFromBeginning
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "LBL", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasureOnce()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24", 
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "MUSC1"),
                        GetGeneratorNote(2, "MUSC1A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, null, "MUSC1B")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "MUSC1B", "MUSC1A" ),
                        ( "REPEAT", "STOP" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.StopAtEnd
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "MUSC", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        //This is the scenario that 'RepeatFromFirstJump' is designed for.
        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasureForever()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "MUSC1"),
                        GetGeneratorNote(2, "MUSC1A"),
                        GetGeneratorNote(3, null, "MUSC1B")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "MUSC1B", "MUSC1A" ),
                        ( "REPEAT", "REPT1" )
                    }
                },
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "MUSC2"),
                        GetGeneratorNote(2, "MUSC2A"),
                        GetGeneratorNote(3, null, "MUSC2B")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "MUSC2B", "MUSC2A" ),
                        ( "REPEAT", "REPT2" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.RepeatFromFirstJump
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "MUSC", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_RepeatBarsInMiddleOfSong_WholeSongForever()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "OPRA1"),
                        GetGeneratorNote(2, "OPRA1A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, "OPRA1B", "OPRA1C")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "OPRA1B", "OPRA1A" ),
                        ( "OPRA1C", "OPRA1" ),
                        ( "REPEAT", "REPT1" )
                    }
                },
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "OPRA2"),
                        GetGeneratorNote(2, "OPRA2A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, "OPRA2B", "OPRA2C")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "OPRA2B", "OPRA2A" ),
                        ( "OPRA2C", "OPRA2" ),
                        ( "REPEAT", "REPT2" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.RepeatFromBeginning
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "OPRA", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        //This isn't really what 'RepeatFromFirstJump' is designed for either.
        //But if someone decides to use it here, this is the best result.
        //See also ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasureForever()
        [Test]
        public void ToneGenerator_RepeatBarsInMiddleOfSong_EverythingExceptIntroForever()
        {
            var parsedMusic = new ParsedMusic
            {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "OPRA1"),
                        GetGeneratorNote(2, "OPRA1A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, "OPRA1B", "OPRA1C")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "OPRA1B", "OPRA1A" ),
                        ( "OPRA1C", "OPRA1A" ),
                        ( "REPEAT", "REPT1" )
                    }
                },
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "OPRA2"),
                        GetGeneratorNote(2, "OPRA2A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, "OPRA2B", "OPRA2C")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "OPRA2B", "OPRA2A" ),
                        ( "OPRA2C", "OPRA2A" ),
                        ( "REPEAT", "REPT2" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.RepeatFromFirstJump
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "OPRA", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_TwoVoltaBrackets_OnSecondPlayThroughSkipFirstVoltaBracket()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            //Measure 1
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            //Measure 3
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 1,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            //Measure 5
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 2,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            //Measure 8
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 3,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "SOUN1"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, "SOUN1A"),
                        GetGeneratorNote(4),
                        GetGeneratorNote(5, "SOUN1B"),
                        GetGeneratorNote(6),
                        GetGeneratorNote(7),
                        GetGeneratorNote(8, "SOUN1C"),
                        GetGeneratorNote(9, null, "SOUN1D")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "SOUN1B", "SOUN1" ),    //Play through first volta bracket and return to beginning
                        ( "SOUN1A", "SOUN1B" ),   //When reaching the first volta bracket again, skip it and go to second volta bracket
                        ( "SOUN1C", "SOUN1" ),    //Finish second volta bracket and return to beginning
                        ( "SOUN1A", "SOUN1C" ),   //When reaching the first volta bracket again, skip it and go to third (final) volta bracket
                        ( "SOUN1D", "STOP" ),
                        ( "REPEAT", "STOP" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.StopAtEnd
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "SOUND", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_VoltaBracketFollowedByForwardRepeat_PlayEachVoltaBracketThenPlayRepeatTwice()
        {
            var parsedMusic = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            //Measure 1
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            },
                            //Measure 2
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 1,
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            //Measure 3
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 2,
                                Voices = GetParsedVoice()
                            },
                            //Measure 4
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedVoice()
                            },
                            //Measure 6
                            new Measure
                            {
                                Voices = GetParsedVoice()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        GetGeneratorNote(1, "TUNE1"),
                        GetGeneratorNote(2, "TUNE1A"),
                        GetGeneratorNote(3, "TUNE1B"),
                        GetGeneratorNote(4, "TUNE1C"),
                        GetGeneratorNote(5),
                        GetGeneratorNote(6, "TUNE1D", "TUNE1E")
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "TUNE1B", "TUNE1" ),  //Play through first volta bracket and return to beginning
                        ( "TUNE1A", "TUNE1B" ), //When reaching the frist volta bracket again, skip it and go to second (final) volta bracket
                        ( "TUNE1D", "TUNE1C" ), //When reaching backward repeat, jump to forward repeat, and play that section a second time
                        ( "TUNE1E", "STOP" ),
                        ( "REPEAT", "STOP" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.StopAtEnd
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "TUNE", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_LabelsRetainedWhenRestMerged()
        {
            var parsedMusic = new ParsedMusic
            {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = GetParsedMeasureEndingInRest()
                            },
                            new Measure
                            {
                                Voices = GetParsedMeasureOfRests()
                            },
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = GetParsedMeasureOfRests()
                            },
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = GetParsedMeasureOfRests()
                            }
                        }
                    }
                }
            };
            var expectedGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = new List<GeneratorNote> {
                        new GeneratorNote
                        {
                            StartMeasure = 1,
                            EndMeasure = 1,
                            Pitch = nameof(Pitch.C2),
                            Duration = Duration.N4,
                            Label = "MUSC1",
                            LabelAtEnd = null
                        },
                        new GeneratorNote
                        {
                            StartMeasure = 1,
                            EndMeasure = 2,
                            Pitch = nameof(Pitch.REST),
                            Duration = Duration.N2DOT,
                        },
                        new GeneratorNote
                        {
                            StartMeasure = 3,
                            EndMeasure = 4,
                            Pitch = nameof(Pitch.REST),
                            Duration = Duration.N1,
                            Label = "MUSC1A",
                            LabelAtEnd = "MUSC1B"
                        }
                    },
                    RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                    {
                        ( "MUSC1B", "MUSC1A" ),
                        ( "REPEAT", "REPT1" )
                    }
                }
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.RepeatFromFirstJump
            };

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(parsedMusic, "MUSC", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        private static Dictionary<string, Voice> GetParsedVoice()
        {
            return new Dictionary<string, Voice>
            {
                {
                    "v1p1",
                    new Voice
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
                                        Type = "whole",
                                        Duration = "96"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private static Dictionary<string, Voice> GetParsedMeasureEndingInRest()
        {
            return new Dictionary<string, Voice>
            {
                {
                    "v1p1",
                    new Voice
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
                                        Type = "quarter",
                                        IsDotted = true,
                                        Duration = "24"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private static Dictionary<string, Voice> GetParsedMeasureOfRests()
        {
            return new Dictionary<string, Voice>
            {
                {
                    "v1p1",
                    new Voice
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
                                        Type = "quarter",
                                        Duration = "24"
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private static GeneratorNote GetGeneratorNote(int measure, string preceedingLabel = null, string succeedingLabel = null)
        {
            return new GeneratorNote
            {
                StartMeasure = measure,
                EndMeasure = measure,
                Pitch = nameof(Pitch.C2),
                Duration = Duration.N1,
                Label = preceedingLabel,
                LabelAtEnd = succeedingLabel
            };
        }
    }
}