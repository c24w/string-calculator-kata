using System.Collections.Generic;
using System.Linq;
using StringCalculator.PatternMatching;

namespace StringCalculator.Parsers
{
	class CustomDelimiterParser : Parser
	{
		private readonly CapturedData _capturedData;

		public CustomDelimiterParser(string rawData, CapturedData capturedData)
			: base(rawData)
		{
			_capturedData = capturedData;
		}

		public override void Parse()
		{
			CheckForUndefinedDelimiters(_capturedData.DefinedDelimiters);
			Numbers = ParseIntegers(_capturedData.Numbers);
		}

		private void CheckForUndefinedDelimiters(IEnumerable<string> definedDelimiters)
		{
			var usedDelims = _capturedData.UsedDelimiters;
			var undefinedDelims = usedDelims.Where(d => !definedDelimiters.Contains(d)).ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(Data).UndefinedDelimiters(undefinedDelims);
		}
	}
}