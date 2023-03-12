using MusicXmlParser.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace MusicXmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = GetOptions(args);
            var xml = XDocument.Load(options.InputFile);
            var credits = GetCredits(xml);
            var notes = NoteParser.GetNotes(xml, options.ShortLabel, out var repeatLabels);
            FileWriter.WriteFile(options, credits, notes, repeatLabels);
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

            return options;
        }

        private static Credits GetCredits(XDocument xml)
        {
            var credits = new Credits();

            var work = xml.Root.Descendants("work").Descendants("work-title").FirstOrDefault();
            if (work != null)
            {
                credits.WorkTitle = work.Value;
            }

            var identification = xml.Root.Descendants("identification").FirstOrDefault();
            if (identification != null)
            {
                var creator = identification.Descendants("creator").FirstOrDefault();
                if (creator != null) credits.Creator = creator.Value;
                var source = identification.Descendants("source").FirstOrDefault();
                if (source != null) credits.Source = source.Value;
            }
            return credits;
        }
    }
}
