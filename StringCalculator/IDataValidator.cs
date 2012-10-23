using System.Collections.Generic;

namespace StringCalculator
{
    public interface IDataValidator
    {
        void Validate(IEnumerable<int> numbers);
        void EnsureOnlyDefinedDelimitersAreUsed(string[] delims, string valuesCapture);
    }
}