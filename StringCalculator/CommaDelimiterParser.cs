using System.Collections.Generic;

namespace StringCalculator
{
	class CommaDelimiterParser : Parser
	{
		private readonly CommaDelimiterSyntaxMatcher _commaDelimSyntaxMatcher;

		public CommaDelimiterParser(string data, CommaDelimiterSyntaxMatcher commaDelimSyntaxMatcher) : base(data)
		{
			_commaDelimSyntaxMatcher = commaDelimSyntaxMatcher;
		}

		public override void Parse()
		{
			Numbers = ParseIntegers(_commaDelimSyntaxMatcher.GetCapturedNumbers());
		}
	}
}