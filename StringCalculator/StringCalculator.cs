using System;
using System.Collections.Generic;
using System.Linq;

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

            IEnumerable<IDataParser> parsers = new List<IDataParser>
            {
                new CustomDelimiterDataParser(_data),
                new DefaultDataParser(_data)
            };

            foreach (var parser in parsers)
            {
                if (parser.CanParse())
                {
                    parser.Parse();
                    return parser.Numbers.Sum();
                }
            }

            throw new Exception("Data could not be parsed");
        }
    }
}