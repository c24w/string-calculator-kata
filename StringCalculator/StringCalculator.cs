using System.Linq;
using StringCalculator.Parsers;

namespace StringCalculator
{
	public class StringCalculator
	{
		private readonly string _data;

		public StringCalculator(string data)
		{
			_data = data;
		}

		public int Sum()
		{
			var parser = new CalculatorDataParser();
			parser.Parse(_data);
			return parser.Numbers.Sum();
		}
	}
}