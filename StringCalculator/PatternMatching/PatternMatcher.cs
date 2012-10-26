using System.Text.RegularExpressions;
using StringCalculator.DataContainers;

namespace StringCalculator.PatternMatching
{
	public abstract class PatternMatcher
	{
		protected readonly Match _match;

		protected PatternMatcher(string data, string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Compiled);
			_match = regex.Match(data);
		}

		public bool Success
		{
			get { return _match.Success; }
		}

		public virtual CapturedData GetCapturedData()
		{
			return new CapturedData(_match.Groups);
		}
	}
}