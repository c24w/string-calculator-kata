using System.Text.RegularExpressions;

namespace StringCalculator.DataContainers
{
	public class CustomDelimiterCapturedData : CapturedData
	{
		public CustomDelimiterCapturedData(GroupCollection regexMatchGroups) : base(regexMatchGroups) { }
		
	}
}