using FluentAssertions;
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
        [Test]
        public void ToneGenerator_OnlyOneBackwardRepeat_RepeatFromBeginningOnce()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
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
                RepetitionType = RepetitionType.Default
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "LBL", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_OnlyOneBackwardRepeat_RepeatFromBeginningForever()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        }
                    }
                },
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
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
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "LBL", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasureOnce()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
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
                RepetitionType = RepetitionType.Default
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "MUSC", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasureForever()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        }
                    }
                },
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
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
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "MUSC", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_RepeatBarsInMiddleOfSong_WholeSongForever()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        }
                    }
                },
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
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
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "OPRA", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_TwoVoltaBrackets_OnSecondPlayThroughSkipFirstVoltaBracket()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        //Measure 1
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        //Measure 3
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 1,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        //Measure 5
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 2,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        //Measure 8
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 3,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
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
                        GetGeneratorNote(9)
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "SOUN1B", "SOUN1" ),    //Play through first volta bracket and return to beginning
                ( "SOUN1A", "SOUN1B" ),   //When reaching the first volta bracket again, skip it and go to second volta bracket
                ( "SOUN1C", "SOUN1" ),    //Finish second volta bracket and return to beginning
                ( "SOUN1A", "SOUN1C" ),   //When reaching the first volta bracket again, skip it and go to third (final) volta bracket
                ( "REPEAT", "STOP" )
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.Default
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "SOUND", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_VoltaBracketFollowedByForwardRepeat_PlayEachVoltaBracketThenPlayRepeatTwice()
        {
            var parsedMusic = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        //Measure 1
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
                        },
                        //Measure 2
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 1,
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        //Measure 3
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 2,
                            Voices = GetParsedVoice()
                        },
                        //Measure 4
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = GetParsedVoice()
                        },
                        //Measure 6
                        new NewMeasure
                        {
                            Voices = GetParsedVoice()
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
                        GetGeneratorNote(6, "TUNE1D")
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "TUNE1B", "TUNE1" ),  //Play through first volta bracket and return to beginning
                ( "TUNE1A", "TUNE1B" ), //When reaching the frist volta bracket again, skip it and go to second (final) volta bracket
                ( "TUNE1D", "TUNE1C" ), //When reaching backward repeat, jump to forward repeat, and play that section a second time
                ( "REPEAT", "STOP" )
            };
            var options = new Options
            {
                RepetitionType = RepetitionType.Default
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic, "TUNE", options);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        private static Dictionary<string, NewVoice> GetParsedVoice()
        {
            return new Dictionary<string, NewVoice>
            {
                {
                    "v1p1",
                    new NewVoice
                    {
                        Chords = new List<NewChord>
                        {
                            new NewChord
                            {
                                Notes = new List<NewNote>
                                {
                                    new NewNote
                                    {
                                        Step = "C",
                                        Octave = "4",
                                        Type = "whole"
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