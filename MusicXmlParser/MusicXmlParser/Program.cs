using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;
using System;

namespace MusicXmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = GetOptions(args);
            var assemblyMaker = new AssemblyMaker(new NewNoteParser(), new SN76489NoteGenerator(), new AssemblyWriter());
            assemblyMaker.ConvertToAssembly(options);
        }

        private static Options GetOptions(string[] args)
        {
            var options = new Options();

            if (args.Length < 3 || args.Length > 5)
                throw new Exception("Useage: input-file output-file assembly-label 60hz-ratio(optional) 50hz-ratio(optional)");

            options.InputFile = args[0];
            options.OutputFile = args[1];
            options.AsmLabel = args[2];

            options.Ratio60Hz = args.Length > 3 ? args[3].Replace(":", ",") : "1:1";
            options.Ratio50Hz = args.Length > 4 ? args[4].Replace(":", ",") : "5:6";

            //TODO: Take this from command line
            options.RepetitionType = Enums.RepetitionType.RepeatFromBeginning;

            return options;
        }
    }
}
