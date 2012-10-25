using System.Linq;
using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class CustomStringDelimitedTests
	{
		[Test]
		[TestCase("&", 1)]
		[TestCase("##", 234)]
		public void Custom_string_delimited_single_number_less_than_1000_returns_that_number(string delimiter, int number)
		{
			var data = TestDataBuilder.GetStringDelimitedData(delimiter, number);
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(number));
		}

		[Test]
		[TestCase("!!", 1, 234, 5)]
		[TestCase("@#~", 12, 345, 6)]
		[TestCase("[]", 12, 345, 6)]
		public void Custom_string_delimited_numbers_returns_the_sum_of_values_less_than_1000(string delimiter, params int[] numbers)
		{
			var data = TestDataBuilder.GetStringDelimitedData(delimiter, numbers);
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(numbers.Sum()));
		}

		[Test]
		[TestCase("//[@:~]\n12@:~345\n6", 363)]
		[TestCase("//[[]]\n12\n345[]6", 363)]
		public void Custom_string_and_new_line_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expectedSum));
		}

		[Test]
		[TestCase(1, 1, 1000)]
		[TestCase(24, 1, 23, 4567)]
		[TestCase(1002, 1, 2, 999)]
		public void Custom_string_delimited_numbers_greater_than_999_are_ignored(int expected, params int[] numbers)
		{
			var data = TestDataBuilder.GetStringDelimitedData("/*+", numbers);
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expected));
		}

		[Test]
		[TestCase(new[] { -1 })]
		[TestCase(1, -2)]
		[TestCase(-111, 22, -3)]
		public void Custom_string_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
		{
			var data = TestDataBuilder.GetStringDelimitedData(":;", numbers);
			var negatives = numbers.Where(i => i < 0).ToArray();
			var expectedException = new UnparseableDataException(data).ContainsNegatives(negatives);

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}
	}
}