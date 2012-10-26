using System.Collections.Generic;

namespace StringCalculator.PatternMatching
{
	public interface IPatternMatcher
	{
		bool Success { get; }
		CapturedData GetCapturedData();
	}
}