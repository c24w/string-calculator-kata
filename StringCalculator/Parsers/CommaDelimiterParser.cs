using StringCalculator.DataContainers;

namespace StringCalculator.Parsers
{
	class CommaDelimiterParser : DelimiterParser
	{
		public override void Parse(string rawData, CapturedData capturedData)
		{
			Numbers = ParseIntegers(capturedData.Numbers);
		}
	}
}