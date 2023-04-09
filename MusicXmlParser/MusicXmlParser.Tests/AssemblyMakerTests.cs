using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class AssemblyMakerTests
    {
        [Test]
        public void AssemblyMaker_SecondVoiceHasShorterDurationThanFirst_RestAddedToOutput()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
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
            new AssemblyMakerInstantiator().GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
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
<score-partwise>
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
            new AssemblyMakerInstantiator().GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
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
<score-partwise>
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
            new AssemblyMakerInstantiator().GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
            streamWriter.Flush();

            //Assert
            memoryStream.Position = 0;
            using var streamReader = new StreamReader(memoryStream);
            var actualText = streamReader.ReadToEnd();
            TextAsserts.EquivalentLines(EXPECTED_TEXT, actualText);
        }
    }
}