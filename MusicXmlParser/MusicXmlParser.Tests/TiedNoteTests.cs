using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class TiedNoteTests
    {
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
            new AssemblyMakerInstantiator().GetAssemblyMaker().ConvertToAssembly(options, XDocument.Parse(MUSIC_XML), ref streamWriter);
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
* Measure 1 - 2
MELD1
       BYTE E2,N4
       BYTE A3,30
       BYTE D2,N4DOT
MELD1A
*

* Generator 2
* Measure 1 - 2
MELD2
       BYTE C2,N4
       BYTE REST,30
       BYTE A2,N4DOT
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