﻿using MusicXmlParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.SN76489Generation
{
    internal static class RepeatPopulator
    {
        internal static void PopulateRepeatLabels(List<NewPart> parsedParts, ref List<ToneGenerator> toneGenerators)
        {
            var labelPairs = new List<(string From, string To)>();
            //A dictionary of all measures that will have an Assembly Language label at beginning of the measure
            var measuresWithLabel = new Dictionary<int, string>();
            var measureCount = parsedParts.First().Measures.Count;
            FindRepeats(parsedParts, measureCount, ref labelPairs, ref measuresWithLabel);
            var generatorNumber = 0;
            var labelPrefix = "LBL";
            foreach (var toneGenerator in toneGenerators)
            {
                ++generatorNumber;
                AfixLabelsToNotes(measuresWithLabel, measureCount, generatorNumber, labelPrefix, toneGenerator);
                PopulatRepeatList(labelPairs, generatorNumber, labelPrefix, toneGenerator);
            }
        }

        private static void FindRepeats(List<NewPart> parsedParts, int measureCount, ref List<(string From, string To)> labelPairs, ref Dictionary<int, string> measuresWithLabel)
        {
            var repeatSuffix = 'A';
            var mostRecentForwardRepeat = "";
            var mostRecentVoltaBracket1 = "";
            measuresWithLabel[1] = string.Empty;
            for (var measureNumber = 1; measureNumber <= measureCount; measureNumber++)
            {
                var measure = parsedParts.First().Measures[measureNumber - 1];
                if (measure.HasVoltaBracket)
                {
                    if (!measuresWithLabel.ContainsKey(measureNumber))
                    {
                        measuresWithLabel[measureNumber] = repeatSuffix.ToString();
                        ++repeatSuffix;
                    }
                    if (measure.VoltaNumber == 1)
                    {
                        mostRecentVoltaBracket1 = measuresWithLabel[measureNumber];
                    }
                    else
                    {
                        labelPairs.Add((mostRecentVoltaBracket1, measuresWithLabel[measureNumber]));
                    }
                }
                if (measure.HasForwardRepeat)
                {
                    if (!measuresWithLabel.ContainsKey(measureNumber))
                    {
                        measuresWithLabel[measureNumber] = repeatSuffix.ToString();
                        ++repeatSuffix;
                    }
                    mostRecentForwardRepeat = measuresWithLabel[measureNumber];
                }
                if (measure.HasBackwardRepeat)
                {
                    var nextMeasure = measureNumber + 1;
                    measuresWithLabel[nextMeasure] = repeatSuffix.ToString();
                    labelPairs.Add((repeatSuffix.ToString(), mostRecentForwardRepeat));
                    ++repeatSuffix;
                }
            }
        }

        private static void AfixLabelsToNotes(Dictionary<int, string> measuresWithLabel, int measureCount, int generatorNumber, string labelPrefix, ToneGenerator toneGenerator)
        {
            foreach (var labeledMeasure in measuresWithLabel)
            {
                var label = labelPrefix + generatorNumber + labeledMeasure.Value.ToString();
                if (labeledMeasure.Key <= measureCount)
                {
                    var firstNoteInMeasure = toneGenerator.GeneratorNotes.First(n => n.StartMeasure == labeledMeasure.Key);
                    firstNoteInMeasure.Label = label;
                }
                else
                {
                    var lastNoteInMeasure = toneGenerator.GeneratorNotes.Last(n => n.EndMeasure == labeledMeasure.Key - 1);
                    lastNoteInMeasure.LabelAtEnd = label;
                }
            }
        }

        private static void PopulatRepeatList(List<(string From, string To)> labelPairs, int generatorNumber, string labelPrefix, ToneGenerator toneGenerator)
        {
            var repeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>();
            foreach (var (from, to) in labelPairs)
            {
                var fromLabel = labelPrefix + generatorNumber + from;
                var toLabel = labelPrefix + generatorNumber + to;
                repeatLabels.Add((FromThisLabel: fromLabel, JumpToThisLabel: toLabel));
            }
            toneGenerator.RepeatLabels = repeatLabels;
        }
    }
}