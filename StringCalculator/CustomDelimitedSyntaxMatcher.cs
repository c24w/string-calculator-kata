using System.Text.RegularExpressions;

namespace StringCalculator
{
    class CustomDelimitedSyntaxMatcher
    {
        private Match _match;

        public CustomDelimitedSyntaxMatcher(string testSubject)
        {
            ApplyPattern(testSubject);
        }

        public void ApplyPattern(string testSubject)
        {
            _match = RegexPatterns.MatchCustomDelimiterSyntax.Match(testSubject);
        }

        public string CapturedDelimiters
        {
            get { return _match.Groups["delimDef"].Captures[0].Value; }
        }

        public string CapturedValues
        {
            get { return _match.Groups["delimNums"].Captures[0].Value; }
        }
    }
}