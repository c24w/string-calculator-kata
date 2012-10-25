using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
	public class RegexPatterns
	{
		private static readonly string MatchCustomDelimiterSyntaxPattern =
			string.Format(@"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+(((?<delims>.+?)|{0})-?\d+)*)$", Parser.ConstDelimiter);

		/*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
		/*   note: .+? = lazy, i.e. won't consume the subsequent ] or subsequent -  */


		public static readonly Regex MatchCustomDelimiterSyntax = new Regex(MatchCustomDelimiterSyntaxPattern, RegexOptions.Compiled);


		public static Regex EnforceValuesDelimitedByDefinedDelimiters(params string[] delimiters)
		{
			var delims = new List<string>(delimiters.Select(Regex.Escape))
			{
				new string(Parser.ConstDelimiter, 1)
			};
			var pattern = string.Format(@"^-?\d+((?<delims>({0}))-?\d+)*$", string.Join("|", delims)); // capture one or more ints (possibly negative) arbitrarily delimited by items from string[] argument; store all delimiters in the delims capture group
			return new Regex(pattern, RegexOptions.Compiled);
		}


		public static Regex MatchCommaDelimited()
		{
			var delimPattern = string.Format("({0}|{1})", ',', Parser.ConstDelimiter);
			var pattern = string.Format(@"^-?\d+({0}-?\d+)*$", delimPattern); // capture one or more ints (possibly negative) delimited by , or constant delimiter
			return new Regex(pattern, RegexOptions.Compiled);
		}
	}
}