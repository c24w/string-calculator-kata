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
			/*
			var patternParserPairs = new Dictionary<PatternMatcher, Parser>
			{
				{new CommaDelimiterPatternMatcher(Data), new CommaDelimiterParser(Data)},
				{new CustomDelimiterPatternMatcher(Data), new CustomDelimiterParser(Data)}
			};

			foreach (var patternParserPair in patternParserPairs)
			{
				var patternMatcher = patternParserPair.Key;
				if (patternMatcher.Success)
				{
					var parser = patternParserPair.Value;
					parser.SetCapturedData(patternMatcher.GetCapturedData());
					return parser;
				}
			}
			throw new UnparseableDataException(Data).InvalidSyntax();
			 */

			var commaDelimSyntaxMatcher = new CommaDelimiterPatternMatcher(data);
			if (commaDelimSyntaxMatcher.Success)
			{
				var parser = new CommaDelimiterParser();
				parser.Parse(data, commaDelimSyntaxMatcher.GetCapturedData());
				return parser;
			}

			var customDelimSyntaxMatcher = new CustomDelimiterPatternMatcher(data);
			if (customDelimSyntaxMatcher.Success)
			{
				var parser = new CustomDelimiterParser();
				parser.Parse(data, customDelimSyntaxMatcher.GetCapturedData());
				return parser;
			}

			throw new UnparseableDataException(data).InvalidSyntax();
		}
	}
}