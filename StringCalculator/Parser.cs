using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Parser
    {
        private readonly string _data;
        public IEnumerable<int> Numbers { get; protected set; }
        private const char ConstDelimiter = '\n';

        public Parser(string data)
        {
            _data = data;
        }

        public void Parse()
        {
            string[] values;

            if (IsCommaDelimited())
            {
                values = SplitDataOnCommas();
            }
            else
            {
                var customDelimSyntaxMatcher = new CustomDelimiterSyntaxPatternMatcher(_data);

                if (!customDelimSyntaxMatcher.Success)
                    throw new UnparseableDataException(_data).InvalidSyntax();

                var capturedDelimiters = customDelimSyntaxMatcher.CapturedDelimiters;
                var capturedValues = customDelimSyntaxMatcher.CapturedValues;

                var delimiters = SplitDelimiters(capturedDelimiters);

                if (!OnlyDefinedDelimitersAreUsed(delimiters, capturedValues))
                    throw new UnparseableDataException(_data).UndefinedDelimiter();

                values = SplitValuesOnDelimiters(delimiters, capturedValues);
            }
            
            Numbers = ParseToIntegers(values);
            ValidateParsedNumbers(Numbers);
        }

        private string[] SplitDataOnCommas()
        {
            return _data.Split(new[] { ',', ConstDelimiter });
        }

        private static string[] SplitDelimiters(string capturedDelimiters)
        {
            return capturedDelimiters.Split(new[] { "][" }, StringSplitOptions.None);
        }

        private string[] SplitValuesOnDelimiters(string[] delimiters, string capturedValues)
        {
            return capturedValues.Split(delimiters, StringSplitOptions.None);
        }

        private bool IsCommaDelimited()
        {
            return RegexPatterns.MatchCommaDelimitedSyntax.Match(_data).Success;
        }

        private bool OnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
        {
            return RegexPatterns.MatchDefinedDelimiters(delimiters).Match(delimitedValues).Success;
        }

        public void ValidateParsedNumbers(IEnumerable<int> numbers)
        {
            CheckForNegatives(numbers);
        }

        private void CheckForNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
            {
                throw new UnparseableDataException(_data).ContainsNegatives(negatives);
            }
        }

        private IEnumerable<int> ParseToIntegers(IEnumerable<string> values)
        {
            return values.Select(int.Parse).Where(i => i < 1000);
        }
    }
}