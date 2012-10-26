using System.Collections.Generic;
using StringCalculator.PatternMatching;

namespace StringCalculator.Parsers
{
	class CommaDelimiterParser : Parser
	{
		private readonly CommaDelimiterPatternMatcher _commaDelimPatternMatcher;

		public CommaDelimiterParser(string data, CommaDelimiterPatternMatcher commaDelimPatternMatcher)
			: base(data)
		{
			_commaDelimPatternMatcher = commaDelimPatternMatcher;
		}

		public override void Parse()
		{
			Numbers = ParseIntegers(_commaDelimPatternMatcher.GetCapturedNumbers());
		}
	}
}