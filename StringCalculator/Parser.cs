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
            var parser = SelectParser();
            parser.Parse();
            Numbers = parser.Numbers;
            ValidateParsedNumbers(Numbers);
        }

        private Parser SelectParser()
        {
            if (IsCommaDelimited())
                return new CommaDelimitedParser(Data);

            if (IsCustomDelimited())
                return new CustomDelimitedParser(Data);

            throw new UnparseableDataException(Data).InvalidSyntax();
        }

        private bool IsCommaDelimited()
        {
            return RegexPatterns.GetDefinedDelimitersPattern(",").Match(Data).Success;
        }

        private bool IsCustomDelimited()
        {
            return RegexPatterns.MatchCustomDelimiterSyntax.Match(Data).Success;
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