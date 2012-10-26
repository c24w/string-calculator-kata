using System.Collections.Generic;
using System.Linq;
using StringCalculator.DataContainers;

namespace StringCalculator.Parsers
{
	class CustomDelimiterParser : Parser
	{
		public CustomDelimiterParser(string rawData, CapturedData capturedData) : base(rawData, capturedData) { }

		public override void Parse()
		{
			CheckForUndefinedDelimiters(CapturedData.DefinedDelimiters);
			Numbers = ParseIntegers(CapturedData.Numbers);
		}

		private void CheckForUndefinedDelimiters(IEnumerable<string> definedDelimiters)
		{
			var usedDelims = CapturedData.UsedDelimiters;
			var undefinedDelims = usedDelims.Where(d => !definedDelimiters.Contains(d)).ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(Data).UndefinedDelimiters(undefinedDelims);
		}
	}
}