using StringCalculator.DataContainers;

namespace StringCalculator.Validation
{
	internal interface ICapturedDataValidator
	{
		void Validate(string rawData, CapturedData capturedData);
	}
}