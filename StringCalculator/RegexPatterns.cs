using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class RegexPatterns
    {
        private static readonly string MatchCustomDelimiterSyntaxPattern =
            string.Format(@"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+((.+|{0})-?\d+)*)$", Parser.ConstDelimiter);

                          /*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
                          /*   note: .+? = lazy, i.e. won't consume the subsequent ]   */


        public static readonly Regex MatchCustomDelimiterSyntax = new Regex(MatchCustomDelimiterSyntaxPattern, RegexOptions.Compiled);


        public static Regex EnforceValuesDelimitedByDefinedDelimiters(params string[] delimiters)
        {
            var delimiterPattern = Regex.Escape(string.Join("|", delimiters));
            var pattern = string.Format(@"^-?\d+(({0}|{1})-?\d+)*$", delimiterPattern, Parser.ConstDelimiter); // capture one or more ints (possibly negative) each delimited by any string from string[] argument
            return new Regex(pattern, RegexOptions.Compiled);
        }

        
        public static Regex EnforceValuesDelimitedByDefinedDelimiters(params char[] delimiters)
        {
            string[] toStrings = delimiters.Select(c => c.ToString(CultureInfo.InvariantCulture)).ToArray();
            return EnforceValuesDelimitedByDefinedDelimiters(toStrings);
        }
    }
}