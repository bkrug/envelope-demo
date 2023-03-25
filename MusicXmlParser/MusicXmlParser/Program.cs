using System.CommandLine;
using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using MusicXmlParser.SN76489Generation;

namespace MusicXmlParser
{
    class Program
    {
        static int Main(string[] args)
        {
            var inputOption = new Option<string>(
                name: "--input",
                description: "An uncompressed MusicXml file to read")
                { IsRequired = true };

            var outputOption = new Option<string>(
                name: "--output",
                description: "Assembly language file")
                { IsRequired = true };

            var asmLabelOption = new Option<string>(
                name: "--asmLabel",
                description: "The prefix to several labels in the assemly language output")
                { IsRequired = true };

            var ratio60HzOption = new Option<string>(
                name: "--ratio60Hz",
                description: "ratio representing how much to increase the length of a note by.",
                getDefaultValue: () => "1:1");

            var ratio50HzOption = new Option<string>(
                name: "--ratio50Hz",
                description: "ratio representing how much to increase the length of a note by.",
                getDefaultValue: () => "5:6");

            var repetitionTypeOption = new Option<RepetitionType>(
                name: "--repetitionType",
                description: "specifies whether and how a song should be repeated when it completes",
                getDefaultValue: () => RepetitionType.RepeatFromBeginning);
            
            var rootCommand = new RootCommand("MusicXml parser for TMS9900 assembly and SN76489 sound chip");
            rootCommand.AddOption(inputOption);
            rootCommand.AddOption(outputOption);
            rootCommand.AddOption(asmLabelOption);
            rootCommand.AddOption(ratio60HzOption);
            rootCommand.AddOption(ratio50HzOption);
            rootCommand.AddOption(repetitionTypeOption);

            rootCommand.SetHandler((input, output, asmLabel, ratio60hz, ratio50hz, repetitionType) =>
                {
                    ConvertXmlToAssembly(input, output, asmLabel, ratio60hz, ratio50hz, repetitionType);
                },
                inputOption, outputOption, asmLabelOption, ratio60HzOption, ratio50HzOption, repetitionTypeOption);

            return rootCommand.Invoke(args);
        }

        private static void ConvertXmlToAssembly(string inputFile, string outputFile, string asmLabel, string ratio60Hz, string ratio50Hz, RepetitionType repetitionType)
        {
            var options = new Options()
            {
                InputFile = inputFile,
                OutputFile = outputFile,
                AsmLabel = asmLabel,
                Ratio60Hz = ratio60Hz.Replace(":", ","),
                Ratio50Hz = ratio50Hz.Replace(":", ","),
                RepetitionType = repetitionType
            };

            var assemblyMaker = new AssemblyMaker(new NoteParser(), new SN76489NoteGenerator(), new AssemblyWriter());
            assemblyMaker.ConvertToAssembly(options);
        }
    }
}
