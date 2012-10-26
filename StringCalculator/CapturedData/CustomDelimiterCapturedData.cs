using System.Text.RegularExpressions;

namespace StringCalculator.CapturedData
{
	public class CustomDelimiterCapturedData : CapturedData
	{
		public CustomDelimiterCapturedData(GroupCollection regexMatchGroups) : base(regexMatchGroups) { }
		
	}
}