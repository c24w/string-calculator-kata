using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            if (Regex.IsMatch(_data, @"^-?\d+(,-?\d+)*$"))
            {
                values = _data.Split(new[] { DefaultDelimiter, ConstDelimiter });

                Numbers = values.Select(int.Parse).Where(i => i < 1000);
            }
            else
            {
                var matchBasicParts = Regex.Match(_data, @"^//((?<delimDef>.)|\[(?<delimDef>.+?)\])\n(?<delimNums>-?\d+((.+|\n)-?\d+)*)$", RegexOptions.Compiled);

                if (!matchBasicParts.Success)
                    throw new FormatException("Data cannot be parsed: " + _data);

                var delimCapture = matchBasicParts.Groups["delimDef"].Captures[0].Value;
                var delims = delimCapture.Split(new[] { "][" }, StringSplitOptions.None);

                var valuesCapture = matchBasicParts.Groups["delimNums"].Captures[0].Value;

                EnsureOnlyDefinedDelimitersAreUsed(delims, valuesCapture);

                values = valuesCapture.Split(delims, StringSplitOptions.None);
            }

            ParseValues(values);

            _dataValidator.Validate(Numbers);
        }

        private void EnsureOnlyDefinedDelimitersAreUsed(string[] delimiters, string delimitedValues)
        {
            var delimiterPattern = Regex.Escape(string.Join("|", delimiters));

            var matchDefinedDelimsPattern = string.Format(@"^-?\d+(({0})-?\d+)*$", delimiterPattern);

            var matchDefinedDelims = Regex.Match(delimitedValues, matchDefinedDelimsPattern, RegexOptions.Compiled);

            if (!matchDefinedDelims.Success)
                UnparseableException("number values contain an undefined delimiter");
        }

        private void UnparseableException(string extraInfo)
        {
            throw new FormatException(string.Format("Data cannot be parsed ({0}): {1}", extraInfo, _data));
        }

        private void ParseValues(IEnumerable<string> values)
        {
            Numbers = values.Select(int.Parse).Where(i => i < 1000);
        }
    }
}