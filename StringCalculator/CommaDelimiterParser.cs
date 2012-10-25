using System.Collections.Generic;

namespace StringCalculator
{
	class CommaDelimiterParser : Parser
	{
		public CommaDelimiterParser(string data) : base(data) { }

		public override void Parse()
		{
			Numbers = ParseIntegers(SplitValues());
		}

		private IEnumerable<string> SplitValues()
		{
			return Data.Split(new[] { ',', ConstDelimiter });
		}
	}
}