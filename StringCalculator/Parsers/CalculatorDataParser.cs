using StringCalculator.PatternMatching;

namespace StringCalculator.Parsers
{
	public class CalculatorDataParser : Parser
	{
		private readonly INumberValidator _numberValidator;

		public CalculatorDataParser(string data) : this(data, new DefaultNumberValidator()) { }

		public CalculatorDataParser(string data, INumberValidator numberValidator)
			: base(data)
		{
			_numberValidator = numberValidator;
		}

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
			_numberValidator.Validate(Data, Numbers);
		}

		private Parser SelectParser()
		{
			var commaDelimSyntaxMatcher = new CommaDelimiterPatternMatcher(Data);
			if (commaDelimSyntaxMatcher.Success)
				return new CommaDelimiterParser(Data, commaDelimSyntaxMatcher.GetCapturedData());

			var customDelimSyntaxMatcher = new CustomDelimiterPatternMatcher(Data);
			if (customDelimSyntaxMatcher.Success)
				return new CustomDelimiterParser(Data, customDelimSyntaxMatcher.GetCapturedData());

			throw new UnparseableDataException(Data).InvalidSyntax();
		}
	}
}