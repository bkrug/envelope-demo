using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using System.Collections;
using System.Xml.Linq;

namespace MusicXmlParser
{
    internal class AssemblyMaker
    {
        private readonly SN76489NoteGenerator _sn76489NoteGenerator;
        private readonly NewNoteParser _noteParser;
        private readonly AssemblyWriter _assemblyWriter;

        internal AssemblyMaker(NewNoteParser noteParser, SN76489NoteGenerator sn76489NoteGenerator, AssemblyWriter assemblyWriter)
        {
            _sn76489NoteGenerator = sn76489NoteGenerator;
            _noteParser = noteParser;
            _assemblyWriter = assemblyWriter;
        }

        internal void ConvertToAssembly(Options options)
        {
            var xml = XDocument.Load(options.InputFile);
            var parsedMusic = _noteParser.Parse(xml);

            var generators = _sn76489NoteGenerator.GetToneGenerators(parsedMusic.Parts, options.Label6Char);

            _assemblyWriter.WriteAssembly(generators, parsedMusic.Credits, options);
        }
    }
}
