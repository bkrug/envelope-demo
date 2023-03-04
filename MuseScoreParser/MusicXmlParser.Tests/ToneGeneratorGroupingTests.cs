using FluentAssertions;
using MuseScoreParser;
using MuseScoreParser.Enums;
using MuseScoreParser.Models;
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
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoPartsNoChords_Success()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoVoicesNoChords_Success()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                .AddMeasureOfOneNoteChords("p1", "v2")
                .AddMeasureOfOneNoteChords("p1", "v2")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_NoneEmpty_OneVoiceIsIgnored()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                .AddMeasureOfOneNoteChords("p1", "v2")
                .AddPartAndVoice("p1", "v3")
                .AddMeasureOfOneNoteChords("p1", "v3")
                .AddPartAndVoice("p1", "v4")
                .AddMeasureOfOneNoteChords("p1", "v4")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasureOfGeneratorNotes(1)
                },
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasureOfGeneratorNotes(1)
                },
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasureOfGeneratorNotes(1)
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_DifferentVoicesAreAllRestsInDifferentMeasures_SkipAllRestVoicesForGivenMeasure()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                .AddPartAndVoice("p1", "v3")
                .AddPartAndVoice("p1", "v4")
                //Measure 1
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfRests("p1", "v2")
                .AddMeasureOfOneNoteChords("p1", "v3")
                .AddMeasureOfOneNoteChords("p1", "v4")
                //Measure 2
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v2")
                .AddMeasureOfRests("p1", "v3")
                .AddMeasureOfOneNoteChords("p1", "v4")
                //
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FirstMeasureHasOneVoice_SecondMeasureHasTwoVoices_TwoToneGeneratorsWithEqualMeasuresResult()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddPartAndVoice("p1", "v2")
                //Measure 1
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfRests("p1", "v2")
                //Measure 2
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v2")
                //
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfGeneratorNotes(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasureOfRests(1)
                        .Concat(GetMeasureOfGeneratorNotes(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_OneVoicesWithChords_IncludeLowerNotesOfChordsInSecondAndThirdGenerators()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfThreeNoteChords("p1", "v1")
                .AddMeasureOfThreeNoteChords("p1", "v1")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasurePart1OfChord(1)
                        .Concat(GetMeasurePart1OfChord(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasurePart2OfChord(1)
                        .Concat(GetMeasurePart2OfChord(2))
                        .ToList()
                },
                new ToneGenerator
                {
                    GeneratorNotes =
                        GetMeasurePart3OfChord(1)
                        .Concat(GetMeasurePart3OfChord(2))
                        .ToList()
                }
            };

            //Act
            var actualToneGenerators = new ToneGeneratorGrouper().GetToneGenerators(singlePartSingleVoice);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoVoicesWithChords_IncludeLowerNotesOfChordsInThirdGenerator()
        {
        }

        [Test]
        public void GroupByGenerator_ThreeVoicesWithChords_IgnoreLowerNotesOfChords()
        {
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
                    Pitch = Pitch.Gb3
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N4,
                    Pitch = Pitch.Ds3
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
                    Duration = Duration.N16,
                    Pitch = Pitch.A2
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = Pitch.B2
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
                    Duration = Duration.N16,
                    Pitch = Pitch.C2
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = Pitch.D2
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
                    Duration = Duration.N16,
                    Pitch = Pitch.Eb2
                },
                new GeneratorNote
                {
                    StartMeasure = measureNumber,
                    EndMeasure = measureNumber,
                    Duration = Duration.N8,
                    Pitch = Pitch.Fs2
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
                    Pitch = Pitch.REST
                }
            };
        }
    }
}