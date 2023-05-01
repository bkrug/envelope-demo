using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace MusicXmlParser.Tests
{
    public class NoteParserTests
    {
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        private static Chord GenerateSingleNoteChord(string step, string alter, string octave, Duration duration, string type, bool isGraceSlash = false)
        {
            return new Chord
            {
                Notes = new List<Note>
                {
                    new Note
                    {
                        Step = step,
                        Alter = alter,
                        Octave = octave,
                        Duration = ((int)duration).ToString(),
                        Type = type,
                        IsGraceNote = isGraceSlash
                    }
                }
            };
        }

        private NoteParser GetNoteParser()
        {
            return new NoteParser(_logger.Object);
        }

        //TODO: Create the following big unit tests:
        //* Parse rests correctly

        //Warning: I guess this needs to stay a small unit test, because the VoltaBracketTests are small.
        [Test]
        public void Parse_XmlContainsContainsVoltaBracket_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <barline location=""left"">
                <ending number=""1"" type=""start"" default-y=""33.10""/>
            </barline>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>3</octave>
                </pitch>
                <duration>48</duration>
                <type>half</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>3</octave>
                </pitch>
                <type>half</type>
                <duration>48</duration>
                <voice>1</voice>
            </note>
            <barline location=""right"">
                <bar-style>light-heavy</bar-style>
                <ending number=""1"" type=""stop""/>
                <repeat direction=""backward""/>
            </barline>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Divisions = "24",
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                HasVoltaBracket = true,
                                VoltaNumber = 1,
                                HasBackwardRepeat = true,
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("E", "", "3", Duration.N2, "half"),
                                                GenerateSingleNoteChord("A", "", "3", Duration.N2, "half")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsRests_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note default-x=""52.68"" default-y=""-81.59"">
                <pitch>
                    <step>A</step>
                    <octave>3</octave>
                </pitch>
                <duration>6</duration>
                <voice>5</voice>
                <type>16th</type>
            </note>
            <note>
                <rest/>
                <duration>6</duration>
                <voice>5</voice>
                <type>16th</type>
                <staff>2</staff>
            </note>
            <note>
                <rest/>
                <duration>12</duration>
                <voice>5</voice>
                <type>eighth</type>
                <staff>2</staff>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic
            {
                Parts = new List<Part>
                {
                    new Part
                    {
                        Divisions = "24",
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "5",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                new Chord
                                                {
                                                    Notes = new List<Note>
                                                    {
                                                        new Note
                                                        {
                                                            Step = "A",
                                                            Alter = string.Empty,
                                                            Octave = "3",
                                                            Duration = "6",
                                                            Type = "16th"
                                                        }
                                                    }
                                                },
                                                new Chord
                                                {
                                                    Notes = new List<Note>
                                                    {
                                                        new Note
                                                        {
                                                            IsRest = true,
                                                            Step = string.Empty,
                                                            Alter = string.Empty,
                                                            Octave= string.Empty,
                                                            Duration = "6",
                                                            Type = "16th"
                                                        }
                                                    }
                                                },
                                                new Chord
                                                {
                                                    Notes = new List<Note>
                                                    {
                                                        new Note
                                                        {
                                                            IsRest = true,
                                                            Step = string.Empty,
                                                            Alter = string.Empty,
                                                            Octave= string.Empty,
                                                            Duration = "12",
                                                            Type = "eighth"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_StepIsMissingInSource_ErrorLoggedToConsole()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                </pitch>
                <duration>6</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <type>eighth</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);

            //Act
            GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualMessage.Should().Be("Note in measure 1 missing a 'step' tag.");
        }

        [Test]
        public void Parse_OctaveIsMissingInSource_ErrorLoggedToConsole()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                </pitch>
                <duration>6</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
        </measure>
        <measure>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                </pitch>
                <duration>12</duration>
                <type>eighth</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);

            //Act
            GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualMessage.Should().Be("Note in measure 2 missing an 'octave' tag.");
        }

        [Test]
        public void Parse_DurationIsMissingInSource_ErrorLoggedToConsole()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                </pitch>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <type>eighth</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);

            //Act
            GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualMessage.Should().Be("Note in measure 1 missing a 'duration' tag.");
        }

        [Test]
        public void Parse_VoiceIsMissingInSource_ErrorLoggedToConsole()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
        </measure>
        <measure>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <type>eighth</type>
            </note>
        </measure>
    </part>
</score-partwise>";
            string actualMessage = string.Empty;
            _logger.Setup(l => l.WriteError(It.IsAny<string>()))
                .Callback((string m) => actualMessage = m);

            //Act
            GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualMessage.Should().Be("Note in measure 2 missing a 'voice' tag.");
        }

        [Test]
        [TestCase("A", "<alter>-1</alter>", "4", nameof(Pitch.Ab2))]
        [TestCase("A", "", "4", nameof(Pitch.A2))]
        [TestCase("A", "<alter>1</alter>", "4", nameof(Pitch.As2))]
        [TestCase("B", "<alter>-1</alter>", "4", nameof(Pitch.Bb2))]
        [TestCase("B", "", "4", nameof(Pitch.B2))]
        [TestCase("C", "", "4", nameof(Pitch.C2))]
        [TestCase("C", "<alter>1</alter>", "4", nameof(Pitch.Cs2))]
        [TestCase("D", "<alter>-1</alter>", "4", nameof(Pitch.Db2))]
        [TestCase("D", "", "4", nameof(Pitch.D2))]
        [TestCase("D", "<alter>1</alter>", "4", nameof(Pitch.Ds2))]
        [TestCase("E", "<alter>-1</alter>", "4", nameof(Pitch.Eb2))]
        [TestCase("E", "", "4", nameof(Pitch.E2))]
        [TestCase("F", "", "4", nameof(Pitch.F2))]
        [TestCase("F", "<alter>1</alter>", "4", nameof(Pitch.Fs2))]
        [TestCase("G", "<alter>-1</alter>", "4", nameof(Pitch.Gb2))]
        [TestCase("G", "", "4", nameof(Pitch.G2))]
        [TestCase("G", "<alter>1</alter>", "4", nameof(Pitch.Gs2))]
        [TestCase("a", "", "4", nameof(Pitch.A2))]
        [TestCase("e", "<alter>-1</alter>", "4", nameof(Pitch.Eb2))]
        [TestCase("D", "", "6", nameof(Pitch.D4))]
        [TestCase("E", "<alter>-1</alter>", "5", nameof(Pitch.Eb3))]
        [TestCase(" F ", "", "6", nameof(Pitch.F4))]
        public void Parse_XmlHasOnePitch_OutputContainsMatchingSn76489Pitch(string step, string alter, string octave, string expectedPitch)
        {
            string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P13"">
    <measure>
      <attributes>
        <divisions>24</divisions>
      </attributes>
      <note>
        <pitch>
          <step>" + step + @"</step>
          " + alter + @"
          <octave>" + octave + @"</octave>
          </pitch>
        <duration>96</duration>
        <voice>5</voice>
      </note>
    </measure>
  </part>
</score-partwise>";
            string EXPECTED_TEXT =
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
       BYTE " + expectedPitch + @",N1
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
        [TestCase("30", nameof(Duration.N32))]
        [TestCase("60", nameof(Duration.N16))]
        [TestCase("480", nameof(Duration.N2))]
        [TestCase("1920", nameof(Duration.NDBL))]
        [TestCase("10", nameof(Duration.N64TRP))]
        [TestCase("80", nameof(Duration.N8TRP))]
        [TestCase("320", nameof(Duration.N2TRP))]
        [TestCase("90", nameof(Duration.N16DOT))]
        [TestCase("720", nameof(Duration.N2DOT))]
        [TestCase(" 240 ", nameof(Duration.N4))]
        public void Parse_XmlHasOneDuration_OutputContainsMatchingDuration(string duration, string durationName)
        {
            string MUSIC_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise>
  <part id=""P13"">
    <measure>
      <attributes>
        <divisions>240</divisions>
      </attributes>
      <note>
        <pitch>
          <step>C</step>
          <octave>4</octave>
          </pitch>
        <duration>" + duration + @"</duration>
        <voice>5</voice>
      </note>
    </measure>
  </part>
</score-partwise>";
            string EXPECTED_TEXT =
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
       BYTE C2," + durationName + @"
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