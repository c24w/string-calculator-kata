using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.Parsing
{
    public class CustomStringDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private readonly IDataValidator _dataValidator;
        private readonly INumberListParser _numListParser;
        public IEnumerable<int> Numbers { get; protected set; }

        public CustomStringDelimiterDataParser(string data, IDataValidator dataValidator, INumberListParser numListParser)
        {
            _data = data;
            _dataValidator = dataValidator;
            _numListParser = numListParser;
        }

        public CustomStringDelimiterDataParser(string data) : this(data, new DefaultDataValidator(), new DefaultNumberListParser()) { }

        public void Parse()
        {
            var numList = _data.Substring(_data.IndexOf('\n') + 1);

            Numbers = _numListParser.ParseNumberList(numList, ExtractDelimiter());

            _dataValidator.Validate(Numbers);
        }

        private string ExtractDelimiter()
        {
            var delimLength = _data.IndexOf("]\n", StringComparison.Ordinal) - 3;
            return _data.Substring(3, delimLength);
        }

        public bool CanParse()
        {
            return Regex.IsMatch(_data, @"^//\[(?<delim>.+)\]\n-?\d+((\k<delim>|\n)-?\d+)*$", RegexOptions.Compiled);
        }
    }
}