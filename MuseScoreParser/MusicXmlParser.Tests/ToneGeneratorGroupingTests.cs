using FluentAssertions;
using MuseScoreParser;
using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using NUnit.Framework;
using System.Collections.Generic;

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
                    GeneratorNotes = new List<GeneratorNote>
                    {
                        new GeneratorNote
                        {
                            StartMeasure = 1,
                            EndMeasure = 1,
                            Duration = Duration.N8,
                            Pitch = Pitch.Gb3
                        },
                        new GeneratorNote
                        {
                            StartMeasure = 1,
                            EndMeasure = 1,
                            Duration = Duration.N4,
                            Pitch = Pitch.Ds3
                        },
                        new GeneratorNote
                        {
                            StartMeasure = 2,
                            EndMeasure = 2,
                            Duration = Duration.N8,
                            Pitch = Pitch.Gb3
                        },
                        new GeneratorNote
                        {
                            StartMeasure = 2,
                            EndMeasure = 2,
                            Duration = Duration.N4,
                            Pitch = Pitch.Ds3
                        }
                    }
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
}