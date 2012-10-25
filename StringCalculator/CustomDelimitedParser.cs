using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	class CustomDelimitedParser : Parser
	{
		private readonly CustomDelimitedSyntaxMatcher _customDelimSyntaxMatcher;

		public CustomDelimitedParser(string data, CustomDelimitedSyntaxMatcher customDelimSyntaxMatcher) : base(data)
		{
			_customDelimSyntaxMatcher = customDelimSyntaxMatcher;
		}

		public override void Parse()
		{
			var customDelimMatcher = new CustomDelimitedSyntaxMatcher(Data);
			var capturedDelims = customDelimMatcher.CapturedDelimitersDefinition;
			var capturedValues = customDelimMatcher.CapturedValues;

			var delimiters = SplitDelimiters(capturedDelims);

			if (!OnlyDefinedDelimitersAreUsed(delimiters, capturedValues))
				throw new UnparseableDataException(Data).UndefinedDelimiter();

			var values = SplitValuesOnDelimiters(delimiters, capturedValues);

			Numbers = ParseToIntegers(values);
		}

		private static string[] SplitDelimiters(string capturedDelimiters)
		{
			return capturedDelimiters.Split(new[] { "][" }, StringSplitOptions.None);
		}

		private static IEnumerable<string> SplitValuesOnDelimiters(IEnumerable<string> delimiters, string capturedValues)
		{
			var delims = new List<string>(delimiters)
            {
                new string(ConstDelimiter, 1)
            };
			return capturedValues.Split(delims.ToArray(), StringSplitOptions.None);
		}

		private bool OnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
		{
			;
			var capturedDelims = _customDelimSyntaxMatcher.GetCapturedDelimiters();

			foreach (var capturedDelim in capturedDelims)
			{
				if (!delimiters.Any(capturedDelim.Equals))
					return false;
			}
			return true;
		}
	}
}