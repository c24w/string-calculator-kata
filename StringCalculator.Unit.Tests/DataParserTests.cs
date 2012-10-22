using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
    [TestFixture]
    public class DataParserTests
    {
        [Test]
        [TestCase("")]
        [TestCase("1")]
        [TestCase("1,2,3")]
        [TestCase("12,345,6")]
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
        [TestCase("//#\n1#2#3")]
        [TestCase("//[~~]\n1~~2~~3")]
        public void Default_data_parser_can_parse_method_returns_false_for_unaccepted_strings(string data)
        {
            Assert.That(new DefaultDataParser(data).CanParse(), Is.EqualTo(false));
        }

        [Test]
        [TestCase("//#\n1")]
        [TestCase("//*\n12*345*6")]
        [TestCase("//[%�]\n12%�3%�456")]
        [TestCase("//[~~~]\n1~~~2")]
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
        [TestCase("//#1#2#3")]
        [TestCase("//[~~]1~~2~~3")]
        public void Custom_delimiter_data_parser_can_parse_method_returns_false_for_unaccepted_strings(string data)
        {
            Assert.That(new DefaultDataParser(data).CanParse(), Is.EqualTo(false));
        }
    }
}