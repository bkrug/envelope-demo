using FluentAssertions;
using MuseScoreParser;
using MuseScoreParser.Enums;
using NUnit.Framework;

namespace MusicXmlParser.Tests
{
    public class EnumParsingTests
    {
        [Test]
        [TestCase("whole", Duration.N1)]
        [TestCase("haLF", Duration.N2)]
        [TestCase("Eighth", Duration.N8)]
        [TestCase("8th", Duration.N8)]
        public void ParseDuration_NoDots_IsValid(string input, Duration expectedOutput)
        {
            //Act
            var isParsed = DurationParser.TryParse(input, out Duration actualOutput);

            //Assert
            isParsed.Should().BeTrue();
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        [TestCase("16th", true, Duration.N16DOT)]
        [TestCase("SixTeenth", false, Duration.N16)]
        [TestCase("whOle", true, Duration.N1DOT)]
        [TestCase("Whole", false, Duration.N1)]
        public void ParseDuration_WithDots_IsValid(string input, bool dotted, Duration expectedOutput)
        {
            //Act
            var isParsed = DurationParser.TryParse(input, dotted, out Duration actualOutput);

            //Assert
            isParsed.Should().BeTrue();
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        public void ParseDuration_NoDots_IsNotValid()
        {
            //Act
            var isParsed = DurationParser.TryParse("invalidValue", out Duration actualOutput);

            //Assert
            isParsed.Should().BeFalse();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ParseDuration_WithDots_IsNotValid(bool isDotted)
        {
            //Act
            var isParsed = DurationParser.TryParse("invalidValue", isDotted, out Duration actualOutput);

            //Assert
            isParsed.Should().BeFalse();
        }

        [Test]
        public void ParseDuration_NoDottedVersionExists_IsNotValid()
        {
            //Act
            var isParsed = DurationParser.TryParse("32nd", true, out Duration actualOutput);

            //Assert
            //Dotted 32nd Notes are not supported by this parser.
            //The duration cannot be divided by 2 using integers.
            isParsed.Should().BeFalse();
        }
    }
}