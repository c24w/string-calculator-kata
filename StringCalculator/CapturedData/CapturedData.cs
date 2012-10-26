using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringCalculator.PatternMatching;

namespace StringCalculator.CapturedData
{
	public class CapturedData
	{
		protected readonly GroupCollection RegexMatchGroups;

		public CapturedData(GroupCollection regexMatchGroups)
		{
			RegexMatchGroups = regexMatchGroups;
		}

		protected IEnumerable<string> GetCapturedValues(CaptureGroups captureGroup)
		{
			var captureCollection = RegexMatchGroups[captureGroup.ToString()].Captures;
			for (var i = 0; i < captureCollection.Count; i++)
				yield return captureCollection[i].Value;
		}

		public virtual IEnumerable<string> Numbers
		{
			get { return GetCapturedValues(CaptureGroups.Numbers); }
		}

		public virtual IEnumerable<string> DefinedDelimiters
		{
			get { return GetCapturedValues(CaptureGroups.DelimitersDefinition); }
		}

		public virtual IEnumerable<string> UsedDelimiters
		{
			get { return GetCapturedValues(CaptureGroups.DelimitersUsed); }
		}
	}
}