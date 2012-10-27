using System.Text.RegularExpressions;
using StringCalculator.DataContainers;
using StringCalculator.Parsers;

namespace StringCalculator.PatternMatching
{
	public abstract class PatternMatcher
	{
		protected Match Match;

		public void ApplyPatternToData(string data, string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Compiled);
			Match = regex.Match(data);
		}

		public abstract void ExecuteMatch(string data);

		public bool Success
		{
			get { return Match.Success; }
		}

		public virtual CapturedData GetCapturedData()
		{
			return new CapturedData(Match.Groups);
		}

	    public abstract DelimiterParser GetAssociatedParser();
	}
}