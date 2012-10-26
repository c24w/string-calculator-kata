using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Validation
{
	class DefaultNumberValidator : INumberValidator
	{
		private readonly string _data;

		public DefaultNumberValidator(string data)
		{
			_data = data;
		}

		public void Validate(IEnumerable<int> numbers)
		{
			var negatives = GetNegativeValues(numbers).ToArray();
			if (negatives.Any())
				throw new UnparseableDataException(_data).ContainsNegatives(negatives);
		}

		private static IEnumerable<int> GetNegativeValues(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i < 0);
		}
	}
}