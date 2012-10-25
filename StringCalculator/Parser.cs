using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	public class Parser
	{
		protected readonly string Data;
		public const char ConstDelimiter = '\n';
		public IEnumerable<int> Numbers { get; set; }

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
			ValidateParsedNumbers(Numbers);
		}

		private Parser SelectParser()
		{
			if (IsCommaDelimited())
				return new CommaDelimiterParser(Data);

			var customDelimSyntaxMatcher = new CustomDelimitedSyntaxMatcher(Data);
			if (customDelimSyntaxMatcher.Success)
				return new CustomDelimiterParser(Data, customDelimSyntaxMatcher);

			throw new UnparseableDataException(Data).InvalidSyntax();
		}

		private bool IsCommaDelimited()
		{
			return RegexPatterns.MatchCommaDelimited().Match(Data).Success;
		}

		public void ValidateParsedNumbers(IEnumerable<int> numbers)
		{
			var negatives = GetNegativeValues(numbers).ToArray();
			if (negatives.Any())
				throw new UnparseableDataException(Data).ContainsNegatives(negatives);
		}

		private static IEnumerable<int> GetNegativeValues(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i < 0);
		}

		public static IEnumerable<int> ParseIntegers(IEnumerable<string> values)
		{
			try
			{
				return values.Select(int.Parse).Where(i => i < 1000);
			}
			catch
			{
				throw new UnparseableDataException("SHIT THE BED");
			}
		}
	}
}