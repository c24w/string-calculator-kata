using System.Collections.Generic;

namespace StringCalculator
{
    public interface IDataValidator
    {
        void ValidateValues(IEnumerable<int> numbers);
        void ValidateSyntax(string[] delims, string valuesCapture);
    }
}