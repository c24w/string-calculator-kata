using System.Collections.Generic;

namespace StringCalculator
{
    public interface IDataValidator
    {
        void ValidateParsedNumbers(IEnumerable<int> numbers);
        void ValidateDelimitedData(string[] delims, string valuesCapture);
    }
}