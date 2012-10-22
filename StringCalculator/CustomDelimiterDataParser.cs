using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class CustomDelimiterDataParser : IDataParser
    {
        private readonly string _data;
        private readonly IDataValidator _dataValidator;
        private readonly INumberListParser _numListParser;
        public IEnumerable<int> Numbers { get; protected set; }

        public CustomDelimiterDataParser(string data, IDataValidator dataValidator, INumberListParser numListParser)
        {
            _data = data;
            _dataValidator = dataValidator;
            _numListParser = numListParser;
        }

        public CustomDelimiterDataParser(string data) : this(data, new DefaultDataValidator(), new DefaultNumberListParser()) { }

        public void Parse()
        {
            var numList = _data.Substring(_data.IndexOf('\n') + 1);

            Numbers = _numListParser.ParseNumberList(numList, ExtractDelimiter());

            _dataValidator.Validate(Numbers);
        }

        private string ExtractDelimiter()
        {
            if (IsStringDelimited())
            {
                var delimLength = _data.IndexOf("]\n", StringComparison.Ordinal) - 3;

                return _data.Substring(3, delimLength);
            }

            return _data[2].ToString();
        }

        private bool IsStringDelimited()
        {
            return _data.StartsWith("//[");
        }

        public bool CanParse()
        {
            return Regex.IsMatch(_data, @"^//(\[(?<delim>.+)\]|(?<delim>.+))\n-?\d+((\k<delim>|\n)-?\d+)*$");
        }
    }
};