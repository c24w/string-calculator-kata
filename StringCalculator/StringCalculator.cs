using System.Linq;

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
			var parser = new Parser(_data);
			parser.Parse();
			return parser.Numbers.Sum();
		}
	}
}