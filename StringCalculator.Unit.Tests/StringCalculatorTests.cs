using System.Collections.Generic;
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
        [TestCase('"', 345)]
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
        public void Test()
        {
            
        }

        private static string GetCharDelimitedData(char delimiter, params int[] numbers)
        {
            return string.Format("//{0}\n{1}", delimiter, string.Join(delimiter.ToString(), numbers));
        }
    }

    public class StringCalculator
    {
        private readonly string _data;
        private const char DefaultDelimiter = ',';
        private const char ConstDelimiter = '\n';

        public StringCalculator(string data)
        {
            _data = data;
        }

        public int Sum()
        {
            if (_data.Equals(string.Empty))
                return 0;

            if (_data.StartsWith("//"))
                return SumCustomCharDelimited();

            string[] splitNums = _data.Split(new[] { DefaultDelimiter, ConstDelimiter });

            return SumNums(splitNums);
        }

        private int SumCustomCharDelimited()
        {
            var delimiters = new[] { _data[2], ConstDelimiter };

            var splitNums = _data.Substring(_data.IndexOf(ConstDelimiter) + 1).Split(delimiters);

            return SumNums(splitNums);
        }

        private static int SumNums(IEnumerable<string> splitNums)
        {
            return splitNums.Sum(n => int.Parse(n));
        }
    }
}
