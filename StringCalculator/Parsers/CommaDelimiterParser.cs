using StringCalculator.PatternMatching;

namespace StringCalculator.Parsers
{
	class CommaDelimiterParser : Parser
	{
		private readonly CapturedData _capturedData;

		public CommaDelimiterParser(string rawData, CapturedData capturedData)
			: base(rawData)
		{
			_capturedData = capturedData;
		}

		public override void Parse()
		{
			Numbers = ParseIntegers(_capturedData.Numbers);
		}
	}
}