using System.Collections.Generic;
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

		public bool Success
		{
			get { return _match.Success; }
		}

		private void ApplyPattern(string testSubject)
		{
			_match = RegexPatterns.MatchCustomDelimiterSyntax.Match(testSubject);
		}

		public string CapturedDelimitersDefinition
		{
			get { return _match.Groups["delimDef"].Captures[0].Value; }
		}

		public string CapturedValues
		{
			get { return _match.Groups["delimNums"].Captures[0].Value; }
		}

		public IEnumerable<string> GetCapturedDelimiters()
		{
			var delimCaptures = _match.Groups["delims"].Captures;
			for(var i = 0; i < delimCaptures.Count; i++)
				yield return delimCaptures[i].Value;
		}
	}
}