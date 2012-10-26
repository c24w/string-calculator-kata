using StringCalculator.DataContainers;
using StringCalculator.Validation;

namespace StringCalculator.Parsers
{
	class CustomDelimiterParser : DelimiterParser
	{
		private readonly ICapturedDataValidator _capturedDataValidator;

		public CustomDelimiterParser(ICapturedDataValidator capturedDataValidator)
		{
			_capturedDataValidator = capturedDataValidator;
		}

		public CustomDelimiterParser() : this(new CapturedDataValidator()) { }

		public override void Parse(string rawData, CapturedData capturedData)
		{
			_capturedDataValidator.Validate(rawData, capturedData);
			Numbers = ParseIntegers(capturedData.Numbers);
		}
	}
}