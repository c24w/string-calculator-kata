using System.Text.RegularExpressions;

namespace StringCalculator
{
	public class RegexPatterns
	{
		public class CaptureGroups
		{
			public const string DelimitersDefinition = "delimDef";
			public const string DelimitedNumbers = "delimNums";
			public const string DelimitersUsed = "delims";
		}

		private static readonly string MatchCustomDelimiterSyntaxPattern =
			string.Format(
				@"^//((?<{0}>.)|\[(?<{1}>.+?)\])\n(?<{2}>-?\d+(((?<delims>.+?)|{3})-?\d+)*)$",
				CaptureGroups.DelimitersDefinition,
				CaptureGroups.DelimitedNumbers,
				CaptureGroups.DelimitersUsed,
				Parser.ConstDelimiter
			);

		/*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
		/*   note: .+? = lazy, i.e. won't consume the subsequent ] or subsequent -  */


		public static readonly Regex MatchCustomDelimiterSyntax = new Regex(MatchCustomDelimiterSyntaxPattern, RegexOptions.Compiled);


		public static Regex MatchCommaDelimited()
		{
			var delimPattern = string.Format("({0}|{1})", ',', Parser.ConstDelimiter);
			var pattern = string.Format(@"^-?\d+({0}-?\d+)*$", delimPattern); // capture one or more ints (possibly negative) delimited by , or constant delimiter
			return new Regex(pattern, RegexOptions.Compiled);
		}
	}
}