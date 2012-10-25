using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class UndelimitedDataTests
	{
		[Test]
		public void Empty_string_returns_zero()
		{
			var sum = new StringCalculator(string.Empty).Sum();
			Assert.That(sum, Is.EqualTo(0));
		}

		[Test]
		[TestCase("0", 0)]
		[TestCase("1", 1)]
		[TestCase("123", 123)]
		public void Single_number_less_than_1000_returns_that_number(string data, int expected)
		{
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expected));
		}
	}
}
