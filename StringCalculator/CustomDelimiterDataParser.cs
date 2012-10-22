using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class CustomDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private const char CompulsoryDelimiter = '\n';
        public IEnumerable<int> Numbers { get; protected set; }

        public CustomDelimiterDataParser(string data)
        {
            _data = data;
        }

        public void Parse()
        {
            var delimiter = ExtractDelimiter();

            Numbers = ExtractNumberData(delimiter).Select(int.Parse);

            var negatives = Numbers.Where(i => i < 0).ToArray();

            if (negatives.Any())
            {
                throw new Exception("Data cannot contain negative numbers: " + string.Join(",", negatives));
            }
        }

        private string ExtractDelimiter()
        {
            if (IsStringDelimited())
            {
                var delimLength = _data.IndexOf("]\n") - 3;

                return _data.Substring(3, delimLength);
            }

            return _data[2].ToString();
        }

        private bool IsStringDelimited()
        {
            return _data.StartsWith("//[");
        }

        private IEnumerable<string> ExtractNumberData(string delimiter)
        {
            var numberDataIndex = _data.IndexOf('\n') + 1;

            var delimiters = new[] { delimiter, CompulsoryDelimiter.ToString() };

            return _data.Substring(numberDataIndex).Split(delimiters, StringSplitOptions.None);
        }

        public bool CanParse()
        {
            return Regex.IsMatch(_data, @"^//(\[(?<delim>.+)\]|(?<delim>.+))\n-?\d+(\k<delim>-?\d+)*$");
        }
    }
}