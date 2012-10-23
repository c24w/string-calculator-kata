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
            {
                var message = "cannot contain negative numbers: " + string.Join(",", negatives);
                UnparseableException(message);
            }
        }

        private void EnsureOnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
        {
            var delimiterPattern = Regex.Escape(string.Join("|", delimiters));

            var matchDefinedDelimsPattern = string.Format(@"^-?\d+(({0})-?\d+)*$", delimiterPattern);

            var matchDefinedDelims = Regex.Match(delimitedValues, matchDefinedDelimsPattern, RegexOptions.Compiled);

            if (!matchDefinedDelims.Success)
                UnparseableException("number values contain an undefined delimiter");
        }

        private void UnparseableException(string extraInfo)
        {
            throw new FormatException(string.Format("Data cannot be parsed ({0})", extraInfo));
        }
    }
}