using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class AssemblyMakerTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        //TODO: Write a test case for ties
        private AssemblyMaker GetAssemblyMaker()
        {
            return new AssemblyMaker(new NoteParser(_logger.Object), new SN76489NoteGenerator(_logger.Object), new AssemblyWriter());
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
                RepetitionType = RepetitionType.RepeatFromBeginning,
                DisplayRepoWarning = true
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

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MELDY  DATA MELD1,0,0
* Data structures dealing with repeated music
       DATA REPT1,0,0
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
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_NoteOutsideRangeOfSN76489_OutputRestWithComment()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <pitch>
          <step>F</step>
          <octave>2</octave>
        </pitch>
        <duration>6</duration>
        <voice>1</voice>
        <type>16th</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
       BYTE REST,N16      * Invalid: F0
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
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_NoteIsMissingRequiredTags_OutputWithComment()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <pitch>
          <alter>1</alter>
        </pitch>
        <voice>1</voice>
        <duration>6</duration>
        <type>16th</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
       BYTE REST,N16      * Invalid: 
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
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_RestsCanMerge_MeasuresAreCombined()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <pitch>
          <step>C</step>
          <alter>1</alter>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note default-x=""81.48"" default-y=""-5.00"">
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note default-x=""81.48"" default-y=""-5.00"">
        <rest />
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
        <rest />
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note default-x=""81.48"" default-y=""-5.00"">
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  ORCHES

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
       BYTE B0,N4
       BYTE Cs1,N4
* Measure 2 - 4
       BYTE A0,N4
       BYTE REST,N4DOT
       BYTE A0,N4
       BYTE REST,N4DOT
       BYTE A0,N4
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
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_PitchNotParsable_OutputCommentOnInvalid_LogError()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <pitch>
          <step>chalkboards</step>
          <alter>are</alter>
          <octave>instruments</octave>
        </pitch>
        <duration>6</duration>
        <voice>1</voice>
        <type>16th</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
       BYTE REST,N16      * Invalid: 
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
            var memoryStream = new MemoryStream();
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);
            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
            actualMessage.Should().BeEquivalentTo("Could not parse pitch in measure 1: {\"Step\":\"chalkboards\",\"Alter\":\"are\",\"Octave\":\"instruments\",\"IsRest\":false,\"IsDotted\":false,\"IsTripplet\":false,\"IsGraceNote\":false,\"Duration\":\"6\",\"Type\":\"16th\"}");
        }

        [Test]
        public void AssemblyMaker_DurationNotParsable_OutputCommentOnInvalid_LogError()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""60.39"" default-y=""-35.00"">
        <pitch>
          <step>C</step>
          <alter>1</alter>
          <octave>3</octave>
        </pitch>
        <duration>not-parsable</duration>
        <voice>1</voice>
        <type>16th</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
*       BYTE Cs1,0        Invalid duration
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
            var memoryStream = new MemoryStream();
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);
            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
            actualMessage.Should().BeEquivalentTo("Could not parse duration in measure 1: \"not-parsable\"");
        }

        [Test]
        public void AssemblyMaker_CreditInformationSupplied_CreditInformationInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <work>
    <work-title>I Love Eggs</work-title>
  </work>
  <identification>
    <creator type=""composer"">Somebody Somewhere</creator>
    <source>http://whitehouse.gov</source>
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_IncludeCompilerWarningSupplied_WarningInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
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
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note default-x=""81.48"" default-y=""-5.00"">
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
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_SecondVoiceHasShorterDurationThanFirst_RestAddedToOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <part-list>
    <score-part id=""P1"">
    </score-part>
  </part-list>
  <part id=""P1"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
    </measure>
    <measure number=""1"">
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>C</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>48</duration>
      </backup>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>2</voice>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  ORCHES

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
ORCHES DATA ORCH1,ORCH2,0
* Data structures dealing with repeated music
       DATA REPT1,REPT2,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1A,ORCH1
       DATA REPEAT,REPT1
