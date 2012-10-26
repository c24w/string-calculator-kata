using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	class NumberValidator
	{
		private readonly string _data;
		private readonly IEnumerable<int> _numbers;

		public NumberValidator(string data, IEnumerable<int> numbers)
		{
			_data = data;
			_numbers = numbers;
		}

		public void Validate()
		{
			var negatives = GetNegativeValues(_numbers).ToArray();
			if (negatives.Any())
				throw new UnparseableDataException(_data).ContainsNegatives(negatives);
		}

		private static IEnumerable<int> GetNegativeValues(IEnumerable<int> numbers)
		{
			return numbers.Where(i => i < 0);
		}
	}
}