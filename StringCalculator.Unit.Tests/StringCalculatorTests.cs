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
            var data = GetCharDelimitedData(delimiter, number);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(number));
        }

        [Test]
        [TestCase('~', 1, 2)]
        [TestCase('*', 12, 345, 6)]
        public void Custom_char_delimited_numbers_returns_the_sum(char delimiter, params int[] numbers)
        {
            var data = GetCharDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase("&", 1)]
        [TestCase("##", 234)]
        public void Custom_string_delimited_number_returns_that_number(string delimiter, int number)
        {
            var data = GetStringDelimitedData(delimiter, number);

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
            var data = GetStringDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(numbers.Sum()));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1,-2)]
        [TestCase(-111,22,-3)]
        public void Comma_delimited_containing_negative_numbers_throws_an_exception(params int[] numbers)
        {
            var data = string.Join(",", numbers);

            var negatives = string.Join(",", numbers.Where(i => i < 0));

            var exception = Assert.Throws(typeof(Exception), () => new StringCalculator(data).Sum());
            
            var expectedMessage = "Data cannot contain negative numbers: " + negatives;

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        private static string GetCharDelimitedData(char delimiter, params int[] numbers)
        {
            return string.Format("//{0}\n{1}", delimiter, string.Join(delimiter.ToString(), numbers));
        }

        private static string GetStringDelimitedData(string delimiter, params int[] numbers)
        {
            return string.Format("//[{0}]\n{1}", delimiter, string.Join(delimiter, numbers));
        }
    }
}