REPT2
       DATA ORCH2A,ORCH2
       DATA REPEAT,REPT2

* Generator 1
* Measure 1
ORCH1
       BYTE A0,N4
* Measure 2
       BYTE B0,N4
       BYTE C1,N4
ORCH1A
*

* Generator 2
* Measure 1
ORCH2
       BYTE REST,N4
* Measure 2
       BYTE D1,N4
       BYTE REST,N4
ORCH2A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_FirstVoiceHasShorterDurationThanSecond_RestAddedToOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <part-list>
    <score-part id=""P1"">
    </score-part>
  </part-list>
  <part id=""P1"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
    </measure>
    <measure number=""1"">
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>C</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>2</voice>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  ORCHES

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
ORCHES DATA ORCH1,ORCH2,0
* Data structures dealing with repeated music
       DATA REPT1,REPT2,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1A,ORCH1
       DATA REPEAT,REPT1
REPT2
       DATA ORCH2A,ORCH2
       DATA REPEAT,REPT2

* Generator 1
* Measure 1
ORCH1
       BYTE A0,N4
* Measure 2
       BYTE B0,N4
       BYTE REST,N4
ORCH1A
*

* Generator 2
* Measure 1
ORCH2
       BYTE REST,N4
* Measure 2
       BYTE C1,N4
       BYTE D1,N4
ORCH2A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            actualText.Should().BeEquivalentTo(EXPECTED_TEXT);
        }

        [Test]
        public void AssemblyMaker_BackupDoesNotReturnToMeasureStart_RestInsertedBeforeNotes()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <part-list>
    <score-part id=""P1"">
    </score-part>
  </part-list>
  <part id=""P1"">
    <measure number=""0"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>A</step>
          <alter>1</alter>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>2</voice>
      </note>
    </measure>
    <measure number=""1"">
      <note>
        <pitch>
          <step>B</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>C</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>36</duration>
      </backup>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>24</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>E</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  ORCHES

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
ORCHES DATA ORCH1,ORCH2,0
* Data structures dealing with repeated music
       DATA REPT1,REPT2,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1A,ORCH1
       DATA REPEAT,REPT1
REPT2
       DATA ORCH2A,ORCH2
       DATA REPEAT,REPT2

* Generator 1
* Measure 1
ORCH1
       BYTE A0,N4
* Measure 2
       BYTE B0,N4
       BYTE C1,N4
ORCH1A
*

* Generator 2
* Measure 1
ORCH2
       BYTE As0,N4
* Measure 2
       BYTE REST,N8
       BYTE D1,N4
       BYTE E1,N8
ORCH2A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromBeginning
            };
            var memoryStream = new MemoryStream();

            //Act
            var streamWriter = new StreamWriter(memoryStream);
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }

        [Test]
        public void TiedNotes_StayWithinSingleMeasure_AppearAsLongerNotesInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <part-list>
    <score-part id=""P13"">
    </score-part>
  </part-list>
  <part id=""P13"">
    <measure number=""0"" implicit=""yes"" width=""147.40"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note default-x=""16.50"" default-y=""-125.00"">
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>24</duration>
        <tie type=""start""/>
        <voice>5</voice>
        <type>quarter</type>
      </note>
      <note default-x=""16.50"" default-y=""-90.00"">
        <chord/>
        <pitch>
          <step>D</step>
          <octave>4</octave>
          </pitch>
        <duration>24</duration>
        <tie type=""start""/>
        <voice>5</voice>
        <type>quarter</type>
      </note>
      <note default-x=""110.83"" default-y=""-125.00"">
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>12</duration>
        <tie type=""stop""/>
        <voice>5</voice>
        <type>eighth</type>
      </note>
      <note default-x=""110.83"" default-y=""-90.00"">
        <chord/>
        <pitch>
          <step>D</step>
          <octave>4</octave>
          </pitch>
        <duration>12</duration>
        <tie type=""stop""/>
        <voice>5</voice>
        <type>eighth</type>
      </note>
      <note default-x=""157.99"" default-y=""-140.00"">
        <pitch>
          <step>A</step>
          <octave>2</octave>
          </pitch>
        <duration>12</duration>
        <voice>5</voice>
        <type>eighth</type>
      </note>
      <note default-x=""157.99"" default-y=""-105.00"">
        <chord/>
        <pitch>
          <step>A</step>
          <octave>3</octave>
          </pitch>
        <duration>12</duration>
        <voice>5</voice>
        <type>eighth</type>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  MELDY

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MELDY  DATA MELD1,MELD2,0
* Data structures dealing with repeated music
       DATA REPT1,REPT2,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA MELD1A,MELD1
       DATA REPEAT,REPT1
