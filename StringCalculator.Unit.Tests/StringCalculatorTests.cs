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
        [TestCase('|', 1, 1)]
        [TestCase('\\', 345, 345)]
        public void Custom_char_delimited_single_number_returns_that_number(char delimiter, int expectedSum, int number)
        {
            var data = GetCharDelimitedData(delimiter, number);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        [Test]
        [TestCase('~', 3, 1, 2)]
        [TestCase('*', 357, 12, 345)]
        public void Custom_char_delimited_numbers_returns_the_sum(char delimiter, int expectedSum, params int[] numbers)
        {
            var data = GetCharDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(expectedSum));
        }

        private static string GetCharDelimitedData(char delimiter, params int[] numbers)
        {
            return string.Format("//{0}\n{1}", delimiter, string.Join(delimiter.ToString(), numbers));
        }
    }

    public class StringCalculator
    {
        private string _data;

        public StringCalculator(string data)
        {
            _data = data;
        }

        public int Sum()
        {
            if (_data.Equals(string.Empty))
                return 0;

            char delimiter = ',';

            if (_data.StartsWith("//"))
            {
                delimiter = _data[2];
                _data = _data.Substring(4);
            }

            string[] splitNums = _data.Split(delimiter);

            return splitNums.Sum(n => int.Parse(n));
        }
    }
}
