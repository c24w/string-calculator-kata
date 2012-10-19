using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Unit.Tests
{
    public class DefaultDataParser : IDataParser
    {
        private readonly string _data;
        private const char DefaultDelimiter = ',';
        private const char ConstDelimiter = '\n';
        public IEnumerable<int> Numbers { get; private set; }

        public DefaultDataParser(string data)
        {
            _data = data;
            Parse();
        }

        private void Parse()
        {
            var delimiters = new[] { DefaultDelimiter, ConstDelimiter };

            Numbers = _data.Split(delimiters).Select(int.Parse);
        }
    }
}