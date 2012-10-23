using System;
using System.Linq;
using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
    [TestFixture]
    public class DataParserTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(1, -2)]
        [TestCase(-111, 22, -3)]
        public void Comma_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
        {
            var data = string.Join(",", numbers);

            var negatives = string.Join(",", numbers.Where(i => i < 0));

            var exception = Assert.Throws(typeof(FormatException), () => new StringCalculator(data).Sum());

            var expectedMessage = string.Format("Data cannot be parsed (cannot contain negative numbers: {0})", negatives);

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1, -2)]
        [TestCase(-111, 22, -3)]
        public void Custom_char_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
        {
            var data = DataBuilder.GetCharDelimitedData('^', numbers);

            var negatives = string.Join(",", numbers.Where(i => i < 0));

            var expectedMessage = string.Format("Data cannot be parsed (cannot contain negative numbers: {0})", negatives);

            var exception = Assert.Throws(typeof(FormatException), () => new StringCalculator(data).Sum());

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1, -2)]
        [TestCase(-111, 22, -3)]
        public void Custom_string_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
        {
            var data = DataBuilder.GetStringDelimitedData(":;", numbers);

            var negatives = string.Join(",", numbers.Where(i => i < 0));

            var expectedMessage = string.Format("Data cannot be parsed (cannot contain negative numbers: {0})", negatives);

            var exception = Assert.Throws(typeof(FormatException), () => new StringCalculator(data).Sum());

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
    }
}