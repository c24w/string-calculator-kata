using StringCalculator.DataContainers;

namespace StringCalculator.Parsers
{
	class CommaDelimiterParser : Parser
	{

		public CommaDelimiterParser(string rawData, CapturedData capturedData) : base(rawData, capturedData) { }

		public override void Parse()
		{
			Numbers = ParseIntegers(CapturedData.Numbers);
		}
	}
}