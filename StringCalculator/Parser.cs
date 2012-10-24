using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                values = _data.Split(new[] { ',', ConstDelimiter });
            }
            else
            {
                var customDelimSyntaxMatch = RegexPatterns.CustomDelimiterSyntaxPattern.Match(_data);

                if (!customDelimSyntaxMatch.Success)
                {
                    throw new UnparseableDataException(_data).InvalidSyntax();
                }

                var delimCapture = customDelimSyntaxMatch.Groups["delimDef"].Captures[0].Value;
                var valuesCapture = customDelimSyntaxMatch.Groups["delimNums"].Captures[0].Value;

                var delimiters = delimCapture.Split(new[] { "][" }, StringSplitOptions.None);

                values = SplitValuesOnDelimiters(delimiters, valuesCapture);
            }

            ParseToIntegers(values);

            ValidateParsedNumbers(Numbers);
        }

        private string[] SplitValuesOnDelimiters(string[] delimiters, string valuesCapture)
        {
            if (OnlyDefinedDelimitersAreUsed(delimiters, valuesCapture) == false)
            {
                throw new UnparseableDataException(_data).UndefinedDelimiter();
            }

            return valuesCapture.Split(delimiters, StringSplitOptions.None);
        }

        private bool IsCommaDelimited()
        {
            return RegexPatterns.CommaDelimitedPattern.Match(_data).Success;
        }

        private void ParseToIntegers(IEnumerable<string> values)
        {
            Numbers = values.Select(int.Parse).Where(i => i < 1000);
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

        private bool OnlyDefinedDelimitersAreUsed(string[] definedDelimiters, string delimitedValues)
        {
            var matchDefinedDelims = RegexPatterns.OnlyAllowDefinedDelimitersPattern(definedDelimiters).Match(delimitedValues);

            return matchDefinedDelims.Success;
        }
    }
}