REPT2
       DATA MELD2A,MELD2
       DATA REPEAT,REPT2

* Generator 1
* Measure 1
MELD1
       BYTE D1,N4DOT
       BYTE A0,N8
MELD1A
*

* Generator 2
* Measure 1
MELD2
       BYTE D2,N4DOT
       BYTE A1,N8
MELD2A
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
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }

        [Test]
        public void TiedNotes_AcrossMeasures_AppearAsLongerNotesInOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE score-partwise PUBLIC ""-//Recordare//DTD MusicXML 3.1 Partwise//EN"" ""http://www.musicxml.org/dtds/partwise.dtd"">
<score-partwise version=""3.1"">
  <part-list>
    <score-part id=""P13"">
    </score-part>
  </part-list>
  <part id=""P13"">
    <measure number=""0"" implicit=""yes"" width=""147.40"">
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>E</step>
          <octave>4</octave>
          </pitch>
        <duration>24</duration>
        <voice>5</voice>
        <type>quarter</type>
      </note>
      <note>
        <chord />
        <pitch>
          <step>C</step>
          <octave>4</octave>
          </pitch>
        <duration>24</duration>
        <voice>5</voice>
        <type>quarter</type>
      </note>
      <note default-x=""16.50"" default-y=""-90.00"">
        <pitch>
          <step>A</step>
          <octave>5</octave>
          </pitch>
        <duration>24</duration>
        <tie type=""start""/>
        <voice>5</voice>
        <type>quarter</type>
      </note>
    </measure>
    <measure number=""1"">
      <note>
        <pitch>
          <step>A</step>
          <octave>5</octave>
          </pitch>
        <duration>6</duration>
        <tie type=""stop""/>
        <voice>5</voice>
        <type>16th</type>
      </note>
      <note default-x=""110.83"" default-y=""-90.00"">
        <pitch>
          <step>D</step>
          <octave>4</octave>
          </pitch>
        <duration>36</duration>
        <voice>5</voice>
        <type>eighth</type>
      </note>
      <note default-x=""157.99"" default-y=""-140.00"">
        <chord />
        <pitch>
          <step>A</step>
          <octave>4</octave>
          </pitch>
        <duration>36</duration>
        <voice>5</voice>
        <type>eighth</type>
      </note>
    </measure>
  </part>
</score-partwise>";
            const string EXPECTED_TEXT =
@"       DEF  MELDY

       COPY 'NOTEVAL.asm'
       COPY 'CONST.asm'

*
* Song Header
*
MELDY  DATA MELD1,MELD2,0
* Data structures dealing with repeated music
       DATA REPT1,REPT2,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA MELD1A,MELD1
       DATA REPEAT,REPT1
REPT2
       DATA MELD2A,MELD2
       DATA REPEAT,REPT2

* Generator 1
* Measures 1-2
MELD1
       BYTE E2,N4
       BYTE A3,30
       BYTE D2,N8DOT
MELD1A
*

* Generator 2
* Measures 1-2
MELD2
       BYTE C2,N4
       BYTE REST,30
       BYTE A2,N8DOT
MELD2A
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
            GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }
    }
}