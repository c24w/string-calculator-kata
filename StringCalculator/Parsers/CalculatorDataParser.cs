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

			var parser = DetermineParserAndParse(data);
			Numbers = parser.Numbers;
			_numberValidator.Validate(data, Numbers);
		}

		private DelimiterParser DetermineParserAndParse(string data)
		{
			var patternMatchers = new List<PatternMatcher>
			{
				new CommaDelimiterPatternMatcher(),
				new CustomDelimiterPatternMatcher()
			};

			var delimiterParsers = new List<DelimiterParser>
			{
				new CommaDelimiterParser(),
				new CustomDelimiterParser()
			};

			foreach (var patternMatcher in patternMatchers)
			{
				patternMatcher.ExecuteMatch(data);
				if (patternMatcher.Success)
				{
					var parser = delimiterParsers[patternMatchers.IndexOf(patternMatcher)];
					parser.Parse(data, patternMatcher.GetCapturedData());
					return parser;
				}
			}
			throw new UnparseableDataException(data).InvalidSyntax();
		}
	}
}