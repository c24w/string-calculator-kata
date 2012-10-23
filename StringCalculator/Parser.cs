using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Parser
    {
        private readonly string _data;
        private readonly IDataValidator _dataValidator;
        public IEnumerable<int> Numbers { get; protected set; }
        private const char DefaultDelimiter = ',';
        private const char ConstDelimiter = '\n';

        public Parser(string data, IDataValidator dataValidator)
        {
            _data = data;
            _dataValidator = dataValidator;
        }

        public Parser(string data) : this(data, new DefaultDataValidator()) { }

        public void Parse()
        {
            string[] values;

            if (RegexPatterns.CommaDelimitedPattern.Match(_data).Success)
            {
                values = _data.Split(new[] { DefaultDelimiter, ConstDelimiter });

                Numbers = values.Select(int.Parse).Where(i => i < 1000);
            }
            else
            {
                var matchBasicPattern = RegexPatterns.BasicSyntaxPattern.Match(_data);
                
                if (!matchBasicPattern.Success)
                    throw new FormatException("Data cannot be parsed: " + _data);

                var delimCapture = matchBasicPattern.Groups["delimDef"].Captures[0].Value;
                var delims = delimCapture.Split(new[] { "][" }, StringSplitOptions.None);

                var valuesCapture = matchBasicPattern.Groups["delimNums"].Captures[0].Value;

                _dataValidator.ValidateSyntax(delims, valuesCapture);

                values = valuesCapture.Split(delims, StringSplitOptions.None);
            }

            ParseToIntegers(values);

            _dataValidator.ValidateValues(Numbers);
        }

        private void ParseToIntegers(IEnumerable<string> values)
        {
            Numbers = values.Select(int.Parse).Where(i => i < 1000);
        }
    }
}