using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
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
    }
}