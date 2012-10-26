using System.Text.RegularExpressions;

namespace StringCalculator.DataContainers
{
	public class CustomDelimiterCapturedData : CapturedData
	{
		public CustomDelimiterCapturedData(GroupCollection matchGroups) : base(matchGroups) { }
		
	}
}