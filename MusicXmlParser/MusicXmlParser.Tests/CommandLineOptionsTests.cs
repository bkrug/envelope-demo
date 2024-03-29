﻿using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class CommandLineOptionsTests
    {
        [Test]
        public void CommandLineOptions_CreditInformationSupplied_CreditInformationInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <work>
    <work-title>I Love Eggs</work-title>
  </work>
  <identification>
    <creator type=""composer"">Somebody Somewhere</creator>
    <source>http://whitehouse.gov</source>
  </identification>
  <part id=""P13"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
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
@"       DEF  ORCHES

*
* I Love Eggs
* Somebody Somewhere
* Source: http://whitehouse.gov
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
ORCHES DATA ORCH1,0,0
* Data structures dealing with repeated music
       DATA REPT1,0,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1A,ORCH1
       DATA REPEAT,REPT1

* Generator 1
* Measure 1
ORCH1
       BYTE B0,N8
       BYTE A0,N16
ORCH1A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };
            var instantiator = new AssemblyMakerInstantiator();

            //Act
            var streamWriter = new StreamWriter(instantiator.MemoryStream);
            instantiator.GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            var actualText = instantiator.GetContentsOfMemoryStream();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }

        [Test]
        public void CommandLineOptions_IncludeCompilerWarningSupplied_WarningInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P13"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
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
@"       DEF  ORCHES

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
ORCHES DATA ORCH1,0,0
* Data structures dealing with repeated music
       DATA REPT1,0,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1A,ORCH1
       DATA REPEAT,REPT1

* Generator 1
* Measure 1
ORCH1
       BYTE B0,N8
       BYTE A0,N16
ORCH1A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning,
                DisplayRepoWarning = true
            };
            var instantiator = new AssemblyMakerInstantiator();

            //Act
            var streamWriter = new StreamWriter(instantiator.MemoryStream);
            instantiator.GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            var actualText = instantiator.GetContentsOfMemoryStream();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }

        [Test]
        public void CommandLineOptions_AsmLabelProvided_DefMatchesParameterAndInternalLabelsMatchFirstFourLetters()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P13"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
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
@"       DEF  MOZART

*
* This is auto-generated code.
* It is only included in the repo for the convenience of people who haven't cloned it.
*

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MOZART DATA MOZA1,0,0
* Data structures dealing with repeated music
       DATA REPT1,0,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA MOZA1A,MOZA1
       DATA REPEAT,REPT1

* Generator 1
* Measure 1
MOZA1
       BYTE B0,N8
       BYTE A0,N16
MOZA1A
*

";
            var options = new Options
            {
                AsmLabel = "MOZART",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning,
                DisplayRepoWarning = true
            };
            var instantiator = new AssemblyMakerInstantiator();

            //Act
            var streamWriter = new StreamWriter(instantiator.MemoryStream);
            instantiator.GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            var actualText = instantiator.GetContentsOfMemoryStream();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }
    }
}