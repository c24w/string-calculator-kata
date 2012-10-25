using System.Linq;
using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class CommaDelimitedTests
	{
		[Test]
		[TestCase("0,1", 1)]
		[TestCase("1,2,3", 6)]
		[TestCase("1,22,333", 356)]
		public void Comma_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expectedSum));
		}

		[Test]
		[TestCase("0\n1", 1)]
		[TestCase("1,2\n3", 6)]
		[TestCase("1\n22,333", 356)]
		[TestCase("1\n22\n333", 356)]
		public void Comma_and_line_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expectedSum));
		}

		[Test]
		[TestCase(1, 1, 1000)]
		[TestCase(24, 1, 23, 4567)]
		[TestCase(1002, 1, 2, 999)]
		public void Comma_delimited_numbers_greater_than_999_are_ignored(int expected, params int[] numbers)
		{
			var data = string.Join(",", numbers);
			var sum = new StringCalculator(data).Sum();
			Assert.That(sum, Is.EqualTo(expected));
		}

		[Test]
		[TestCase(new[] { -1 })]
		[TestCase(1, -2)]
		[TestCase(-111, 22, -3)]
		public void Comma_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
		{
			var data = string.Join(",", numbers);
			var negatives = numbers.Where(i => i < 0);
			var expectedException = new UnparseableDataException(data).ContainsNegatives(negatives);

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}
	}
}