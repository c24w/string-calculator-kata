using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator
{
	public class CommaDelimiterPatternMatcher : IPatternMatcher
	{
		private const string NumbersCaptureGroup = "nums";

		private static readonly string DelimPattern = string.Format(
			"({0}|{1})", ',', BaseParser.ConstDelimiter
		);

		private static readonly string Pattern = string.Format(
			@"^(?<{0}>-?\d+)({1}(?<{0}>-?\d+))*$",
			NumbersCaptureGroup,
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

		public IEnumerable<string> GetCapturedNumbers()
		{
			return GetCapturedValues(NumbersCaptureGroup);
		}

		public IEnumerable<string> GetCapturedValues(string captureGroup)
		{
			var captureCollection = _match.Groups[captureGroup].Captures;
			for (var i = 0; i < captureCollection.Count; i++)
				yield return captureCollection[i].Value;
		}
	}
}