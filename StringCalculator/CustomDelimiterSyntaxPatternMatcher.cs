using System.Text.RegularExpressions;

namespace StringCalculator
{
    class CustomDelimiterSyntaxPatternMatcher
    {
        private Match _match;

        public CustomDelimiterSyntaxPatternMatcher(string testSubject)
        {
            ApplyPattern(testSubject);
        }

        public void ApplyPattern(string testSubject)
        {
            _match = RegexPatterns.MatchCustomDelimiterSyntax.Match(testSubject);
        }

        public bool Success
        {
            get { return _match.Success; }
        }

        public string CapturedDelimiter
        {
            get { return _match.Groups["delimDef"].Captures[0].Value; }
        }

        public string CapturedValues
        {
            get { return _match.Groups["delimNums"].Captures[0].Value; }
        }
    }
}