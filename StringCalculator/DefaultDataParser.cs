using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class DefaultDataParser : IDataParser
    {
        private readonly string _data;
        private const char DefaultDelimiter = ',';
        private const char CompulsoryDelimiter = '\n';
        public IEnumerable<int> Numbers { get; protected set; }

        public DefaultDataParser(string data)
        {
            _data = data;
        }

        public virtual void Parse()
        {
            var delimiters = new[] { DefaultDelimiter, CompulsoryDelimiter };

            Numbers = _data.Split(delimiters).Select(int.Parse);
        }

        public virtual bool CanParse()
        {
            return Regex.IsMatch(_data, "^(\\d+(,\\d+)*)?$");
        }
    }
}