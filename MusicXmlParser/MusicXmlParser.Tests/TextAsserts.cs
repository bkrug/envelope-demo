using NUnit.Framework;
using System;
using System.IO;

namespace MusicXmlParser.Tests
{
    public static class TextAsserts
    {
        public static void EquivalentLines(string expectedStr, string actualStr)
        {
            var expectedLines = expectedStr.Split(Environment.NewLine);
            var actualLines = actualStr.Split(Environment.NewLine);
            for(var lineNum = 0; lineNum < Math.Min(expectedLines.Length, actualLines.Length); ++lineNum)
            {
                if (!actualLines[lineNum].Equals(expectedLines[lineNum]))
                    Assert.Fail($"Expected line {lineNum + 1} to contain \"{expectedLines[lineNum]}\" {Environment.NewLine}but actually contained \"{actualLines[lineNum]}\"");
            }
            if (expectedLines.Length != actualLines.Length)
                Assert.Fail($"Expected string to have {expectedLines.Length}, but actually had {actualLines.Length}");
        }
    }
}