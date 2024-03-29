﻿using MusicXmlParser.Enums;
using MusicXmlParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicXmlParser.SN76489Generation
{

    internal static class RepeatPopulator
    {
        internal static void PopulateRepeatLabels(List<Part> parsedParts, string labelPrefix, Options options, ref List<ToneGenerator> toneGenerators)
        {
            var measureCount = parsedParts.First().Measures.Count;
            FindRepeats(parsedParts, measureCount, options, out var labelPairs, out var measuresWithLabel);
            var generatorNumber = 0;
            labelPrefix = labelPrefix.Length > 4 ? labelPrefix[..4] : labelPrefix;
            foreach (var toneGenerator in toneGenerators)
            {
                ++generatorNumber;
                AfixLabelsToNotes(measuresWithLabel, measureCount, generatorNumber, labelPrefix, toneGenerator);
                PopulatRepeatList(labelPairs, generatorNumber, labelPrefix, toneGenerator, options);
            }
        }

        private static void FindRepeats(
            List<Part> parsedParts,
            int measureCount, Options options,
            out List<(string From, string To)> labelPairs,
            out Dictionary<int, string> measuresWithLabel)
        {
            labelPairs = new List<(string From, string To)>();
            //A dictionary of all measures that will have an Assembly Language label at beginning of the measure
            measuresWithLabel = new Dictionary<int, string>();

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
                if (measureNumber == measureCount)
                {
                    var nextMeasure = measureNumber + 1;
                    var jumpToBeginningExists = labelPairs.LastOrDefault().To == measuresWithLabel[1];
                    var jumpFromFinishExists = measuresWithLabel.ContainsKey(nextMeasure) && labelPairs.LastOrDefault().From == measuresWithLabel[nextMeasure];
                    if (options.RepetitionType == RepetitionType.StopAtEnd && !jumpFromFinishExists)
                    {
                        if (!measuresWithLabel.ContainsKey(nextMeasure))
                            measuresWithLabel[nextMeasure] = repeatSuffix.ToString();
                        labelPairs.Add((measuresWithLabel[nextMeasure], Symbols.STOP));
                    }
                    else if (options.RepetitionType == RepetitionType.RepeatFromBeginning && !jumpToBeginningExists)
                    {
                        if (!measuresWithLabel.ContainsKey(nextMeasure))
                            measuresWithLabel[nextMeasure] = repeatSuffix.ToString();
                        labelPairs.Add((measuresWithLabel[nextMeasure], measuresWithLabel[1]));
                    }
                    else if (options.RepetitionType == RepetitionType.RepeatFromFirstJump && !jumpFromFinishExists)
                    {
                        if (!measuresWithLabel.ContainsKey(nextMeasure))
                            measuresWithLabel[nextMeasure] = repeatSuffix.ToString();
                        var destination = labelPairs.Any() ? labelPairs.First().To : measuresWithLabel[1];
                        labelPairs.Add((measuresWithLabel[nextMeasure], destination));
                    }
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

        private static void PopulatRepeatList(List<(string From, string To)> labelPairs, int generatorNumber, string labelPrefix, ToneGenerator toneGenerator, Options options)
        {
            var repeatLabels = new List<(string FromThisLabel, string JumpToThisLabel)>();
            foreach (var (from, to) in labelPairs)
            {
                var fromLabel = labelPrefix + generatorNumber + from;
                var toLabel = to == Symbols.STOP ? to : labelPrefix + generatorNumber + to;
                repeatLabels.Add((FromThisLabel: fromLabel, JumpToThisLabel: toLabel));
            }
            switch (options.RepetitionType)
            {
                case RepetitionType.StopAtEnd:
                    repeatLabels.Add(("REPEAT", Symbols.STOP));
                    break;
                case RepetitionType.RepeatFromBeginning:
                case RepetitionType.RepeatFromFirstJump:
                    repeatLabels.Add(("REPEAT", "REPT" + generatorNumber));
                    break;
            }
            toneGenerator.RepeatLabels = repeatLabels;
        }
    }
}