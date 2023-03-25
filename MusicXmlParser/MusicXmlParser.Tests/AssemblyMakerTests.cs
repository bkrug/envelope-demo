using FluentAssertions;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.IO;

namespace MusicXmlParser.Tests
{
    public class AssemblyMakerTests
    {
        //TODO: Add a (shorter) test for situations where a single note crosses two measures
        //TODO: Add a (shorter) test for situations where a note is outside of the chip's range
        private AssemblyMaker GetAssemblyMaker()
        {
            return new AssemblyMaker(new NewNoteParser(), new SN76489NoteGenerator(), new AssemblyWriter());
        }

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
            GetAssemblyMaker().ConvertToAssembly(options);

            //Assert
            var expectedText = File.ReadAllText("Files//TUNEFURELISE.asm");
            var actualText = File.ReadAllText(options.OutputFile);
            actualText.Should().BeEquivalentTo(expectedText);
        }

        [Test]
        public void AssemblyMaker_GraceNoteInSource_CommentedOutNoteInOutput()
        {
            const string MUSIC_XML = 
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <work>
    <work-title>Dawning of the Age of Acquarius</work-title>
  </work>
  <identification>
    <creator type=""composer"">Ludwig de Belize</creator>
    <source>http://note-a-real-website/you-better-respect-it-anyway/music/3</source>
  </identification>
  <part-list>
    <score-part id=""P13"">
    </score-part>
  </part-list>
  <part id=""P13"">
    <measure number=""0"" implicit=""yes"" width=""147.40"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note default-x=""81.48"" default-y=""-5.00"">
        <pitch>
          <step>E</step>
          <octave>5</octave>
        </pitch>
        <duration>6</duration>
        <voice>1</voice>
        <type>16th</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <grace slash=""yes""/>
        <pitch>
          <step>F</step>
          <octave>4</octave>
          </pitch>
        <voice>1</voice>
        <type>16th</type>
        <stem>up</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
        <pitch>
          <step>F</step>
          <octave>5</octave>
        </pitch>
        <duration>6</duration>
        <voice>1</voice>
        <type>16th</type>
        <stem>down</stem>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  MELDY

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*
* Dawning of the Age of Acquarius
* Ludwig de Belize
* Source: http://note-a-real-website/you-better-respect-it-anyway/music/3
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MELDY  DATA MELD1,MELD2,MELD3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA MELD1A,MELD1
       DATA REPEAT,REPT1

* Generator 1
* Measure 1
MELD1
       BYTE E3,N16
*       BYTE F2,0        Grace Note
       BYTE F3,N16
MELD1A
*

";
            var options = new Options
            {
                AsmLabel = "MELDY",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, MUSIC_XML, ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }
    }
}