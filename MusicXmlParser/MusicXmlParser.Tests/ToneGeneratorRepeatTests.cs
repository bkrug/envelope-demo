using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
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
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(parsedMusic);

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