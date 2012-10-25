using System.Text.RegularExpressions;

namespace StringCalculator
{
	public class RegexPatterns
	{
		public static Regex MatchCommaDelimited()
		{
			var delimPattern = string.Format("({0}|{1})", ',', Parser.ConstDelimiter);
			var pattern = string.Format(@"^-?\d+({0}-?\d+)*$", delimPattern); // capture one or more ints (possibly negative) delimited by , or constant delimiter
			return new Regex(pattern, RegexOptions.Compiled);
		}
	}
}