using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringCalculator
{
    class CustomDelimitedParser : Parser
    {
        public CustomDelimitedParser(string data) : base(data) { }

        public override void Parse()
        {
            var customDelimMatcher = new CustomDelimitedSyntaxMatcher(Data);
            var capturedDelims = customDelimMatcher.CapturedDelimiters;
            var capturedValues = customDelimMatcher.CapturedValues;

            var delimiters = SplitDelimiters(capturedDelims);

            if (!OnlyDefinedDelimitersAreUsed(delimiters, capturedValues))
                throw new UnparseableDataException(Data).UndefinedDelimiter();

            var values = SplitValuesOnDelimiters(delimiters, capturedValues);

            Numbers = ParseToIntegers(values);
        }

        private static string[] SplitDelimiters(string capturedDelimiters)
        {
            return capturedDelimiters.Split(new[] { "][" }, StringSplitOptions.None);
        }

        private static IEnumerable<string> SplitValuesOnDelimiters(string[] delimiters, string capturedValues)
        {
            var delims = new List<string>(delimiters)
            {
                new string(ConstDelimiter, 1)
            };
            return capturedValues.Split(delims.ToArray(), StringSplitOptions.None);
        }

        private static bool OnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
        {
            return RegexPatterns.EnforceValuesDelimitedByDefinedDelimiters(delimiters).Match(delimitedValues).Success;
        }
    }
}