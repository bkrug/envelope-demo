using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class DivisionsTests
    {
        [Test]
        public void Divisions_QuarterNoteHasLengthOf24_32ndHasLengthOf3()
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
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>24</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <alter>1</alter>
          <octave>2</octave>
          </pitch>
        <duration>3</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>2</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <octave>2</octave>
          </pitch>
        <duration>2</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>2</duration>
        <voice>5</voice>
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
       BYTE D1,N4
       BYTE As0,N32
       BYTE D1,N32TRP
       BYTE A0,N32TRP
       BYTE D1,N32TRP
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
        public void Divisions_QuarterNoteHasLengthOf8_32ndHasLengthOf1()
        {
            const string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P13"">
    <measure number=""0"">
      <attributes>
        <divisions>8</divisions>
      </attributes>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>8</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>A</step>
          <alter>1</alter>
          <octave>2</octave>
          </pitch>
        <duration>1</duration>
        <voice>5</voice>
      </note>
      <note>
        <pitch>
          <step>D</step>
          <octave>3</octave>
          </pitch>
        <duration>32</duration>
        <voice>5</voice>
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
       BYTE D1,N4
       BYTE As0,N32
       BYTE D1,N1
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