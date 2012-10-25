using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator
{
	class CustomDelimitedSyntaxMatcher
	{
		private Match _match;

		private struct CaptureGroups
		{
			public const string DelimitersDefinition = "delimDef";
			public const string DelimitedNumbers = "delimNums";
			public const string DelimitersUsed = "delims";
		}

		private static readonly string Pattern =
			string.Format(
				@"^//((?<{0}>.)|\[(?<{0}>.+?)\])\n(?<{1}>-?\d+(((?<{2}>.+?)|{3})-?\d+)*)$",
				CaptureGroups.DelimitersDefinition,
				CaptureGroups.DelimitedNumbers,
				CaptureGroups.DelimitersUsed,
				Parser.ConstDelimiter
			);

		/*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
		/*   note: .+? = lazy, i.e. won't consume the subsequent ] or subsequent -  */

		public static readonly Regex Regex = new Regex(Pattern, RegexOptions.Compiled);

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
			_match = Regex.Match(testSubject);
		}

		public string GetCapturedDelimitersDefinition()
		{
			return _match.Groups[CaptureGroups.DelimitersDefinition].Captures[0].Value;
		}

		public string GetCapturedDelimitedNumbers()
		{
			return _match.Groups[CaptureGroups.DelimitedNumbers].Captures[0].Value;
		}

		public IEnumerable<string> GetCapturedDelimiters()
		{
			var delimCaptures = _match.Groups[CaptureGroups.DelimitersUsed].Captures;
			for(var i = 0; i < delimCaptures.Count; i++)
				yield return delimCaptures[i].Value;
		}
	}
}