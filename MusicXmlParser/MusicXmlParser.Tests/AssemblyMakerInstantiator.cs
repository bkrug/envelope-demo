using Moq;
using MusicXmlParser.SN76489Generation;
using System.IO;

namespace MusicXmlParser.Tests
{
    internal class AssemblyMakerInstantiator
    {
        internal readonly Mock<ILogger> Logger = new Mock<ILogger>();
        internal readonly MemoryStream MemoryStream = new MemoryStream();

        internal AssemblyMaker GetAssemblyMaker()
        {
            return new AssemblyMaker(new NoteParser(Logger.Object), new SN76489NoteGenerator(Logger.Object), new AssemblyWriter());
        }

        internal string GetContentsOfMemoryStream()
        {
            MemoryStream.Position = 0;
            using var streamReader = new StreamReader(MemoryStream);
            return streamReader.ReadToEnd();
        }
    }
}