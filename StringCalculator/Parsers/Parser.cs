using System.Collections.Generic;
using System.Linq;
using StringCalculator.DataContainers;

namespace StringCalculator.Parsers
{
	public abstract class Parser
	{
		protected readonly string Data;
		protected readonly CapturedData CapturedData;
		public const char UniversalDelimiter = '\n';
		public IEnumerable<int> Numbers { get; protected set; }

		protected Parser(string data)
		{
			Data = data;
		}

		protected Parser(string data, CapturedData capturedData)
			: this(data)
		{
			CapturedData = capturedData;
		}

		public abstract void Parse();

		public static IEnumerable<int> ParseIntegers(IEnumerable<string> values)
		{
			return values.Select(int.Parse).Where(i => i < 1000);
		}
	}
}