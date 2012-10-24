using System.Collections.Generic;

namespace StringCalculator
{
    class CommaDelimitedParser : Parser
    {
        public CommaDelimitedParser(string data) : base(data) { }

        public override void Parse()
        {
            Numbers = ParseToIntegers(SplitValues());
        }

        private IEnumerable<string> SplitValues()
        {
            return Data.Split(new[] { ',', ConstDelimiter });
        }
    }
}