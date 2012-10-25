﻿using System.Linq;
using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class StringCalculatorTests
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
		[TestCase('|', 1)]
		[TestCase('"', 234)]
		public void Custom_char_delimited_single_number_less_than_1000_returns_that_number(char delimiter, int number)
		{
			var data = TestDataBuilder.GetCharDelimitedData(delimiter, number);

			var sum = new StringCalculator(data).Sum();

			Assert.That(sum, Is.EqualTo(number));
		}

		[Test]
		[TestCase('~', 1, 2)]
		[TestCase('*', 12, 345, 6)]
		public void Custom_char_delimited_numbers_returns_the_sum_of_values_less_than_1000(char delimiter, params int[] numbers)
		{
			var data = TestDataBuilder.GetCharDelimitedData(delimiter, numbers);

			var sum = new StringCalculator(data).Sum();

			Assert.That(sum, Is.EqualTo(numbers.Sum()));
		}

		[Test]
		[TestCase("//~\n1~2\n3", 6)]
		[TestCase("//*\n12\n345*6", 363)]
		public void Custom_char_and_new_line_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();

			Assert.That(sum, Is.EqualTo(expectedSum));
		}

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
		[TestCase("//[)(][***]\n12***345)(6", 363)]
		public void Multiple_custom_delimiter_and_new_line_delimited_numbers_returns_the_sum_of_values_less_than_1000(string data, int expectedSum)
		{
			var sum = new StringCalculator(data).Sum();

			Assert.That(sum, Is.EqualTo(expectedSum));
		}

		[Test]
		[TestCase("//#\n1~2", "~")]
		[TestCase("//[~~~]\n1~~2", "~~")]
		[TestCase("//[~~~][~]\n1~~2", "~~")]
		[TestCase("//^\n1^2%3", "%")]
		[TestCase("//$\n1\n2£3", "£")]
		public void Exception_is_thrown_when_an_undefined_custom_delimiter_is_used(string data, string undefinedDelimiter)
		{
			var expectedException = new UnparseableDataException(data).UndefinedDelimiter();

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}

		[Test]
		[TestCase("a")]
		[TestCase("a,b,c")]
		[TestCase(",")]
		[TestCase(",1,")]
		[TestCase(",1,23")]
		[TestCase("1,")]
		[TestCase("1,-23,")]
		[TestCase("//#1\n2#3")]
		[TestCase("//[~~]1~~2~~3")]
		public void Exception_is_thrown_when_data_is_syntactically_incorrect(string data)
		{
			var expectedException = new UnparseableDataException(data).InvalidSyntax();

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
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
		[TestCase(1, 1, 1000)]
		[TestCase(24, 1, 23, 4567)]
		[TestCase(1002, 1, 2, 999)]
		public void Custom_char_delimited_numbers_greater_than_999_are_ignored(int expected, params int[] numbers)
		{
			var data = TestDataBuilder.GetCharDelimitedData('d', numbers);

			var sum = new StringCalculator(data).Sum();

			Assert.That(sum, Is.EqualTo(expected));
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
		public void Comma_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
		{
			var data = string.Join(",", numbers);
			var negatives = numbers.Where(i => i < 0);
			var expectedException = new UnparseableDataException(data).ContainsNegatives(negatives);

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}

		[Test]
		[TestCase(new[] { -1 })]
		[TestCase(1, -2)]
		[TestCase(-111, 22, -3)]
		public void Custom_char_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
		{
			var data = TestDataBuilder.GetCharDelimitedData('^', numbers);
			var negatives = numbers.Where(i => i < 0);
			var expectedException = new UnparseableDataException(data).ContainsNegatives(negatives);

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}

		[Test]
		[TestCase(new[] { -1 })]
		[TestCase(1, -2)]
		[TestCase(-111, 22, -3)]
		public void Custom_string_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
		{
			var data = TestDataBuilder.GetStringDelimitedData(":;", numbers);
			var negatives = numbers.Where(i => i < 0);
			var expectedException = new UnparseableDataException(data).ContainsNegatives(negatives);

			var exception = Assert.Throws<UnparseableDataException>(() => new StringCalculator(data).Sum());

			Assert.That(exception, Is.EqualTo(expectedException));
		}
	}
}
