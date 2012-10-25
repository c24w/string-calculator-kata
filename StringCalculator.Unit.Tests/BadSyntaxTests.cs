using NUnit.Framework;

namespace StringCalculator.Unit.Tests
{
	[TestFixture]
	public class BadSyntaxTests
	{
		[Test]
		[TestCase("//#\n1~2", "~")]
		[TestCase("//[~~~]\n1~~2", "~~")]
		[TestCase("//[~~~][~]\n1~~2", "~~")]
		[TestCase("//^\n1^2%3", "%")]
		[TestCase("//$\n1\n2£3", "£")]
		public void Exception_is_thrown_when_an_undefined_custom_delimiter_is_used(string data, string undefinedDelimiter)
		{
			var expectedException = new UnparseableDataException(data).UndefinedDelimiters(undefinedDelimiter);
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
 
	}
}