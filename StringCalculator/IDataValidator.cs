using System.Collections.Generic;

namespace StringCalculator
{
    public interface IDataValidator
    {
        void Validate(IEnumerable<int> numbers);
    }
}