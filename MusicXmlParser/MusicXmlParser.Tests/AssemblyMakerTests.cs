using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    //TODO: Delete the first middle C in "The Merry Farmer" and see the error that happens.
    //      Write a test to reproduce this.
    //TODO: Delete the Fuer Elise .mscz and .musicxml files from the source code.
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
        public void GroupByGenerator_FirstMeasureHasFewerNotesThanNextMeasure_NoNullReferenceErrors()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P1"">
    <measure number=""0"">
      <attributes>
        <divisions>2</divisions>
      </attributes>
      <note>
        <rest/>
        <duration>1</duration>
        <voice>1</voice>
        <type>eighth</type>
        <staff>1</staff>
        </note>
      <backup>
        <duration>1</duration>
        </backup>
      <note>
        <pitch>
          <step>C</step>
          <octave>3</octave>
          </pitch>
        <duration>1</duration>
        <voice>5</voice>
        <type>eighth</type>
        <stem>up</stem>
        <staff>2</staff>
      </note>
    </measure>
    <measure number=""1"">
      <note>
        <pitch>
          <step>C</step>
          <octave>4</octave>
          </pitch>
        <duration>8</duration>
        <voice>1</voice>
        <type>whole</type>
      </note>
      <backup>
        <duration>8</duration>
        </backup>
      <note>
        <pitch>
          <step>D</step>
          <octave>4</octave>
        </pitch>
        <duration>4</duration>
        <voice>3</voice>
        <type>half</type>
      </note>
      <backup>
        <duration>4</duration>
      </backup>
      <note>
        <pitch>
          <step>F</step>
          <octave>3</octave>
          </pitch>
        <duration>8</duration>
        <voice>5</voice>
        <type>whole</type>
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
ORCHES DATA ORCH1,ORCH2,ORCH3
* Data structures dealing with repeated music
       DATA REPT1,REPT2,REPT3
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
REPT3
       DATA ORCH3A,ORCH3
       DATA REPEAT,REPT3

* Generator 1
* Measure 1 - 2
ORCH1
       BYTE REST,N4
       BYTE C2,N8
       BYTE C2,N8
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N8
ORCH1A
*

* Generator 2
* Measure 1 - 2
ORCH2
       BYTE C1,N8
       BYTE F1,N4DOT
       BYTE A1,N8
       BYTE C2,N4DOT
       BYTE F1,N8
ORCH2A
*

* Generator 3
* Measure 1 - 2
ORCH3
       BYTE REST,N4
       BYTE F2,N8
       BYTE F2,N8
       BYTE REST,N4
       BYTE A2,N8
       BYTE A2,N8
       BYTE REST,N8
ORCH3A
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