using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class RegexPatterns
    {
        public static readonly Regex MatchCustomDelimiterSyntax = new Regex(
            @"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+((.+|\n)-?\d+)*)$"
            /*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
            /*   note: .+? = lazy, i.e. won't consume the subsequent ]   */
            , RegexOptions.Compiled
        );

        public static Regex GetDefinedDelimitersPattern(params string[] delimiters)
        {
            var delimiterPattern = Regex.Escape(string.Join("|", delimiters));
            var pattern = string.Format(@"^-?\d+(({0})-?\d+)*$", delimiterPattern); // capture one or more ints (possibly negative) delimited by contents of string[] argument
            return new Regex(pattern, RegexOptions.Compiled);
        }
    }
}