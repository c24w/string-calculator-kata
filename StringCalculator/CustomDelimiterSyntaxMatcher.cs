using System.Text.RegularExpressions;

namespace StringCalculator
{
    class CustomDelimiterSyntaxMatcher
    {
        private Match _match;

        public CustomDelimiterSyntaxMatcher(string testSubject)
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