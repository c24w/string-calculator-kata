using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.DataContainers
{
	public abstract class CommaDelimiterCapturedData : CapturedData
	{
		protected CommaDelimiterCapturedData(GroupCollection matchGroups) : base(matchGroups) { }

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