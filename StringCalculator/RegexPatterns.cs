using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class RegexPatterns
    {

        public static readonly Regex MatchCommaDelimitedSyntax = new Regex(
            /* capture one or more ints (possibly negative) - \n or comma-delimited */
            @"^-?\d+(,-?\d+)*$"
            , RegexOptions.Compiled
        );


        public static readonly Regex MatchCustomDelimiterSyntax = new Regex(
            @"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+((.+|\n)-?\d+)*)$"
            /*   capture 1 char | >=1 chars in []       capture >=1 ints (possibly negative) delimited by >=1 char or \n
            /*   note: .+? = lazy, i.e. won't consume the subsequent ]   */
            , RegexOptions.Compiled
        );


        public static Regex OnlyMatchDefinedDelimiters(string[] definedDelimiters)
        {
            var delimsOptionsPattern = Regex.Escape(string.Join("|", definedDelimiters));

            var onlyMatchDefinedDelimitersPattern = string.Format(@"^-?\d+(({0})-?\d+)*$", delimsOptionsPattern);

            return new Regex(onlyMatchDefinedDelimitersPattern, RegexOptions.Compiled);
        }
    }
}