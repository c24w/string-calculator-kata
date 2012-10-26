using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.CapturedData
{
	public abstract class CommaDelimiterCapturedData : CapturedData
	{
		protected CommaDelimiterCapturedData(GroupCollection regexMatchGroups) : base(regexMatchGroups) { }

		public override IEnumerable<string> DefinedDelimiters
		{
			get { yield return ","; }
		}

		public override IEnumerable<string> UsedDelimiters
		{
			get { yield return ","; }
		}
	}
}