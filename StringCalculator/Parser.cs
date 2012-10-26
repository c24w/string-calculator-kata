using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
	public abstract class Parser
	{
		protected readonly string Data;
		public const char ConstDelimiter = '\n';
		public IEnumerable<int> Numbers { get; protected set; }

		protected Parser(string data)
		{
			Data = data;
		}

		public abstract void Parse();

		public static IEnumerable<int> ParseIntegers(IEnumerable<string> values)
		{
			return values.Select(int.Parse).Where(i => i < 1000);
		}
	}
}