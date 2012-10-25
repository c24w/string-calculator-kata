using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class MultipleCustomDelimiterTests
	{
		[Test]
		[TestCase(1)]
		[TestCase(234)]
		public void Multiple_custom_delimited_single_number_less_than_1000_returns_that_number(int number)
		{
			var sum = new StringCalculator("//[a][bc]\n" + number).Sum();
			Assert.That(sum, Is.EqualTo(number));
		}

		[Test]
		[TestCase("//[?][]^^]\n1]^^2?3\n4", 10)]
		[TestCase("//[@:~][[]]\n12@:~345\n6", 363)]
		[TestCase("//[()()][***]\n12***345()()6", 363)]
		public void Multiple_custom_delimiter_and_new_line_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expectedSum));
		} 
	}
}