using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringCalculator.Parsers;

namespace StringCalculator.PatternMatching
{
	public class CommaDelimiterPatternMatcher : IPatternMatcher
	{
		private static readonly string DelimPattern = string.Format(
			"({0}|{1})", ',', Parser.UniversalDelimiter
		);

		private static readonly string Pattern = string.Format(
			@"^(?<{0}>-?\d+)({1}(?<{0}>-?\d+))*$",
			CaptureGroups.Numbers,
			DelimPattern
		);

		private readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);
		private readonly Match _match;

		public CommaDelimiterPatternMatcher(string data)
		{
			_match = _regex.Match(data);
		}

		public bool Success
		{
			get { return _match.Success; }
		}

		public CapturedData GetCapturedData()
		{
			return new CapturedData(_match.Groups);
		}
	}
}