using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicXmlParser
{
    internal class AssemblyWriter
    {
        internal void WriteAssemblyToSteam(ICollection<ToneGenerator> toneGenerators, Credits credits, Options options, ref StreamWriter writer)
        {
            WriteFileHeader(credits, options, writer);
            WriteRepeats(toneGenerators, writer);
            WriteNotes(toneGenerators, options, writer);
        }

        private static void WriteFileHeader(Credits credits, Options options, StreamWriter writer)
        {
            writer.WriteLine($"       DEF  {options.AsmLabel}");
            writer.WriteLine();
            writer.WriteLine("*");
            writer.WriteLine("* This is auto-generated code.");
            writer.WriteLine("* It is only included in the repo for the convenience of people who haven't cloned it.");
            writer.WriteLine("*");
            if (!string.IsNullOrEmpty(credits?.WorkTitle))
                writer.WriteLine($"* {credits.WorkTitle}");
            if (!string.IsNullOrEmpty(credits?.Creator))
                writer.WriteLine($"* {credits.Creator}");
            if (!string.IsNullOrEmpty(credits?.Source))
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
            writer.WriteLine("* Data structures dealing with repeated music");
            writer.WriteLine($"       DATA REPT1,REPT2,REPT3");
            writer.WriteLine("* Duration ratio in 60hz environment");
            writer.WriteLine($"       DATA {options.Ratio60Hz.Replace(":", ",")}");
            writer.WriteLine("* Duration ratio in 50hz environment");
            writer.WriteLine($"       DATA {options.Ratio50Hz.Replace(":", ",")}");
            writer.WriteLine();
        }

        private static void WriteRepeats(ICollection<ToneGenerator> toneGenerators, StreamWriter writer)
        {
            var i = 0;
            foreach (var generator in toneGenerators)
            {
                ++i;
                var repeatLocations = generator.RepeatLabels;
                writer.WriteLine($"REPT{i}");
                foreach (var repeatPair in repeatLocations)
                {
                    writer.WriteLine($"       DATA {repeatPair.FromThisLabel},{repeatPair.JumpToThisLabel}");
                }
            }
            writer.WriteLine();
        }

        private static void WriteNotes(ICollection<ToneGenerator> toneGenerators, Options options, StreamWriter writer)
        {
            for (var g = 1; g <= Math.Min(3, toneGenerators.Count); ++g)
            {
                var generator = toneGenerators.ElementAt(g - 1);
                writer.WriteLine($"* Generator {g}");
                foreach (var measureGroup in generator.GeneratorNotes.GroupByMeasure())
                {
                    var startMeasure = measureGroup.First().StartMeasure;
                    var endMeasure = measureGroup.Last().EndMeasure;
                    writer.WriteLine(startMeasure == endMeasure
                        ? $"* Measure {startMeasure}"
                        : $"* Measure {startMeasure} - {endMeasure}"
                    );

                    foreach (var note in measureGroup)
                    {
                        if (!string.IsNullOrEmpty(note.Label))
                            writer.WriteLine(note.Label);

                        writer.WriteLine(GetPitchedSound(note));

                        if (!string.IsNullOrEmpty(note.LabelAtEnd))
                            writer.WriteLine(note.LabelAtEnd);
                    }
                }
                writer.WriteLine("*");
                writer.WriteLine(string.Empty);
            }
        }

        private static string GetPitchedSound(GeneratorNote note)
        {
            var durationString = Enum.IsDefined(typeof(Duration), note.Duration) ? note.Duration.ToString() : ((int)note.Duration).ToString();
            if (note.IsGraceNote)
                return $"*       BYTE {note.Pitch},{durationString}        Grace Note";
            else if (note.IsPitchValid)
                return $"       BYTE {note.Pitch},{durationString}";
            else
                return $"       BYTE {Pitch.REST},{durationString}      * Invalid: {note.Pitch}";
        }
    }
}
