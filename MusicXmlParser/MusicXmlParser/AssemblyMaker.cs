using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using System.IO;
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
            var xmlDocument = XDocument.Load(options.InputFile);
            var writer = File.CreateText(options.OutputFile);

            ConvertToAssembly(options, xmlDocument, ref writer);

            writer.Close();
        }

        internal void ConvertToAssembly(Options options, XDocument xDocument, ref StreamWriter streamWriter)
        {
            var parsedMusic = _noteParser.Parse(xDocument);
            var generators = _sn76489NoteGenerator.GetToneGenerators(parsedMusic, options.Label6Char, options);
            _assemblyWriter.WriteAssemblyToSteam(generators, parsedMusic.Credits, options, ref streamWriter);
        }
    }
}
