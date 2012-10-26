using StringCalculator.DataContainers;
using StringCalculator.Validation;

namespace StringCalculator.Parsers
{
	class CustomDelimiterParser : Parser
	{
		private readonly ICapturedDataValidator _capturedDataValidator;

		public CustomDelimiterParser(string rawData, CapturedData capturedData, ICapturedDataValidator capturedDataValidator)
			: base(rawData, capturedData)
		{
			_capturedDataValidator = capturedDataValidator;
		}

		public CustomDelimiterParser(string rawData, CapturedData capturedData)
			: this(
				rawData,
				capturedData,
				new CapturedDataValidator(rawData, capturedData)
			) { }

		public override void Parse()
		{
			_capturedDataValidator.Validate();
			Numbers = ParseIntegers(CapturedData.Numbers);
		}
	}
}