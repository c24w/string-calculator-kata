using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Parser
    {
        protected readonly string Data;
        public IEnumerable<int> Numbers { get; set; }
        protected const char ConstDelimiter = '\n';

        public Parser(string data)
        {
            Data = data;
        }

        public virtual void Parse()
        {
            Parser parser;
            
            if(IsCommaDelimited())
                parser = new CommaDelimitedParser(Data);
            else
                parser = new CustomDelimiterParser(Data);

            parser.Parse();
            Numbers = parser.Numbers;
            ValidateParsedNumbers(Numbers);
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
            return RegexPatterns.MatchCommaDelimitedSyntax.Match(Data).Success;
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
                throw new UnparseableDataException(Data).ContainsNegatives(negatives);
            }
        }
    }
}