using Moq;
using MusicXmlParser.SN76489Generation;

namespace MusicXmlParser.Tests
{
    internal class AssemblyMakerInstantiator
    {
        public readonly Mock<ILogger> Logger = new Mock<ILogger>();

        internal AssemblyMaker GetAssemblyMaker()
        {
            return new AssemblyMaker(new NoteParser(Logger.Object), new SN76489NoteGenerator(Logger.Object), new AssemblyWriter());
        }
    }
}