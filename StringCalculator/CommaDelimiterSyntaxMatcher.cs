using System.Text.RegularExpressions;

namespace StringCalculator
{
	public interface ICommaDelimiterSyntaxMatcher : ISyntaxMatcher
	{
		void ApplyPattern(string testSubject);
	}

	public class CommaDelimiterSyntaxMatcher : ISyntaxMatcher
	{
		private static readonly string DelimPattern = string.Format("({0}|{1})", ',', Parser.ConstDelimiter);
		private static readonly string Pattern = string.Format(@"^-?\d+({0}-?\d+)*$", DelimPattern);
		private readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);
		private readonly Match _match;

		public CommaDelimiterSyntaxMatcher(string data)
		{
			_match = _regex.Match(data);
		}

		public bool Success
		{
			get { return _match.Success; }
		}
	}
}