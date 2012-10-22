using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class CustomDelimiterDataParser : DefaultDataParser
    {
        public CustomDelimiterDataParser(string data) : base(data) { }

        public override void Parse()
        {
            var delimiter = ExtractDelimiter();

            Numbers = ExtractNumberData(delimiter).Select(int.Parse);
        }

        private string ExtractDelimiter()
        {
            if (IsStringDelimited())
            {
                var delimLength = Data.IndexOf("]\n") - 3;

                return Data.Substring(3, delimLength);
            }

            return Data[2].ToString();
        }

        private bool IsStringDelimited()
        {
            return Data.StartsWith("//[");
        }

        private IEnumerable<string> ExtractNumberData(string delimiter)
        {
            var numberDataIndex = Data.IndexOf('\n') + 1;

            var delimiters = new[] { delimiter, CompulsoryDelimiter.ToString() };

            return Data.Substring(numberDataIndex).Split(delimiters, StringSplitOptions.None);
        }

        public override bool CanParse()
        {
            return Regex.IsMatch(Data, @"^//\[(?<delim>.+)\]|(?<delim>.+)\n\d+(\k<delim>\d+)*$");
        }
    }
}