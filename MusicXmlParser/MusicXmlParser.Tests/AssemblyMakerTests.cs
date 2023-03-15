using FluentAssertions;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.IO;

namespace MusicXmlParser.Tests
{
    public class AssemblyMakerTests
    {
        [Test]
        public void AssemblyMaker_FurElise()
        {
            var options = new Options
            {
                InputFile = "Files//Fr_Elise_SN76489.musicxml",
                OutputFile = "Files//actual_TUNEFURELISE.asm",
                AsmLabel = "BEETHV",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6"
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