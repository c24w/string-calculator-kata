using System;
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
        [TestCase("?", 1, 2)]
        [TestCase("!!", 12, 345, 6)]
        [TestCase("@#~", 12, 345, 6)]
        [TestCase("[]", 12, 345, 6)]
        public void Custom_string_delimited_numbers_returns_the_sum(string delimiter, params int[] numbers)
        {
            var data = GetStringDelimitedData(delimiter, numbers);

            var sum = new StringCalculator(data).Sum();

            Assert.That(sum, Is.EqualTo(numbers.Sum()));
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

    public class StringCalculator
    {
        private readonly string _data;
        private CustomDelimiterDataParser parser;
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
                return new CustomDelimiterDataParser(_data).Numbers.Sum();

            return new DefaultDataParser(_data).Numbers.Sum();
        }
    }

    public class DefaultDataParser
    {
        private readonly string _data;
        private const char DefaultDelimiter = ',';
        private const char ConstDelimiter = '\n';
        public IEnumerable<int> Numbers { get; private set; }

        public DefaultDataParser(string data)
        {
            _data = data;
            Parse();
        }

        private void Parse()
        {
            var delimiters = new[] {DefaultDelimiter, ConstDelimiter};

            Numbers = _data.Split(delimiters).Select(int.Parse);
        }
    }

    internal class CustomDelimiterDataParser
    {
        private readonly string _data;
        private const char ConstDelimiter = '\n';
        public IEnumerable<int> Numbers { get; private set; }

        public CustomDelimiterDataParser(string data)
        {
            _data = data;
            Parse();
        }

        private void Parse()
        {
            var delimiter = _data.StartsWith("//[") ? GetStringDelimiterFromData() : _data[2].ToString();

            Numbers = IsolateNumberData(delimiter).Select(int.Parse);
        }

        private IEnumerable<string> IsolateNumberData(string delimiter)
        {
            var dataIndex = _data.IndexOf('\n') + 1;

            var delimiters = new[] { delimiter, ConstDelimiter.ToString() };

            return _data.Substring(dataIndex).Split(delimiters, StringSplitOptions.None);
        }

        private string GetStringDelimiterFromData()
        {
            var length = _data.IndexOf("]\n", StringComparison.Ordinal) - 3;

            return _data.Substring(3, length);
        }

    }
}
