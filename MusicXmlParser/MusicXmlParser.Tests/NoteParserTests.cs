using FluentAssertions;
using Moq;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

        [Test]
        public void Parse_XmlContainsOnePartAndOneVoicePerMeasureAndNoChords_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
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
                    <step>D</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <type>eighth</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>4</octave>
                </pitch>
                <duration>6</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
        </measure>
        <measure>
            <note>
                <pitch>
                    <step>B</step>
                    <octave>3</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("E", "", "5", Duration.N16, "16th"),
                                                GenerateSingleNoteChord("D", "1", "5", Duration.N8, "eighth"),
                                                GenerateSingleNoteChord("A", "1", "4", Duration.N16, "16th")
                                            }
                                        }
                                    }
                                }
                            },
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("B", "", "3", Duration.N4, "quarter"),
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
        public void Parse_XmlContainsDottedNote_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>48</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                </pitch>
                <duration>18</duration>
                <type>16th</type>
                <dot/>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "48",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
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
                                                            Step = "E",
                                                            Alter = string.Empty,
                                                            Octave = "5",
                                                            Type = "16th",
                                                            Duration = "18",
                                                            IsDotted = true
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
        public void Parse_XmlContainsTripplets_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>48</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <duration>8</duration>
                <time-modification>
                    <actual-notes>3</actual-notes>
                    <normal-notes>2</normal-notes>
                    </time-modification>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>B</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <duration>8</duration>
                <time-modification>
                    <actual-notes>3</actual-notes>
                    <normal-notes>2</normal-notes>
                    <extra-tag-that-should-be-ignored/>
                    </time-modification>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>C</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <duration>8</duration>
                <time-modification>
                    <normal-notes>2</normal-notes>
                    <actual-notes>3</actual-notes>
                    <if-tags-are-in-opposite-order-it-should-not-matter/>
                    </time-modification>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "48",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
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
                                                            Type = "16th",
                                                            Duration = "8",
                                                            IsTripplet = true
                                                        }
                                                    }
                                                },
                                                new Chord
                                                {
                                                    Notes = new List<Note>
                                                    {
                                                        new Note
                                                        {
                                                            Step = "B",
                                                            Alter = string.Empty,
                                                            Octave = "3",
                                                            Type = "16th",
                                                            Duration = "8",
                                                            IsTripplet = true
                                                        }
                                                    }
                                                },
                                                new Chord
                                                {
                                                    Notes = new List<Note>
                                                    {
                                                        new Note
                                                        {
                                                            Step = "C",
                                                            Alter = string.Empty,
                                                            Octave = "3",
                                                            Type = "16th",
                                                            Duration = "8",
                                                            IsTripplet = true
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
        public void Parse_XmlContainsChords_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>12</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>G</step>
                    <octave>3</octave>
                </pitch>
                <duration>2</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>E</step>
                    <octave>3</octave>
                </pitch>
                <duration>2</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>C</step>
                    <octave>3</octave>
                </pitch>
                <duration>2</duration>
                <type>16th</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "12",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
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
                                                            Step = "G",
                                                            Octave = "3",
                                                            Type = "16th",
                                                            Duration = "2",
                                                            Alter = string.Empty
                                                        },
                                                        new Note
                                                        {
                                                            Step = "E",
                                                            Octave = "3",
                                                            Type = "16th",
                                                            Duration = "2",
                                                            Alter = string.Empty
                                                        },
                                                        new Note
                                                        {
                                                            Step = "C",
                                                            Octave = "3",
                                                            Type = "16th",
                                                            Duration = "2",
                                                            Alter = string.Empty
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
        public void Parse_XmlContainsMultipleVoices_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>B</step>
                    <alter>-1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>48</duration>
                <type>half</type>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>I</voice>
            </note>
            <backup>
                <duration>72</duration>
            </backup>
            <note>
                <pitch>
                    <step>F</step>
                    <alter>1</alter>
                    <octave>1</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>V</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <duration>48</duration>
                <type>half</type>
                <voice>V</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "I",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("B", "-1", "2", Duration.N2, "half"),
                                                GenerateSingleNoteChord("A", "1", "2", Duration.N4, "quarter")
                                            }
                                        }
                                    },
                                    {
                                        "V",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("F", "1", "1", Duration.N4, "quarter"),
                                                GenerateSingleNoteChord("A", "", "1", Duration.N2, "half")
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
        public void Parse_XmlContainsMultipleParts_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>B</step>
                    <alter>-1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>48</duration>
                <type>half</type>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>I</voice>
            </note>
        </measure>
    </part>
    <part>
        <measure>
            <note>
                <pitch>
                    <step>F</step>
                    <alter>1</alter>
                    <octave>1</octave>
                </pitch>
                <type>quarter</type>
                <duration>24</duration>
                <voice>II</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <type>half</type>
                <voice>II</voice>
                <duration>48</duration>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "I",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("B", "-1", "2", Duration.N2, "half"),
                                                GenerateSingleNoteChord("A", "1", "2", Duration.N4, "quarter")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "II",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("F", "1", "1", Duration.N4, "quarter"),
                                                GenerateSingleNoteChord("A", "", "1", Duration.N2, "half")
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
        public void Parse_XmlContainsContainsForwardRepeat_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <barline location=""left"">
                <bar-style>not-relevant-data</bar-style>
                <repeat direction=""foRward""/>
            </barline>
            <note>
                <pitch>
                    <step>B</step>
                    <octave>5</octave>
                </pitch>
                <type>quarter</type>
                <voice>1</voice>
                <duration>24</duration>
            </note>
            <note>
                <pitch>
                    <step>F</step>
                    <octave>5</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                HasForwardRepeat = true,
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("B", "", "5", Duration.N4, "quarter"),
                                                GenerateSingleNoteChord("F", "", "5", Duration.N4, "quarter")
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
        public void Parse_XmlContainsContainsBackwardRepeat_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <pitch>
                    <step>G</step>
                    <octave>4</octave>
                </pitch>
                <type>quarter</type>
                <voice>1</voice>
                <duration>24</duration>
            </note>
            <note>
                <pitch>
                    <step>C</step>
                    <octave>5</octave>
                </pitch>
                <duration>24</duration>
                <type>quarter</type>
                <voice>1</voice>
            </note>
            <barline location=""right"">
                <bar-style>not-relelvant-type</bar-style>
                <repeat direction=""backWard""/>
            </barline>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                HasBackwardRepeat = true,
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("G", "", "4", Duration.N4, "quarter"),
                                                GenerateSingleNoteChord("C", "", "5", Duration.N4, "quarter")
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
        public void Parse_XmlContainsContainsVoltaBracket_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
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
            var expectedObject = new ParsedMusic {
                Divisions = "24", 
                Parts =  new List<Part>
                {
                    new Part
                    {
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
        public void Parse_XmlContainsCredits_Success()
        {
            var expectedObject = new Credits
            {
                WorkTitle = "Joe's Discount Symphony in B minor",
                Creator = "Joe. Duh!(1986-that time his Nachos were a little too spicy)",
                Source = "http://fakescore.com/user/2983/scores/17289"
            };
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <work>
        <work-title>Joe's Discount Symphony in B minor</work-title>
    </work>
    <identification>
        <creator type=""composer"">Joe. Duh!(1986-that time his Nachos were a little too spicy)</creator>
        <encoding>
            <software>MuseScore 3.6.2</software>
            <encoding-date>2022-09-12</encoding-date>
            <supports element=""accidental"" type=""yes""/>
            <supports element=""beam"" type=""yes""/>
            <supports element=""print"" attribute=""new-page"" type=""yes"" value=""yes""/>
            <supports element=""print"" attribute=""new-system"" type=""yes"" value=""yes""/>
            <supports element=""stem"" type=""yes""/>
        </encoding>
        <source>http://fakescore.com/user/2983/scores/17289</source>
    </identification>
</score-partwise>";

            //Act
            var actualObject = GetNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Credits.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsRests_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
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
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
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
        public void Parse_XmlRestHasNoTypeElement_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <rest/>
                <duration>6</duration>
                <voice>5</voice>
                <staff>2</staff>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic
            {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
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
                                                            IsRest = true,
                                                            Step = string.Empty,
                                                            Alter = string.Empty,
                                                            Octave= string.Empty,
                                                            Duration = "6",
                                                            Type = string.Empty
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
        public void Parse_XmlContainsGraceSlash_OutputsTheNotesWithDurationOfZero()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <attributes>
                <divisions>24</divisions>
            </attributes>
            <note>
                <grace slash=""yes""/>
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
                    <step>D</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <type>eighth</type>
                <voice>1</voice>
                <duration>12</duration>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>4</octave>
                </pitch>
                <type>16th</type>
                <duration>6</duration>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new ParsedMusic {
                Divisions = "24",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Measures = new List<Measure>
                        {
                            new Measure
                            {
                                Voices = new Dictionary<string, Voice>
                                {
                                    {
                                        "1",
                                        new Voice
                                        {
                                            Chords = new List<Chord>
                                            {
                                                GenerateSingleNoteChord("E", "", "5", Duration.N16, "16th", true),
                                                GenerateSingleNoteChord("D", "1", "5", Duration.N8, "eighth"),
                                                GenerateSingleNoteChord("A", "1", "4", Duration.N16, "16th")
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
<score-partwise version=""3.1"">
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
<score-partwise version=""3.1"">
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
<score-partwise version=""3.1"">
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
<score-partwise version=""3.1"">
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
    }
}