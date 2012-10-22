using System;
using System.Collections.Generic;
using System.Linq;
using StringCalculator.Parsing;

namespace StringCalculator
{
    public class StringCalculator
    {
        private readonly string _data;

        public StringCalculator(string data)
        {
            _data = data;
        }

        public int Sum()
        {
            if (_data.Equals(string.Empty))
                return 0;

            var parser = SelectParser();

            parser.Parse();

            return parser.Numbers.Sum();
        }

        private IDataParser SelectParser()
        {
            IEnumerable<IDataParser> parsers = new List<IDataParser>
            {
                new CustomStringDelimiterDataParser(_data),
                new CustomCharDelimiterDataParser(_data),
                new DefaultDataParser(_data)
            };

            foreach (var parser in parsers)
            {
                if (parser.CanParse())
                    return parser;
            }

            throw new Exception("Data cannot be parsed: " + _data);
       }

    }
}