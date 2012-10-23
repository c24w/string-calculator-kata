using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class DefaultDataValidator : IDataValidator
    {
        public void ValidateValues(IEnumerable<int> numbers)
        {
            CheckForNegatives(numbers);
        }

        public void ValidateSyntax(string[] delimiters, string delimitedValues)
        {
            EnsureOnlyDefinedDelimitersAreUsed(delimiters, delimitedValues);
        }


        private void CheckForNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
                ThrowUnparseable("cannot contain negative numbers: " + string.Join(",", negatives));
        }

        private void EnsureOnlyDefinedDelimitersAreUsed(string[] definedDelimiters, string delimitedValues)
        {
            var matchDefinedDelims = RegexPatterns.OnlyAllowDefinedDelimitersPattern(definedDelimiters).Match(delimitedValues);

            if (!matchDefinedDelims.Success)
                ThrowUnparseable("number values contain an undefined delimiter");
        }

        private void ThrowUnparseable(string reason)
        {
            throw new FormatException(string.Format("Data cannot be parsed ({0})", reason));
        }
    }
}