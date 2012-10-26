using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.PatternMatching
{
	public class CapturedData
	{
		private readonly GroupCollection _matchGroups;

		public CapturedData(GroupCollection matchGroups)
		{
			_matchGroups = matchGroups;
		}

		private IEnumerable<string> GetCapturedValues(CaptureGroups captureGroup)
		{
			var captureCollection = _matchGroups[captureGroup.ToString()].Captures;
			for (var i = 0; i < captureCollection.Count; i++)
				yield return captureCollection[i].Value;
		}

		public IEnumerable<string> DefinedDelimiters
		{
			get { return GetCapturedValues(CaptureGroups.DelimitersDefinition); }
		}

		public IEnumerable<string> UsedDelimiters
		{
			get { return GetCapturedValues(CaptureGroups.DelimitersUsed); }
		}

		public IEnumerable<string> Numbers
		{
			get { return GetCapturedValues(CaptureGroups.Numbers); }
		}
	}
}