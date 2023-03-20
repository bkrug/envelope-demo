using FluentAssertions;
using MusicXmlParser.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    //TODO: Write failure cases where expected elements are missing
    public class NoteParserTests
    {
        private static NewChord GenerateSingleNoteChord(string step, string alter, string octave, string type)
        {
            return new NewChord
            {
                Notes = new List<NewNote>
                {
                    new NewNote
                    {
                        Step = step,
                        Alter = alter,
                        Octave = octave,
                        Type = type
                    }
                }
            };
        }

        [Test]
        public void Parse_XmlContainsOnePartAndOneVoicePerMeasureAndNoChords_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
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
                    <step>D</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <type>eighth</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>4</octave>
                </pitch>
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
                <type>quarter</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("E", "", "5", "16th"),
                                            GenerateSingleNoteChord("D", "1", "5", "eighth"),
                                            GenerateSingleNoteChord("A", "1", "4", "16th")
                                        }
                                    }
                                }
                            }
                        },
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("B", "", "3", "quarter"),
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsDottedNote_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>5</octave>
                </pitch>
                <type>16th</type>
                <dot/>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "E",
                                                        Alter = string.Empty,
                                                        Octave = "5",
                                                        Type = "16th",
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
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsTripplets_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
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
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "A",
                                                        Alter = string.Empty,
                                                        Octave = "3",
                                                        Type = "16th",
                                                        IsTripplet = true
                                                    }
                                                }
                                            },
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "B",
                                                        Alter = string.Empty,
                                                        Octave = "3",
                                                        Type = "16th",
                                                        IsTripplet = true
                                                    }
                                                }
                                            },
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "C",
                                                        Alter = string.Empty,
                                                        Octave = "3",
                                                        Type = "16th",
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
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsChords_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>G</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>E</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>C</step>
                    <octave>3</octave>
                </pitch>
                <type>16th</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "G",
                                                        Octave = "3",
                                                        Type = "16th",
                                                        Alter = string.Empty
                                                    },
                                                    new NewNote
                                                    {
                                                        Step = "E",
                                                        Octave = "3",
                                                        Type = "16th",
                                                        Alter = string.Empty
                                                    },
                                                    new NewNote
                                                    {
                                                        Step = "C",
                                                        Octave = "3",
                                                        Type = "16th",
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
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsMultipleVoices_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>B</step>
                    <alter>-1</alter>
                    <octave>2</octave>
                </pitch>
                <type>half</type>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
                <type>quarter</type>
                <voice>I</voice>
            </note>
            <backup>
                <type>eighth</type>
            </backup>
            <note>
                <pitch>
                    <step>F</step>
                    <alter>1</alter>
                    <octave>1</octave>
                </pitch>
                <type>quarter</type>
                <voice>V</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <type>half</type>
                <voice>V</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "I",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("B", "-1", "2", "half"),
                                            GenerateSingleNoteChord("A", "1", "2", "quarter")
                                        }
                                    }
                                },
                                {
                                    "V",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("F", "1", "1", "quarter"),
                                            GenerateSingleNoteChord("A", "", "1", "half")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsMultipleParts_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>B</step>
                    <alter>-1</alter>
                    <octave>2</octave>
                </pitch>
                <type>half</type>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
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
                <voice>II</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <type>half</type>
                <voice>II</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "I",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("B", "-1", "2", "half"),
                                            GenerateSingleNoteChord("A", "1", "2", "quarter")
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "II",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("F", "1", "1", "quarter"),
                                            GenerateSingleNoteChord("A", "", "1", "half")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsContainsForwardRepeat_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
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
            </note>
            <note>
                <pitch>
                    <step>F</step>
                    <octave>5</octave>
                </pitch>
                <type>quarter</type>
                <voice>1</voice>
            </note>
        </measure>
    </part>
</score-partwise>";
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            HasForwardRepeat = true,
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("B", "", "5", "quarter"),
                                            GenerateSingleNoteChord("F", "", "5", "quarter")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsContainsBackwardRepeat_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <note>
                <pitch>
                    <step>G</step>
                    <octave>4</octave>
                </pitch>
                <type>quarter</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>C</step>
                    <octave>5</octave>
                </pitch>
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
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            HasBackwardRepeat = true,
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("G", "", "4", "quarter"),
                                            GenerateSingleNoteChord("C", "", "5", "quarter")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }

        [Test]
        public void Parse_XmlContainsContainsVoltaBracket_Success()
        {
            const string SOURCE_XML =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<score-partwise version=""3.1"">
    <part>
        <measure>
            <barline location=""left"">
                <ending number=""1"" type=""start"" default-y=""33.10""/>
            </barline>
            <note>
                <pitch>
                    <step>E</step>
                    <octave>3</octave>
                </pitch>
                <type>half</type>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>3</octave>
                </pitch>
                <type>half</type>
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
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            HasVoltaBracket = true,
                            VoltaNumber = 1,
                            HasBackwardRepeat = true,
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "1",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("E", "", "3", "half"),
                                            GenerateSingleNoteChord("A", "", "3", "half")
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
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
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

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
            var expectedObject = new List<NewPart>
            {
                new NewPart
                {
                    Measures = new List<NewMeasure>
                    {
                        new NewMeasure
                        {
                            Voices = new Dictionary<string, NewVoice>
                            {
                                {
                                    "5",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        Step = "A",
                                                        Alter = string.Empty,
                                                        Octave = "3",
                                                        Type = "16th"
                                                    }
                                                }
                                            },
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        IsRest = true,
                                                        Step = string.Empty,
                                                        Alter = string.Empty,
                                                        Octave= string.Empty,
                                                        Type = "16th"
                                                    }
                                                }
                                            },
                                            new NewChord
                                            {
                                                Notes = new List<NewNote>
                                                {
                                                    new NewNote
                                                    {
                                                        IsRest = true,
                                                        Step = string.Empty,
                                                        Alter = string.Empty,
                                                        Octave= string.Empty,
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
            };

            //Act
            var actualObject = new NewNoteParser().Parse(SOURCE_XML);

            //Assert
            actualObject.Parts.Should().BeEquivalentTo(expectedObject);
        }
    }
}