using System;
using System.Collections.Generic;
using System.Linq;
using StringCalculator.PatternMatching;

namespace StringCalculator.Parsers
{
	class CustomDelimiterParser : Parser
	{
		private readonly CustomDelimiterPatternMatcher _customDelimPatternMatcher;

		public CustomDelimiterParser(string data, CustomDelimiterPatternMatcher customDelimPatternMatcher)
			: base(data)
		{
			_customDelimPatternMatcher = customDelimPatternMatcher;
		}

		public override void Parse()
		{
			var delimiters = _customDelimPatternMatcher.GetCapturedDelimitersDefinition().ToArray();

			var undefinedDelims = GetUndefinedDelimiters(delimiters).ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(Data).UndefinedDelimiters(undefinedDelims);

			var values = SplitValuesOnDelimiters(delimiters);

			Numbers = ParseIntegers(values);
		}

		private IEnumerable<string> SplitValuesOnDelimiters(IEnumerable<string> delimiters)
		{
			var delims = new List<string>(delimiters) { ConstDelimiter.ToString() }.ToArray();
			var capturedDelimitedValues = _customDelimPatternMatcher.GetCapturedDelimitedNumbers();
			return capturedDelimitedValues.Split(delims, StringSplitOptions.None);
		}

		private IEnumerable<string> GetUndefinedDelimiters(IEnumerable<string> definedDelimiters)
		{
			var usedDelimiters = _customDelimPatternMatcher.GetCapturedDelimiters();
			return usedDelimiters.Where(d => !definedDelimiters.Contains(d));
		}
	}
}