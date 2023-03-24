using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.IO;

namespace MusicXmlParser.Tests
{
    //TODO: Add a (shorter) test for situations where a single note crosses two measures
    //TODO: Add a (shorter) test for situations where a note is outside of the chip's range
    //TODO: Add a (shorter) test for a grace note
    public class AssemblyMakerTests
    {
        /// <summary>
        /// This resembles the snapshot tests one would run into testing a React application.
        /// If it fails, you want to inspect the differences between the old and current output.
        /// If the changes make sense, just edit "TUNEFURELISE.asm" to match "actual_TUNEFURELISE.asm"
        /// </summary>
        [Test]
        public void AssemblyMaker_FurElise_Snapshot()
        {
            var options = new Options
            {
                InputFile = "Files//Fr_Elise_SN76489.musicxml",
                OutputFile = "Files//actual_TUNEFURELISE.asm",
                AsmLabel = "BEETHV",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };

            //Act
            var maker = new AssemblyMaker(new NewNoteParser(), new SN76489NoteGenerator(), new AssemblyWriter());
            maker.ConvertToAssembly(options);

            //Assert
            var expectedText = File.ReadAllText("Files//TUNEFURELISE.asm");
            var actualText = File.ReadAllText(options.OutputFile);
            actualText.Should().BeEquivalentTo(expectedText);
        }
    }
}