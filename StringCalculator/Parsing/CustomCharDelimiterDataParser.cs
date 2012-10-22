using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.Parsing
{
    public class CustomCharDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private readonly IDataValidator _dataValidator;
        private readonly INumberListParser _numListParser;
        public IEnumerable<int> Numbers { get; protected set; }

        public CustomCharDelimiterDataParser(string data, IDataValidator dataValidator, INumberListParser numListParser)
        {
            _data = data;
            _dataValidator = dataValidator;
            _numListParser = numListParser;
        }

        public CustomCharDelimiterDataParser(string data) : this(data, new DefaultDataValidator(), new DefaultNumberListParser()) { }

        public void Parse()
        {
            var numList = _data.Substring(_data.IndexOf('\n') + 1);

            Numbers = _numListParser.ParseNumberList(numList, ExtractDelimiter());

            _dataValidator.Validate(Numbers);
        }

        private string ExtractDelimiter()
        {
            return _data[2].ToString();
        }
        public bool CanParse()
        {
            return Regex.IsMatch(_data, @"^//(?<delim>.+)\n-?\d+((\k<delim>|\n)-?\d+)*$", RegexOptions.Compiled);
        }
    }
}