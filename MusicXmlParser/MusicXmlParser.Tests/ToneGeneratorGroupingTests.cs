using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.Tests
{
    public class ToneGeneratorGroupingTests
    {
        private readonly Options _defaultOptions = new Options
        {
            RepetitionType = RepetitionType.StopAtEnd
        };

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
                        .ToList(),
                }
            };
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartSingleVoice, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoPartsNoChords_Success()
        {
            var singlePartTwoVoices = new PartBuilder()
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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_TwoVoicesNoChords_Success()
        {
            var singlePartTwoVoices = new PartBuilder()
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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_NoneEmpty_OneVoiceIsIgnored()
        {
            var fourVoicesInTotal = new PartBuilder()
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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(fourVoicesInTotal, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FourVoicesNoChords_DifferentVoicesAreAllRestsInDifferentMeasures_SkipAllRestVoicesForGivenMeasure()
        {
            var fourVoicesInTotal = new PartBuilder()
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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(fourVoicesInTotal, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_FirstMeasureHasOneVoice_SecondMeasureHasTwoVoices_TwoToneGeneratorsWithEqualMeasuresResult()
        {
            var singlePartTwoVoices = new PartBuilder()
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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

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
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartSingleVoice, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
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
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions);

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
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(totalOfThreeVoices, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_RestsInConsecutiveMeasures_MergedTogether()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureEndingInRest("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureStartingInRest("p1", "v1")
                .Build();
            var expectedToneGenerators = new List<ToneGenerator>()
            {
                new ToneGenerator
                {
                    GeneratorNotes = GetMeasureOfGeneratorNotes(1)
                        .Concat(new List<GeneratorNote>
                        {
                            new GeneratorNote
                            {
                                StartMeasure = 2,
                                EndMeasure = 2,
                                Pitch = nameof(Pitch.C2),
                                Duration = Duration.N4
                            },
                            new GeneratorNote
                            {
                                StartMeasure = 2,
                                EndMeasure = 5,
                                Pitch = nameof(Pitch.REST),
                                Duration = ((int)Duration.N1 + Duration.N8)
                            },
                            new GeneratorNote
                            {
                                StartMeasure = 5,
                                EndMeasure = 5,
                                Pitch = nameof(Pitch.C2),
                                Duration = Duration.N8
                            }
                        }).ToList()
                }
            };
            AddDetailsConsistentWithNonRepeatingSong(expectedToneGenerators);

            //Act
            var actualToneGenerators = new SN76489NoteGenerator().GetToneGenerators(singlePartSingleVoice, "LBL", _defaultOptions);

            //Assert
            actualToneGenerators.Should().BeEquivalentTo(expectedToneGenerators);
        }

        [Test]
        public void GroupByGenerator_RestsInConsecutiveMeasures_NoMergedRestShouldBeLongerThan255InDuration()
        {
            var singlePartSingleVoice = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureEndingInRest("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureOfRests("p1", "v1")
                .AddMeasureStartingInRest("p1", "v1")
                .Build();
            var expectedDuration = (int)Duration.N8           //length of rest in first measure
                + ((int)Duration.N8 + (int)Duration.N4) * 9   //length of all the entirely-rest measures
                + (int)Duration.N4;                           //length of rest in last measure

            //Act
            var actualToneGenerator = new SN76489NoteGenerator().GetToneGenerators(singlePartSingleVoice, "LBL", _defaultOptions).Single();

            //Assert
            var notesWeHopeAreRests = actualToneGenerator.GeneratorNotes.Skip(1).Take(actualToneGenerator.GeneratorNotes.Count - 2).ToList();
            actualToneGenerator.GeneratorNotes.First().Pitch.Should().Equals(nameof(Pitch.C2));
            notesWeHopeAreRests.Select(n => n.Pitch).Should().AllBeEquivalentTo(nameof(Pitch.REST));
            notesWeHopeAreRests.Sum(n => (int)n.Duration).Should().Equals(expectedDuration);
            notesWeHopeAreRests.All(n => (int)n.Duration <= byte.MaxValue).Should().BeTrue();
            actualToneGenerator.GeneratorNotes.Last().Pitch.Should().Equals(nameof(Pitch.C2));
        }

        [Test]
        public void GroupByGenerator_TwoPartsDifferentPlayLengths_ThrowException()
        {
            var singlePartTwoVoices = new PartBuilder()
                .AddPartAndVoice("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddMeasureOfOneNoteChords("p1", "v1")
                .AddPartAndVoice("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .AddMeasureOfOneNoteChords("p2", "v3")
                .Build();
            const int DURATION_OF_THESE_HYPOTHETICAL_MEASURES = 36;
            const int DURATION_OF_PART1 = DURATION_OF_THESE_HYPOTHETICAL_MEASURES * 3;
            const int DURATION_OF_PART2 = DURATION_OF_THESE_HYPOTHETICAL_MEASURES * 2;

            //Act
            var ex = Assert.Throws<Exception>(() => new SN76489NoteGenerator().GetToneGenerators(singlePartTwoVoices, "LBL", _defaultOptions));

            //Assert
            ex.Message.Should().Be($"Part 'p1' voice 'v1' has a duration of {DURATION_OF_PART1} over 3 measures, but Part 'p2' voice 'v1' has a curation of {DURATION_OF_PART2} over 2 measures.");
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
                    Duration = Duration.N16,
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
                    Duration = Duration.N16,
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
                    Duration = Duration.N16,
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
                expectedToneGenerators[i - 1].RepeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>
                {
                    ( "REPEAT", "STOP" )
                };
            }
        }
    }
}