using System.Collections.Generic;

namespace StringCalculator
{
    class CommaDelimitedParser : Parser
    {
        public CommaDelimitedParser(string data) : base(data) { }

        public override void Parse()
        {
            var values = SplitDataOnCommas();
            Numbers = ParserTools.ParseToIntegers(values);
        }

        private IEnumerable<string> SplitDataOnCommas()
        {
            return Data.Split(new[] { ',', ConstDelimiter });
        }
    }
}