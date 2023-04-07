using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;

namespace MusicXmlParser.Tests
{
    public class SnapshotTests
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
                RepetitionType = RepetitionType.RepeatFromBeginning,
                DisplayRepoWarning = true
            };

            //Act
            new AssemblyMakerInstantiator().GetAssemblyMaker().ConvertToAssembly(options);

            //Assert
            var expectedText = File.ReadAllText("Files//TUNEFURELISE.asm");
            var actualText = File.ReadAllText(options.OutputFile);
            TextAsserts.EquivalentLines(expectedText, actualText);
        }
    }
}