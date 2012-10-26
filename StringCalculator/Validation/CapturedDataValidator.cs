using System.Collections.Generic;
using System.Linq;
using StringCalculator.DataContainers;

namespace StringCalculator.Validation
{
	class CapturedDataValidator : ICapturedDataValidator
	{
		private readonly string _rawData;
		private readonly CapturedData _capturedData;

		public CapturedDataValidator(string rawData, CapturedData capturedData)
		{
			_rawData = rawData;
			_capturedData = capturedData;
		}

		public void Validate()
		{
			var undefinedDelims = GetUndefinedDelimiters().ToArray();
			if (undefinedDelims.Any())
				throw new UnparseableDataException(_rawData).UndefinedDelimiters(undefinedDelims);
		}

		private IEnumerable<string> GetUndefinedDelimiters()
		{
			var definedDelims = _capturedData.DefinedDelimiters;
			var usedDelims = _capturedData.UsedDelimiters;

			return usedDelims.Where(d => !definedDelims.Contains(d)).ToArray();
		}
	}
}