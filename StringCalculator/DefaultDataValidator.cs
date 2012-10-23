using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class DefaultDataValidator : IDataValidator
    {
        public void ValidateParsedNumbers(IEnumerable<int> numbers)
        {
            CheckForNegatives(numbers);
        }

        private void CheckForNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
                ThrowUnparseable("cannot contain negative numbers: " + string.Join(",", negatives));
        }

        public void ValidateDelimitedData(string[] delimiters, string delimitedValues)
        {
            EnsureOnlyDefinedDelimitersAreUsed(delimiters, delimitedValues);
        }

        private void EnsureOnlyDefinedDelimitersAreUsed(string[] definedDelimiters, string delimitedValues)
        {
            var matchDefinedDelims = RegexPatterns.OnlyAllowDefinedDelimitersPattern(definedDelimiters).Match(delimitedValues);

            if (!matchDefinedDelims.Success)
                ThrowUnparseable("number values contain an undefined delimiter");
        }

        private static void ThrowUnparseable(string reason)
        {
            throw new FormatException(string.Format("Data cannot be parsed ({0})", reason));
        }
    }
}