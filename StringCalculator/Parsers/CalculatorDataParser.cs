using System.Collections.Generic;
using StringCalculator.PatternMatching;
using StringCalculator.Validation;

namespace StringCalculator.Parsers
{
    public class CalculatorDataParser
    {
        private readonly INumberValidator _numberValidator;
        public IEnumerable<int> Numbers;

        public CalculatorDataParser() : this(new DefaultNumberValidator()) { }

        public CalculatorDataParser(INumberValidator numberValidator)
        {
            _numberValidator = numberValidator;
        }

        public void Parse(string data)
        {
            if (data.Equals(string.Empty))
            {
                Numbers = new[] { 0 };
                return;
            }

            var parser = GetParser(data);
            Numbers = parser.Numbers;
            _numberValidator.Validate(data, Numbers);
        }

        private DelimiterParser GetParser(string data)
        {
            var patternMatcher = SelectPatternMatcher(data);
            var parser = patternMatcher.GetAssociatedParser();
            parser.Parse(data, patternMatcher.GetCapturedData());
            return parser;
        }

        private PatternMatcher SelectPatternMatcher(string data)
        {
            var patternMatchers = new List<PatternMatcher>
			{
				new CommaDelimiterPatternMatcher(),
				new CustomDelimiterPatternMatcher()
			};

            foreach (var patternMatcher in patternMatchers)
            {
                patternMatcher.ExecuteMatch(data);
                if (patternMatcher.Success)
                    return patternMatcher;
            }
            throw new UnparseableDataException(data).InvalidSyntax();
        }
    }
}