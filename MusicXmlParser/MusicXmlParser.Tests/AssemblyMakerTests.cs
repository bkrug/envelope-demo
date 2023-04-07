﻿using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class AssemblyMakerTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        private AssemblyMaker GetAssemblyMaker()
        {
            return new AssemblyMaker(new NoteParser(_logger.Object), new SN76489NoteGenerator(_logger.Object), new AssemblyWriter());
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
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
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
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
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
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
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
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
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
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
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
    }
}