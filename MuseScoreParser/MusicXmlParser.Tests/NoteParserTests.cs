using FluentAssertions;
using MuseScoreParser;
using MuseScoreParser.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    //TODO: Write failure cases where expected elements are missing
    //TODO: Write case that includes a rest
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
            actualObject.Should().BeEquivalentTo(expectedObject);
        }
    }
}