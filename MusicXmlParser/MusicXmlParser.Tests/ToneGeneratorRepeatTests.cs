using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    public class ToneGeneratorRepeatTests
    {
        [Test]
        public void ToneGenerator_OnlyBackwardRepeat_RepeatFromBeginning()
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
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "LBL1A", "LBL1" )
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedGenerators);
        }

        [Test]
        public void ToneGenerator_BackwardAndForwardRepeat_RepeatEverythingExceptEarliestMeasure()
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
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2, "LBL1A"),
                        GetGeneratorNote(3),
                        GetGeneratorNote(4, null, "LBL1B")
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "LBL1B", "LBL1A" )
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic);

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
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2),
                        GetGeneratorNote(3, "LBL1A"),
                        GetGeneratorNote(4),
                        GetGeneratorNote(5, "LBL1B"),
                        GetGeneratorNote(6),
                        GetGeneratorNote(7),
                        GetGeneratorNote(8, "LBL1C"),
                        GetGeneratorNote(9)
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "LBL1B", "LBL1" ),    //Play through first volta bracket and return to beginning
                ( "LBL1A", "LBL1B" ),   //When reaching the first volta bracket again, skip it and go to second volta bracket
                ( "LBL1C", "LBL1" ),    //Finish second volta bracket and return to beginning
                ( "LBL1A", "LBL1C" )    //When reaching the first volta bracket again, skip it and go to third (final) volta bracket
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic);

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
                        GetGeneratorNote(1, "LBL1"),
                        GetGeneratorNote(2, "LBL1A"),
                        GetGeneratorNote(3, "LBL1B"),
                        GetGeneratorNote(4, "LBL1C"),
                        GetGeneratorNote(5),
                        GetGeneratorNote(6, "LBL1D")
                    }
                }
            };
            expectedGenerators[0].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
            {
                ( "LBL1B", "LBL1" ),  //Play through first volta bracket and return to beginning
                ( "LBL1A", "LBL1B" ), //When reaching the frist volta bracket again, skip it and go to second (final) volta bracket
                ( "LBL1D", "LBL1C" ), //When reaching backward repeat, jump to forward repeat, and play that section a second time
            };

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(parsedMusic);

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
                Pitch = Pitch.C2,
                Duration = Duration.N1,
                Label = preceedingLabel,
                LabelAtEnd = succeedingLabel
            };
        }
    }
}