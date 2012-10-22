using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class DefaultDataParser : IDataParser
    {
        protected readonly string Data;
        private const char DefaultDelimiter = ',';
        protected const char CompulsoryDelimiter = '\n';
        public IEnumerable<int> Numbers { get; protected set; }

        public DefaultDataParser(string data)
        {
            Data = data;
        }

        public virtual void Parse()
        {
            var delimiters = new[] { DefaultDelimiter, CompulsoryDelimiter };

            Numbers = Data.Split(delimiters).Select(int.Parse);
        }

        public virtual bool CanParse()
        {
            return Regex.IsMatch(Data, "^(\\d+(,\\d+)*)?$");
        }
    }
}