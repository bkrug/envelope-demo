using MuseScoreParser.Models;
using System.Collections.Generic;
using System.IO;

namespace MuseScoreParser
{
    internal static class FileWriter
    {
        internal static void WriteFile(Options options, Credits credits, List<List<IAsmSymbol>> notes, List<RepeatLocations> repeatLabels)
        {
            var writer = File.CreateText(options.OutputFile);
            writer.WriteLine($"       DEF  {options.AsmLabel}");
            writer.WriteLine();
            writer.WriteLine("*");
            writer.WriteLine("* This is auto-generated code.");
            writer.WriteLine("* It is only included in the repo for the convenience of people who haven't cloned it.");
            writer.WriteLine("*");
            if (!string.IsNullOrEmpty(credits.WorkTitle))
                writer.WriteLine($"* {credits.WorkTitle}");
            if (!string.IsNullOrEmpty(credits.Creator))
                writer.WriteLine($"* {credits.Creator}");
            if (!string.IsNullOrEmpty(credits.Source))
                writer.WriteLine($"* Source: {credits.Source}");
            writer.WriteLine("*");
            writer.WriteLine();
            writer.WriteLine("       COPY 'NOTEVAL.asm'");
            writer.WriteLine("       COPY 'CONST.asm'");
            writer.WriteLine();
            writer.WriteLine("*");
            writer.WriteLine("* Song Header");
            writer.WriteLine("*");
            writer.WriteLine($"{options.Label6Char} DATA {options.ShortLabel}1,{options.ShortLabel}2,{options.ShortLabel}3");
            writer.WriteLine("* Duration ratio in 60hz environment");
            writer.WriteLine($"       DATA {options.Ratio60Hz}");
            writer.WriteLine("* Duration ratio in 50hz environment");
            writer.WriteLine($"       DATA {options.Ratio50Hz}");
            writer.WriteLine();
            for (var i = 0; i < repeatLabels.Count; ++i)
            {
                var repeatLocations = repeatLabels[i];
                writer.WriteLine($"REPT{i + 1}");
                for(var j = 0; j < repeatLocations.Labels.Count; j += 2)
                {
                    writer.WriteLine($"       DATA {repeatLocations.Labels[j]},{repeatLocations.Labels[j+1]}");
                }
            }
            writer.WriteLine();
            for (var generator = 1; generator <= 3; ++generator)
            {
                var label = options.ShortLabel + generator;
                writer.WriteLine($"* Generator {generator}");
                writer.WriteLine(label);
                foreach (var symbol in notes[generator - 1])
                {
                    writer.WriteLine(symbol.ToAsm());
                }
                writer.WriteLine("*");
                writer.WriteLine($"       DATA REPEAT,{label}");
                writer.WriteLine(string.Empty);
            }
            writer.Close();
        }
    }
}
