using FluentAssertions;
using MuseScoreParser;
using MuseScoreParser.Enums;
using MuseScoreParser.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    //TODO: Write failure cases where expected elements are missing
    //TODO: Write case that includes a rest
    public class NoteParserTests
    {
        private static NewChord GenerateSingleNoteChord(string step, string alter, string octave, string duration)
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
                        Duration = duration
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
                <duration>6</duration>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>D</step>
                    <alter>1</alter>
                    <octave>5</octave>
                </pitch>
                <duration>12</duration>
                <voice>1</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>4</octave>
                </pitch>
                <duration>6</duration>
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
                                            GenerateSingleNoteChord("E", "", "5", "6"),
                                            GenerateSingleNoteChord("D", "1", "5", "12"),
                                            GenerateSingleNoteChord("A", "1", "4", "6")
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
                                            GenerateSingleNoteChord("B", "", "3", "24"),
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
                <duration>6</duration>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>E</step>
                    <octave>3</octave>
                </pitch>
                <duration>6</duration>
                <voice>1</voice>
            </note>
            <note>
                <chord/>
                <pitch>
                    <step>C</step>
                    <octave>3</octave>
                </pitch>
                <duration>6</duration>
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
                                                        Duration = "6",
                                                        Alter = string.Empty
                                                    },
                                                    new NewNote
                                                    {
                                                        Step = "E",
                                                        Octave = "3",
                                                        Duration = "6",
                                                        Alter = string.Empty
                                                    },
                                                    new NewNote
                                                    {
                                                        Step = "C",
                                                        Octave = "3",
                                                        Duration = "6",
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
                <duration>4</duration>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>8</duration>
                <voice>I</voice>
            </note>
            <backup>
                <duration>12</duration>
            </backup>
            <note>
                <pitch>
                    <step>F</step>
                    <alter>1</alter>
                    <octave>1</octave>
                </pitch>
                <duration>8</duration>
                <voice>V</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <duration>4</duration>
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
                                            GenerateSingleNoteChord("B", "-1", "2", "4"),
                                            GenerateSingleNoteChord("A", "1", "2", "8")
                                        }
                                    }
                                },
                                {
                                    "V",
                                    new NewVoice
                                    {
                                        Chords = new List<NewChord>
                                        {
                                            GenerateSingleNoteChord("F", "1", "1", "8"),
                                            GenerateSingleNoteChord("A", "", "1", "4")
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
                <duration>4</duration>
                <voice>I</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <alter>1</alter>
                    <octave>2</octave>
                </pitch>
                <duration>8</duration>
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
                <duration>8</duration>
                <voice>II</voice>
            </note>
            <note>
                <pitch>
                    <step>A</step>
                    <octave>1</octave>
                </pitch>
                <duration>4</duration>
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
                                            GenerateSingleNoteChord("B", "-1", "2", "4"),
                                            GenerateSingleNoteChord("A", "1", "2", "8")
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
                                            GenerateSingleNoteChord("F", "1", "1", "8"),
                                            GenerateSingleNoteChord("A", "", "1", "4")
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