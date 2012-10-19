using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Unit.Tests
{
    internal class CustomDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private const char ConstDelimiter = '\n';
        public IEnumerable<int> Numbers { get; private set; }

        public CustomDelimiterDataParser(string data)
        {
            _data = data;
            Parse();
        }

        private void Parse()
        {
            var delimiter = _data.StartsWith("//[") ? GetStringDelimiterFromData() : _data[2].ToString();

            Numbers = ExtractNumberData(delimiter).Select(int.Parse);
        }

        private IEnumerable<string> ExtractNumberData(string delimiter)
        {
            var dataIndex = _data.IndexOf('\n') + 1;

            var delimiters = new[] { delimiter, ConstDelimiter.ToString() };

            return _data.Substring(dataIndex).Split(delimiters, StringSplitOptions.None);
        }

        private string GetStringDelimiterFromData()
        {
            var length = _data.IndexOf("]\n", StringComparison.Ordinal) - 3;

            return _data.Substring(3, length);
        }
    }
}