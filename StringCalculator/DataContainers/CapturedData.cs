using System.Collections.Generic;
using System.Text.RegularExpressions;
using StringCalculator.PatternMatching;

namespace StringCalculator.DataContainers
{
	public class CapturedData
	{
		protected readonly GroupCollection MatchGroups;

		public CapturedData(GroupCollection matchGroups)
		{
			MatchGroups = matchGroups;
		}

		protected IEnumerable<string> GetCapturedValues(CaptureGroups captureGroup)
		{
			var captureCollection = MatchGroups[captureGroup.ToString()].Captures;
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