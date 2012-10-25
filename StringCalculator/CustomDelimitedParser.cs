using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringCalculator
{
	class CustomDelimitedParser : Parser
	{
		private readonly CustomDelimitedSyntaxMatcher _customDelimSyntaxMatcher;

		public CustomDelimitedParser(string data, CustomDelimitedSyntaxMatcher customDelimSyntaxMatcher)
			: base(data)
		{
			_customDelimSyntaxMatcher = customDelimSyntaxMatcher;
		}

		public override void Parse()
		{
			var delimiters = SplitDelimiters();

			if (!OnlyDefinedDelimitersAreUsed(delimiters))
				throw new UnparseableDataException(Data).UndefinedDelimiter();

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

		private bool OnlyDefinedDelimitersAreUsed(IEnumerable<string> delimiters)
		{
			var capturedDelims = _customDelimSyntaxMatcher.GetCapturedDelimiters();
			return capturedDelims.All(delimiters.Contains);
		}
	}
}