using System.Collections.Generic;

namespace StringCalculator.Parsing
{
    public interface IDataParser
    {
        IEnumerable<int> Numbers { get; }
        void Parse();
        bool CanParse();
    }
}