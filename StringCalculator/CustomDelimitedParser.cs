using System;
using System.Collections.Generic;

namespace StringCalculator
{
    class CustomDelimitedParser : Parser
    {
        public CustomDelimitedParser(string data) : base(data) { }

        public override void Parse()
        {
            var customDelimSyntaxMatcher = new CustomDelimitedSyntaxMatcher(Data);

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
            return RegexPatterns.GetDefinedDelimitersPattern(delimiters).Match(delimitedValues).Success;
        }
    }
}