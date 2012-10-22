using System;
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

            var negatives = Numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
            {
                throw new Exception("Data cannot contain negative numbers: " + string.Join(",", negatives));
            }
        }

        public virtual bool CanParse()
        {
            return Regex.IsMatch(_data, "^(-?\\d+([,\n]-?\\d+)*)?$");
        }
    }
}