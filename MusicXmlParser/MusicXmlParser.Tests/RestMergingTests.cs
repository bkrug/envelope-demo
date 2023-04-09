using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class RestMergingTests
    {
        [Test]
        public void RestMerging_ConsequtiveRestsDifferentMeasures_MeasuresAreCombined()
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
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
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
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>12</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
        public void RestMerging_LongDurationOfRests_CombinationsDoNotExceedDurationOf255()
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
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>72</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>72</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>72</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
* Measure 1 - 2
ORCH1
       BYTE A0,N4
       BYTE Cs1,N4
       BYTE REST,N1
* Measure 3 - 5
       BYTE REST,216
* Measure 6
       BYTE A0,N4
       BYTE A0,N4
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
        public void RestMerging_LongDurationOfRests_BeginningAndEndOfRestDoesNotAlignToMeasures()
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
          <step>A</step>
          <octave>2</octave>
        </pitch>
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>72</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
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
* Measure 1 - 6
ORCH1
       BYTE A0,N4
       BYTE Cs1,N4
       BYTE REST,120
       BYTE REST,240
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
        public void RestMerging_ContainsRepeatBars_RepeatLabelsRetained()
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
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <barline location=""left"">
        <repeat direction=""foRward""/>
      </barline>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
      </note>
    </measure>
    <measure>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>quarter</type>
        <stem>down</stem>
      </note>
      <note>
        <rest />
        <duration>24</duration>
        <voice>1</voice>
        <type>eighth</type>
        <stem>down</stem>
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
ORCHES DATA ORCH1,0,0
* Data structures dealing with repeated music
       DATA REPT1,0,0
* Duration ratio in 60hz environment
       DATA 2,1
* Duration ratio in 50hz environment
       DATA 10,6

REPT1
       DATA ORCH1B,ORCH1A
       DATA REPEAT,REPT1

* Generator 1
* Measure 1 - 2
ORCH1
       BYTE B0,N4
       BYTE REST,N2DOT
* Measure 3 - 4
ORCH1A
       BYTE REST,N1
ORCH1B
*

";
            var options = new Options
            {
                AsmLabel = "ORCHES",
                Ratio60Hz = "2:1",
                Ratio50Hz = "10:6",
                RepetitionType = RepetitionType.RepeatFromFirstJump
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