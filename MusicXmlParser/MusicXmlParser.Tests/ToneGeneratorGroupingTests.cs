using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.Tests
{
    public class ToneGeneratorGroupingTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        private readonly Options _defaultOptions = new Options
        {
            RepetitionType = RepetitionType.StopAtEnd
        };

        private SN76489NoteGenerator GetGenerator()
        {
            return new SN76489NoteGenerator(_logger.Object);
        }

        [Test]
        public void GroupByGenerator_TwoVoicesWithChords_IncludeLowerNotesOfChordsInThirdGenerator()
        {
            var singlePartTwoVoices = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfThreeNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                .AddMeasureOfOneNoteChords("p1", "v2")
                .AddMeasureOfThreeNoteChords("p1", "v2")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasurePart1OfChord(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasurePart1OfChord(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasurePart2OfChord(1)
                        .Concat(GetMeasurePart2OfChord(2))
                        .ToList()
                }
            };
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_ThreeVoicesWithChords_IgnoreLowerNotesOfChords()
        {
            var totalOfThreeVoices = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfThreeNoteChords("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                .AddMeasureOfThreeNoteChords("p1", "v2")
                .AddPartAndVoice("p2", "v3")
                .AddMeasureOfThreeNoteChords("p2", "v3")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasurePart1OfChord(1)
                },
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasurePart1OfChord(1)
                },
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasurePart1OfChord(1)
                }
            };
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = GetGenerator().GetToneGenerators(totalOfThreeVoices, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoPartsDifferentPlayLengths_MessageIsLogged()
        {
            var singlePartTwoVoices = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .Build();
            //Each voice has the same measure count, but one has a shorter duration
            var voice3 = singlePartTwoVoices.Parts[1].Measures.Last().Voices["v3"];
            voice3.Chords.Remove(voice3.Chords.Last());

            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);

            //Act
            GetGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

            //Assert
            actualMessage.Should().Be($"All voices must have the same duration");
        }

        private static List<GeneratorNote> GetMeasureOfGeneratorNotes(int measureNumber)
        {
            return new List<GeneratorNote>
            {
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = nameof(Pitch.Gb3)
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N4,
                    Pitch = nameof(Pitch.Ds3)
                }
            };
        }

        private static List<GeneratorNote> GetMeasurePart1OfChord(int measureNumber)
        {
            return new List<GeneratorNote>
            {
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N4,
                    Pitch = nameof(Pitch.A2)
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = nameof(Pitch.B2)
                }
            };
        }

        private static List<GeneratorNote> GetMeasurePart2OfChord(int measureNumber)
        {
            return new List<GeneratorNote>
            {
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N4,
                    Pitch = nameof(Pitch.C2)
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = nameof(Pitch.D2)
                }
            };
        }

        private static List<GeneratorNote> GetMeasurePart3OfChord(int measureNumber)
        {
            return new List<GeneratorNote>
            {
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N4,
                    Pitch = nameof(Pitch.Eb2)
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = nameof(Pitch.Fs2)
                }
            };
        }

        private static List<GeneratorNote> GetMeasureOfRests(int measureNumber)
        {
            return new List<GeneratorNote>
            {
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = (Duration)((int)Duration.N4 + (int)Duration.N8),
                    Pitch = nameof(Pitch.REST)
                }
            };
        }

        private static void AddDetailsConsistentWithNonRepeatingSong(List<ToneGenerator> expectedToneGenerators)
        {
            for (var i = 1; i <= expectedToneGenerators.Count; ++i)
            {
                expectedToneGenerators[i - 1].GeneratorNotes.First().Label = "LBL" + i;
                expectedToneGenerators[i - 1].GeneratorNotes.Last().LabelAtEnd = "LBL" + i + "A";
                expectedToneGenerators[i - 1].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                {
                    ( "LBL" + i + "A", Symbols.STOP ),
                    ( "REPEAT", Symbols.STOP )
                };
            }
        }
    }
}