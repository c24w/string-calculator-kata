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
            var parser = IsCommaDelimited() ? (Parser) new CommaDelimitedParser(Data) : new CustomDelimiterParser(Data);
            parser.Parse();
            Numbers = parser.Numbers;
            ValidateParsedNumbers(Numbers);
        }

        private bool IsCommaDelimited()
        {
            return RegexPatterns.MatchCommaDelimitedSyntax.Match(Data).Success;
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