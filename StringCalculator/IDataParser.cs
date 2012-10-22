using System.Collections.Generic;

namespace StringCalculator
{
    public interface IDataParser
    {
        IEnumerable<int> Numbers { get; }
        void Parse();
        bool CanParse();
    }
}