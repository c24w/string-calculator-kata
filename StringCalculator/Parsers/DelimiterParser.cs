using System.Collections.Generic;
using System.Linq;
using StringCalculator.DataContainers;

namespace StringCalculator.Parsers
{
	public abstract class DelimiterParser
	{
		protected string Data;
		protected CapturedData CapturedData;
		public const char UniversalDelimiter = '\n';
		public IEnumerable<int> Numbers { get; protected set; }

		public abstract void Parse(string data, CapturedData capturedData);

		public static IEnumerable<int> ParseIntegers(IEnumerable<string> values)
		{
			return values.Select(int.Parse).Where(i => i < 1000);
		}
	}
}