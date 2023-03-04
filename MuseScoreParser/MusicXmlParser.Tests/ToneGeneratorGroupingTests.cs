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
    }
}