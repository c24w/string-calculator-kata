using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class RegexPatterns
    {                                                               //   capture single char or [char/string]    capture one or more ints (possibly negative); multiple ints delimited by char/string or \n
        public static readonly Regex BasicSyntaxPattern = new Regex(@"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+((.+|\n)-?\d+)*)$", RegexOptions.Compiled);

        // capture one or more ints (possibly negative) - \n or comma-delimited
        public static readonly Regex CommaDelimitedPattern = new Regex(@"^-?\d+(,-?\d+)*$", RegexOptions.Compiled);

        public static Regex OnlyAllowDefinedDelimitersPattern(string[] definedDelimiters)
        {
            var orPatternForDelims = Regex.Escape(string.Join("|", definedDelimiters));
            var onlyMatchDefinedDelimitersPattern = string.Format(@"^-?\d+(({0})-?\d+)*$", orPatternForDelims);
            return new Regex(onlyMatchDefinedDelimitersPattern, RegexOptions.Compiled);
        }
    }
}