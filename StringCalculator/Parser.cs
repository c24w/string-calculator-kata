using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	public class Parser
	{
		protected readonly string Data;
		public const char ConstDelimiter = '\n';
		public IEnumerable<int> Numbers { get; protected set; }

		public Parser(string data)
		{
			Data = data;
		}

		public virtual void Parse()
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
			var commaDelimSyntaxMatcher = new CommaDelimiterSyntaxMatcher(Data);
			if (commaDelimSyntaxMatcher.Success)
				return new CommaDelimiterParser(Data, commaDelimSyntaxMatcher);

			var customDelimSyntaxMatcher = new CustomDelimiterSyntaxMatcher(Data);
			if (customDelimSyntaxMatcher.Success)
				return new CustomDelimiterParser(Data, customDelimSyntaxMatcher);

			throw new UnparseableDataException(Data).InvalidSyntax();
		}

		public static IEnumerable<int> ParseIntegers(IEnumerable<string> values)
		{
			return values.Select(int.Parse).Where(i => i < 1000);
		}
	}
}