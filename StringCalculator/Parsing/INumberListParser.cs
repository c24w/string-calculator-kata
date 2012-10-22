using System.Collections.Generic;

namespace StringCalculator.Parsing
{
    public interface INumberListParser
    {
        IEnumerable<int> ParseNumberList(string data, string delimiter);
        IEnumerable<int> ParseNumberList(string data, char delimiter);
    }
}