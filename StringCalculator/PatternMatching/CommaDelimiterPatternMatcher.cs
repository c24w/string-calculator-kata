using StringCalculator.Parsers;

namespace StringCalculator.PatternMatching
{
	public class CommaDelimiterPatternMatcher : PatternMatcher
	{
		private static readonly string DelimPattern = string.Format(
			"({0}|{1})", ',', Parser.UniversalDelimiter
		);

		private static readonly string Pattern = string.Format(
			@"^(?<{0}>-?\d+)({1}(?<{0}>-?\d+))*$",
			CaptureGroups.Numbers,
			DelimPattern
		);

		public CommaDelimiterPatternMatcher(string data) : base(data, Pattern) { }
	}
}