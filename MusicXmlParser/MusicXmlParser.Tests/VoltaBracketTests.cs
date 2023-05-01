using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    //Most tests in this suite are "big" unit tests; more than one class is under test.
    //That makes it relatively easy for other developers to refactor the code without having to edit many tests.
    //I think these volta bracket tests would be too hard to read if we made them "big" tests, so I'm leaving them "small".
    public class VoltaBracketTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        private SN76489NoteGenerator GetGenerator()
        {
            return new SN76489NoteGenerator(_logger.Object);
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

        [Test]
        public void VoltaBracket_TwoVoltaBrackets_OnSecondPlayThroughSkipFirstVoltaBracket()
        {
            var parsedMusic = new ParsedMusic
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Divisions = "24",
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
                        ( "SOUN1D", Symbols.STOP ),
                        ( "REPEAT", Symbols.STOP )
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
        public void VoltaBracket_VoltaBracketFollowedByForwardRepeat_PlayEachVoltaBracketThenPlayRepeatTwice()
        {
            var parsedMusic = new ParsedMusic
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Divisions = "24",
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
                        ( "TUNE1E", Symbols.STOP ),
                        ( "REPEAT", Symbols.STOP )
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
    }
}