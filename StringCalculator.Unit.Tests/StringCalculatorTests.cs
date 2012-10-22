using System;
using System.Linq;
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
        [TestCase("1", 1)]
        [TestCase("123", 123)]
        public void Single_number_returns_that_number(string data, int expected)
        {
            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("1,2,3", 6)]
        [TestCase("1,22,333", 356)]
        public void Comma_delimited_numbers_returns_the_sum(string data, int expectedSum)
        {
            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        [TestCase('|', 1)]
        [TestCase('"', 234)]
        public void Custom_char_delimited_single_number_returns_that_number(char delimiter, int number)
        {
            var data = DataBuilder.GetCharDelimitedData(delimiter, number);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(number));
        }

        [Test]
        [TestCase('~', 1, 2)]
        [TestCase('*', 12, 345, 6)]
        public void Custom_char_delimited_numbers_returns_the_sum(char delimiter, params int[] numbers)
        {
            var data = DataBuilder.GetCharDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase("&", 1)]
        [TestCase("##", 234)]
        public void Custom_string_delimited_number_returns_that_number(string delimiter, int number)
        {
            var data = DataBuilder.GetStringDelimitedData(delimiter, number);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(number));
        }

        [Test]
        [TestCase("?", 1)]
        [TestCase("!!", 1, 234, 5)]
        [TestCase("@#~", 12, 345, 6)]
        [TestCase("[]", 12, 345, 6)]
        public void Custom_string_delimited_numbers_returns_the_sum(string delimiter, params int[] numbers)
        {
            var data = DataBuilder.GetStringDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase("a")]
        [TestCase("a,b,c")]
        [TestCase(",")]
        [TestCase(",1,")]
        [TestCase(",1,23")]
        [TestCase("1,")]
        [TestCase("1,-23,")]
        [TestCase("//^\n1^2~3")]
        [TestCase("//#1\n2#3")]
        [TestCase("//[~~]1~~2~~3")]
        public void Exception_is_thrown_when_no_suitable_parser_is_found(string data)
        {
            var exception = Assert.Throws(typeof (Exception), () => new StringCalculator(data).Sum());

            var expectedMessage = "Data could not be parsed: " + data;

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
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
            var data = DataBuilder.GetCharDelimitedData('d', numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1, 1, 1000)]
        [TestCase(24, 1, 23, 4567)]
        [TestCase(1002, 1, 2, 999)]
        public void Custom_string_delimited_numbers_greater_than_999_are_ignored(int expected, params int[] numbers)
        {
            var data = DataBuilder.GetStringDelimitedData("/*-", numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expected));
        }
    }
}
