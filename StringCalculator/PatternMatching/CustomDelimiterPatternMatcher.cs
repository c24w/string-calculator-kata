using StringCalculator.Parsers;

namespace StringCalculator.PatternMatching
{
	class CustomDelimiterPatternMatcher : PatternMatcher
	{
		/*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
		/*   note: .+? = lazy, i.e. won't consume the subsequent ] or subsequent -  */
		private static readonly string Pattern = string.Format(
			@"^//((?<{0}>.)|(\[(?<{0}>.+?)\])+)\n(?<{1}>-?\d+)(((?<{2}>.+?)|{3})(?<{1}>-?\d+))*$",
			CaptureGroups.DelimitersDefinition,
			CaptureGroups.Numbers,
			CaptureGroups.DelimitersUsed,
			Parser.UniversalDelimiter
		);

		public CustomDelimiterPatternMatcher(string data) : base(data, Pattern) { }
	}
}