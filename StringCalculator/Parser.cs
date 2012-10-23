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
                if (!BasicSyntaxIsCorrect())
                    ThrowUnparseable("data syntax is invalid: " + _data);

                var delimCapture = RegexPatterns.BasicSyntaxPattern.Match(_data).Groups["delimDef"].Captures[0].Value;
                var delims = delimCapture.Split(new[] { "][" }, StringSplitOptions.None);

                var valuesCapture = RegexPatterns.BasicSyntaxPattern.Match(_data).Groups["delimNums"].Captures[0].Value;

                ValidateDelimitedData(delims, valuesCapture);

                values = valuesCapture.Split(delims, StringSplitOptions.None);
            }

            ParseToIntegers(values);

            ValidateParsedNumbers(Numbers);
        }

        private bool BasicSyntaxIsCorrect()
        {
            return RegexPatterns.BasicSyntaxPattern.Match(_data).Success;
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
                ThrowUnparseable("cannot contain negative numbers: " + string.Join(",", negatives));
        }

        public void ValidateDelimitedData(string[] delimiters, string delimitedValues)
        {
            EnsureOnlyDefinedDelimitersAreUsed(delimiters, delimitedValues);
        }

        private void EnsureOnlyDefinedDelimitersAreUsed(string[] definedDelimiters, string delimitedValues)
        {
            var matchDefinedDelims = RegexPatterns.OnlyAllowDefinedDelimitersPattern(definedDelimiters).Match(delimitedValues);

            if (!matchDefinedDelims.Success)
                ThrowUnparseable("number values contain an undefined delimiter");
        }

        private static void ThrowUnparseable(string reason)
        {
            throw new FormatException(string.Format("Data cannot be parsed ({0})", reason));
        }
    }
}