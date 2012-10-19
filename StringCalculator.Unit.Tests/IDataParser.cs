using System.Collections.Generic;

namespace StringCalculator.Unit.Tests
{
    public interface IDataParser
    {
        IEnumerable<int> Numbers { get; }
    }
}