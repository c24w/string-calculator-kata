using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Parsing
{
    public class DefaultNumberListParser : INumberListParser
    {
        private const char CompulsoryDelimiter = '\n';

        public IEnumerable<int> ParseNumberList(string data, string delimiter)
        {
            var delimiters = new[] { delimiter, CompulsoryDelimiter.ToString() };

            return data.Split(delimiters, StringSplitOptions.None).Select(int.Parse).Where(i => i < 1000);
        }

        public IEnumerable<int> ParseNumberList(string data, char delimiter)
        {
            return ParseNumberList(data, delimiter.ToString());
        }
    }
}