using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	public class BaseParser : Parser
	{
		public BaseParser(string data) : base(data) { }

		public override void Parse()
		{
			if (Data.Equals(string.Empty))
			{
				Numbers = new[] { 0 };
				return;
			}

			var parser = SelectParser();
			parser.Parse();
			Numbers = parser.Numbers;
			new NumberValidator(Data, Numbers).Validate();
		}

		private Parser SelectParser()
		{
			var commaDelimSyntaxMatcher = new CommaDelimiterPatternMatcher(Data);
			if (commaDelimSyntaxMatcher.Success)
				return new CommaDelimiterParser(Data, commaDelimSyntaxMatcher);

			var customDelimSyntaxMatcher = new CustomDelimiterPatternMatcher(Data);
			if (customDelimSyntaxMatcher.Success)
				return new CustomDelimiterParser(Data, customDelimSyntaxMatcher);

			throw new UnparseableDataException(Data).InvalidSyntax();
		}
	}
}