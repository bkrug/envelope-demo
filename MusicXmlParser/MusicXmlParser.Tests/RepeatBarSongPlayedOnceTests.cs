using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class RepeatBarSongPlayedOnceTests
    {
        [Test]
        public void RepeatBarSongPlayedOnce_NoRepeats_PlayTheSongStraightThrough()
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
          <step>C</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
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
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>E</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>E</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>G</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>G</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
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
       DATA ORCH1A,STOP
       DATA REPEAT,STOP
REPT2
       DATA ORCH2A,STOP
       DATA REPEAT,STOP

* Generator 1
* Measure 1
ORCH1
       BYTE C3,N8
       BYTE D3,N8
* Measure 2
       BYTE E3,N8
       BYTE F3,N8
* Measure 3
       BYTE G3,N8
       BYTE A3,N8
ORCH1A
*

* Generator 2
* Measure 1
ORCH2
       BYTE C1,N8
       BYTE D1,N8
* Measure 2
       BYTE E1,N8
       BYTE F1,N8
* Measure 3
       BYTE G1,N8
       BYTE A1,N8
ORCH2A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.StopAtEnd
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
        public void RepeatBarSongPlayedOnce_OnlyOneBackwardRepeat_RepeatFromBeginningOnce()
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
          <step>C</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
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
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>E</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>E</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>G</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>G</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <barline location=""right"">
        <repeat direction=""backWard""/>
      </barline>
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
       DATA REPEAT,STOP
REPT2
       DATA ORCH2A,ORCH2
       DATA REPEAT,STOP

* Generator 1
* Measure 1
ORCH1
       BYTE C3,N8
       BYTE D3,N8
* Measure 2
       BYTE E3,N8
       BYTE F3,N8
* Measure 3
       BYTE G3,N8
       BYTE A3,N8
ORCH1A
*

* Generator 2
* Measure 1
ORCH2
       BYTE C1,N8
       BYTE D1,N8
* Measure 2
       BYTE E1,N8
       BYTE F1,N8
* Measure 3
       BYTE G1,N8
       BYTE A1,N8
ORCH2A
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.StopAtEnd
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
        public void RepeatBarSongPlayedOnce_BackwardAndForwardRepeat_RepeatOneSectionOnce()
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
          <step>C</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
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
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <barline location=""left"">
        <repeat direction=""forward""/>
      </barline>
      <note>
        <pitch>
          <step>E</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>E</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>F</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>G</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>G</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
    </measure>

    <measure>
      <note>
        <pitch>
          <step>B</step>
          <octave>5</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <note>
        <pitch>
          <step>C</step>
          <octave>6</octave>
        </pitch>
        <duration>12</duration>
        <voice>1</voice>
      </note>
      <backup>
        <duration>24</duration>
      </backup>
      <note>
        <pitch>
          <step>B</step>
          <octave>3</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <note>
        <pitch>
          <step>C</step>
          <octave>4</octave>
        </pitch>
        <duration>12</duration>
        <voice>2</voice>
      </note>
      <barline location=""riGht"">
        <repeat direction=""backward""/>
      </barline>
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
       DATA ORCH1B,ORCH1A
       DATA REPEAT,STOP
REPT2
       DATA ORCH2B,ORCH2A
       DATA REPEAT,STOP

* Generator 1
* Measure 1
ORCH1
       BYTE C3,N8
       BYTE D3,N8
* Measure 2
ORCH1A
       BYTE E3,N8
       BYTE F3,N8
* Measure 3
       BYTE G3,N8
       BYTE A3,N8
* Measure 4
       BYTE B3,N8
       BYTE C4,N8
ORCH1B
*

* Generator 2
* Measure 1
ORCH2
       BYTE C1,N8
       BYTE D1,N8
* Measure 2
ORCH2A
       BYTE E1,N8
       BYTE F1,N8
* Measure 3
       BYTE G1,N8
       BYTE A1,N8
* Measure 4
       BYTE B1,N8
       BYTE C2,N8
ORCH2B
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.StopAtEnd
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