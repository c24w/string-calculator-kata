using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class DefaultDataValidator : IDataValidator
    {
        public void Validate(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
                throw new FormatException("Data cannot contain negative numbers: " + string.Join(",", negatives));
        }
    }
}