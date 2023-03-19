using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;

namespace MusicXmlParser.Tests
{
    public class EnumParsingTests
    {
        [Test]
        [TestCase("whole", false, false, Duration.N1)]
        [TestCase("haLF", false, false, Duration.N2)]
        [TestCase("Eighth", false, false, Duration.N8)]
        [TestCase("8th", false, false, Duration.N8)]
        [TestCase("16th", true, false, Duration.N16DOT)]
        [TestCase("SixTeenth", false, false, Duration.N16)]
        [TestCase("whOle", true, false, Duration.N1DOT)]
        [TestCase("Whole", false, false, Duration.N1)]
        [TestCase("16th", false, true, Duration.N16TRP)]
        [TestCase("Quarter", false, true, Duration.N4TRP)]
        [TestCase("64th", false, true, Duration.N64TRP)]
        public void ParseDuration_WithDots_IsValid(string input, bool isDotted, bool isTripplet, Duration expectedOutput)
        {
            //Arrange
            var note = new NewNote
            {
                Type = input,
                IsDotted = isDotted,
                IsTripplet = isTripplet
            };

            //Act
            var isParsed = DurationParser.TryParse(note, out Duration actualOutput);

            //Assert
            isParsed.Should().BeTrue();
            actualOutput.Should().Be(expectedOutput);
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void ParseDuration_NoDots_IsNotValid(bool isDotted, bool isTripplet)
        {
            //Arrange
            var note = new NewNote
            {
                Type = "invalidValue",
                IsDotted = isDotted,
                IsTripplet = isTripplet
            };

            //Act
            var isParsed = DurationParser.TryParse(note, out Duration actualOutput);

            //Assert
            isParsed.Should().BeFalse();
        }

        [Test]
        public void ParseDuration_NoDottedVersionExists_IsNotValid()
        {
            //Arrange
            var note = new NewNote
            {
                Type = "32nd",
                IsDotted = true
            };

            //Act
            var isParsed = DurationParser.TryParse(note, out Duration actualOutput);

            //Assert
            //Dotted 32nd Notes are not supported by this parser.
            //The duration cannot be divided by 2 using integers.
            isParsed.Should().BeFalse();
        }

        [Test]
        [TestCase("C", "0", "4", nameof(Pitch.C2))]
        [TestCase("C", "1", "4", nameof(Pitch.Cs2))]
        [TestCase("C", "-1", "4", nameof(Pitch.B1))]
        [TestCase("B", "0", "3", nameof(Pitch.B1))]
        [TestCase("B", "-1", "3", nameof(Pitch.Bb1))]
        [TestCase("B", "1", "3", nameof(Pitch.C2))]
        [TestCase("G", "0", "4", nameof(Pitch.G2))]
        [TestCase("G", "1", "4", nameof(Pitch.Gs2))]
        [TestCase("G", "-1", "4", nameof(Pitch.Gb2))]
        [TestCase("E", "0", "5", nameof(Pitch.E3))]
        [TestCase("A", "0", "2", nameof(Pitch.A0))]
        [TestCase("F", "0", "2", "F0")]    //These are outside the range of the SN76489 chip
        [TestCase("G", "1", "1", "Gs-1")]
        [TestCase("B", "-1", "9", "Bb7")]
        public void ParsePitch_IsValid(string inputNote, string alter, string octave, string expectedPitch)
        {
            var note = new NewNote
            {
                Step = inputNote,
                Alter = alter,
                Octave = octave
            };

            //Act
            var isParsed = PitchParser.TryParse(note, out var actualPitch);

            //Assert
            isParsed.Should().BeTrue();
            actualPitch.Should().Be(expectedPitch);
        }

        [Test]
        public void ParsePitch_IsRest_IsValid()
        {
            var note = new NewNote
            {
                IsRest = true
            };

            //Act
            var isParsed = PitchParser.TryParse(note, out var actualPitch);

            //Assert
            isParsed.Should().BeTrue();
            actualPitch.Should().Be(nameof(Pitch.REST));
        }

        [Test]
        [TestCase("AA", "0", "2")]
        [TestCase("C", "notANumber", "2")]
        [TestCase("C", "0", "notANumber")]
        public void ParsePitch_IsNotValid(string inputNote, string alter, string octave)
        {
            var note = new NewNote
            {
                Step = inputNote,
                Alter = alter,
                Octave = octave
            };

            //Act
            var isParsed = PitchParser.TryParse(note, out var actualPitch);

            //Assert
            isParsed.Should().BeFalse();
        }
    }
}