using System;
using System.Linq;
using NUnit.Framework;
using StringCalculator.Parsing;

namespace StringCalculator.Unit.Tests
{
    [TestFixture]
    public class DataParserTests
    {
        [Test]
        [TestCase("")]
        [TestCase("1")]
        [TestCase("1,2,3")]
        [TestCase("1,2\n3")]
        [TestCase("12,345,6")]
        [TestCase("12\n345,6")]
        public void Default_data_parser_can_parse_method_returns_true_for_accepted_strings(string data)
        {
            Assert.That(new DefaultDataParser(data).CanParse(), Is.EqualTo(true));
        }

        [Test]
        [TestCase("a")]
        [TestCase("a,b,c")]
        [TestCase(",")]
        [TestCase(",1,")]
        [TestCase(",1,23")]
        [TestCase("1,")]
        [TestCase("1,23,")]
        [TestCase("1\n23,")]
        [TestCase("//#\n1#2#3")]
        [TestCase("//[~~]\n1~~2~~3")]
        public void Default_data_parser_can_parse_method_returns_false_for_unaccepted_strings(string data)
        {
            Assert.That(new DefaultDataParser(data).CanParse(), Is.EqualTo(false));
        }

        [Test]
        [TestCase("//#\n1")]
        [TestCase("//*\n12*345*6")]
        [TestCase("//*\n12*345\n6")]
        [TestCase("//[%£]\n12%£3%£456")]
        [TestCase("//[~~~]\n1~~~2")]
        [TestCase("//[~~~]\n1~~~2\n3")]
        public void Custom_delimiter_data_parser_can_parse_method_returns_true_for_accepted_strings(string data)
        {
            Assert.That(new CustomDelimiterDataParser(data).CanParse(), Is.EqualTo(true));
        }

        [Test]
        [TestCase("a")]
        [TestCase("a,b,c")]
        [TestCase(",")]
        [TestCase(",1,")]
        [TestCase(",1,23")]
        [TestCase("1,")]
        [TestCase("1,23,")]
        [TestCase("//^\n1^2~3")]
        [TestCase("//#1#2#3")]
        [TestCase("//[~~]1~~2\n3")]
        public void Custom_delimiter_data_parser_can_parse_method_returns_false_for_unaccepted_strings(string data)
        {
            Assert.That(new DefaultDataParser(data).CanParse(), Is.EqualTo(false));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1, -2)]
        [TestCase(-111, 22, -3)]
        public void Comma_delimited_data_containing_negative_numbers_throws_an_exception(params int[] numbers)
        {
            var data = string.Join(",", numbers);

            var negatives = string.Join(",", numbers.Where(i => i < 0));

            var exception = Assert.Throws(typeof(Exception), () => new StringCalculator(data).Sum());

            var expectedMessage = "Data cannot contain negative numbers: " + negatives;

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
            
            var expectedMessage = "Data cannot contain negative numbers: " + negatives;

            var exception = Assert.Throws(typeof(Exception), () => new StringCalculator(data).Sum());

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
            
            var expectedMessage = "Data cannot contain negative numbers: " + negatives;

            var exception = Assert.Throws(typeof(Exception), () => new StringCalculator(data).Sum());

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }
    }
}