using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class CustomDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private readonly DefaultDataValidator _dataValidator;
        private const char CompulsoryDelimiter = '\n';
        public IEnumerable<int> Numbers { get; protected set; }

        public CustomDelimiterDataParser(string data, DefaultDataValidator dataValidator)
        {
            _data = data;
            _dataValidator = dataValidator;
        }

        public CustomDelimiterDataParser(string data) : this(data, new DefaultDataValidator()) { }

        public void Parse()
        {
            var delimiter = ExtractDelimiter();

            Numbers = ExtractNumberData(delimiter).Select(int.Parse).Where(i => i < 1000);

            _dataValidator.Validate(Numbers);
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
            return Regex.IsMatch(_data, @"^//(\[(?<delim>.+)\]|(?<delim>.+))\n-?\d+((\k<delim>|\n)-?\d+)*$");
        }
    }
}