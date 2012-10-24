using System;
using System.Collections.Generic;

namespace StringCalculator
{
    class CustomDelimiterParser : Parser
    {
        public CustomDelimiterParser(string data) : base(data) { }

        public override void Parse()
        {
            var customDelimSyntaxMatcher = new CustomDelimiterSyntaxMatcher(Data);

            if (!customDelimSyntaxMatcher.Success)
                throw new UnparseableDataException(Data).InvalidSyntax();

            var capturedDelimiters = customDelimSyntaxMatcher.CapturedDelimiters;
            var capturedValues = customDelimSyntaxMatcher.CapturedValues;

            var delimiters = SplitDelimiters(capturedDelimiters);

            if (!OnlyDefinedDelimitersAreUsed(delimiters, capturedValues))
                throw new UnparseableDataException(Data).UndefinedDelimiter();

            var values = SplitValuesOnDelimiters(delimiters, capturedValues);

            Numbers = ParserTools.ParseToIntegers(values);
        }

        private static string[] SplitDelimiters(string capturedDelimiters)
        {
            return capturedDelimiters.Split(new[] { "][" }, StringSplitOptions.None);
        }

        private static IEnumerable<string> SplitValuesOnDelimiters(string[] delimiters, string capturedValues)
        {
            return capturedValues.Split(delimiters, StringSplitOptions.None);
        }

        private static bool OnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
        {
            return RegexPatterns.MatchDefinedDelimiters(delimiters).Match(delimitedValues).Success;
        }
    }
}