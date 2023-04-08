using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MusicXmlParser.Tests
{
    public static class TextAsserts
    {
        public static void EquivalentLines(string expectedStr, string actualStr)
        {
            var expectedLines = expectedStr.Split(Environment.NewLine);
            var actualLines = actualStr.Split(Environment.NewLine);
            for (var lineNum = 0; lineNum < Math.Min(expectedLines.Length, actualLines.Length); ++lineNum)
            {
                if (!actualLines[lineNum].Equals(expectedLines[lineNum]))
                {
                    var failureMsgs = new List<string>()
                    {
                        $"Expected line {lineNum + 1} to contain \"{expectedLines[lineNum]}\"",
                        $"but actually contained \"{actualLines[lineNum]}\"",
                        $"The whole text is: {actualStr}"
                    };
                    Assert.Fail(string.Join(Environment.NewLine, failureMsgs));
                }
            }
            if (expectedLines.Length != actualLines.Length)
                Assert.Fail($"Expected string to have {expectedLines.Length}, but actually had {actualLines.Length}");
        }
    }
}