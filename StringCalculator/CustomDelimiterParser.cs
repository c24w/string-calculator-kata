using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	class CustomDelimiterParser : Parser
	{
		private readonly CustomDelimitedSyntaxMatcher _customDelimSyntaxMatcher;

		public CustomDelimiterParser(string data, CustomDelimitedSyntaxMatcher customDelimSyntaxMatcher)
			: base(data)
		{
			_customDelimSyntaxMatcher = customDelimSyntaxMatcher;
		}

		public override void Parse()
		{
			var delimiters = SplitDelimiters();

			var undefinedDelims = GetUndefinedDelimiters(delimiters).ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(Data).UndefinedDelimiters(undefinedDelims);

			var values = SplitValuesOnDelimiters(delimiters);

			Numbers = ParseToIntegers(values);
		}

		private string[] SplitDelimiters()
		{
			var capturedDelimsDefinition = _customDelimSyntaxMatcher.GetCapturedDelimitersDefinition();
			return capturedDelimsDefinition.Split(new[] { "][" }, StringSplitOptions.None);
		}

		private IEnumerable<string> SplitValuesOnDelimiters(IEnumerable<string> delimiters)
		{
			var delims = new List<string>(delimiters) { ConstDelimiter.ToString() }.ToArray();
			var capturedDelimitedValues = _customDelimSyntaxMatcher.GetCapturedDelimitedNumbers();
			return capturedDelimitedValues.Split(delims, StringSplitOptions.None);
		}

		private IEnumerable<string> GetUndefinedDelimiters(IEnumerable<string> definedDelimiters)
		{
			var usedDelimiters = _customDelimSyntaxMatcher.GetCapturedDelimiters();
			return usedDelimiters.Where(d => !definedDelimiters.Contains(d));
		}
	}
}