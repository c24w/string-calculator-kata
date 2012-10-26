using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringCalculator.Parsers;

namespace StringCalculator.PatternMatching
{
	class CustomDelimiterPatternMatcher : IPatternMatcher
	{
		private struct CaptureGroups
		{
			public const string DelimitersDefinition = "delimsDef";
			public const string Numbers = "nums";
			public const string DelimitersUsed = "delimsUsed";
		}

		/*   capture 1 char | >=1 char(s) in []     capture int(s) (length >=1) (possibly negative) delimited by char(s) (length >=1)
		/*   note: .+? = lazy, i.e. won't consume the subsequent ] or subsequent -  */
		private static readonly string Pattern = string.Format(
			@"^//((?<{0}>.)|(\[(?<{0}>.+?)\])+)\n(?<{1}>-?\d+)(((?<{2}>.+?)|{3})(?<{1}>-?\d+))*$",
			CaptureGroups.DelimitersDefinition,
			CaptureGroups.Numbers,
			CaptureGroups.DelimitersUsed,
			Parser.UniversalDelimiter
		);

		private readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);
		private readonly Match _match;

		public CustomDelimiterPatternMatcher(string testSubject)
		{
			_match = _regex.Match(testSubject);
		}

		public bool Success
		{
			get { return _match.Success; }
		}

		public IEnumerable<string> GetCapturedDefinedDelimiters()
		{
			return GetCapturedValues(CaptureGroups.DelimitersDefinition);
		}

		public IEnumerable<string> GetCapturedUsedDelimiters()
		{
			return GetCapturedValues(CaptureGroups.DelimitersUsed);
		}

		public IEnumerable<string> GetCapturedNumbers()
		{
			return GetCapturedValues(CaptureGroups.Numbers);
		}

		public IEnumerable<string> GetCapturedValues(string captureGroup)
		{
			var captureCollection = _match.Groups[captureGroup].Captures;
			for (var i = 0; i < captureCollection.Count; i++)
				yield return captureCollection[i].Value;
		}
	}
}