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

            var displayRepoWarningOption = new Option<bool>(
                name: "--displayRepoWarning",
                description: "if true, adds a message saying that this is generated code that is only included in the repo for others' benefit",
                getDefaultValue: () => false);

            var rootCommand = new RootCommand("MusicXml parser for TMS9900 assembly and SN76489 sound chip");
            rootCommand.AddOption(inputOption);
            rootCommand.AddOption(outputOption);
            rootCommand.AddOption(asmLabelOption);
            rootCommand.AddOption(ratio60HzOption);
            rootCommand.AddOption(ratio50HzOption);
            rootCommand.AddOption(repetitionTypeOption);
            rootCommand.AddOption(displayRepoWarningOption);

            rootCommand.SetHandler((input, output, asmLabel, ratio60Hz, ratio50Hz, repetitionType, displayRepoWarning) =>
                {
                    var options = new Options()
                    {
                        InputFile = input,
                        OutputFile = output,
                        AsmLabel = asmLabel,
                        Ratio60Hz = ratio60Hz.Replace(":", ","),
                        Ratio50Hz = ratio50Hz.Replace(":", ","),
                        RepetitionType = repetitionType, 
                        DisplayRepoWarning = displayRepoWarning
                    };
                    ConvertXmlToAssembly(options);
                },
                inputOption, outputOption, asmLabelOption, ratio60HzOption, ratio50HzOption, repetitionTypeOption, displayRepoWarningOption);

            return rootCommand.Invoke(args);
        }

        private static void ConvertXmlToAssembly(Options options)
        {
            var logger = new Logger();
            var assemblyMaker = new AssemblyMaker(new NoteParser(logger), new SN76489NoteGenerator(logger), new AssemblyWriter());
            assemblyMaker.ConvertToAssembly(options);
        }
    }
}
