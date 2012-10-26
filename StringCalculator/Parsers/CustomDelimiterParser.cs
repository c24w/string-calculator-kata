using System;
using System.Collections.Generic;
using System.Globalization;
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
			var definedDelims = _customDelimPatternMatcher.GetCapturedDefinedDelimiters().ToArray();
			CheckForUndefinedDelimiters(definedDelims);
			var numbers = _customDelimPatternMatcher.GetCapturedNumbers();
			Numbers = ParseIntegers(numbers);
		}

		private void CheckForUndefinedDelimiters(IEnumerable<string> definedDelimiters)
		{
			var usedDelims = _customDelimPatternMatcher.GetCapturedUsedDelimiters();
			var undefinedDelims = usedDelims.Where(d => !definedDelimiters.Contains(d)).ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(Data).UndefinedDelimiters(undefinedDelims);
		}
	}
}