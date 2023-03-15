using MusicXmlParser.Models;
using System.Collections.Generic;
using System.IO;

namespace MusicXmlParser
{
    internal class AssemblyWriter
    {
        internal void WriteAssembly(ICollection<ToneGenerator> toneGenerators, Options options)
        {
            var writer = File.CreateText(options.OutputFile);
            writer.Close();

        }
    }
}
