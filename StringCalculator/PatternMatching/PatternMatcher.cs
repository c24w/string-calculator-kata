using System.Text.RegularExpressions;
using StringCalculator.DataContainers;

namespace StringCalculator.PatternMatching
{
	public abstract class PatternMatcher
	{
		protected readonly Match Match;

		protected PatternMatcher(string data, string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Compiled);
			Match = regex.Match(data);
		}

		public bool Success
		{
			get { return Match.Success; }
		}

		public virtual CapturedData GetCapturedData()
		{
			return new CapturedData(Match.Groups);
		}
	}
}