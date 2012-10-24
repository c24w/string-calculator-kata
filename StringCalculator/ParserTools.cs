using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class ParserTools
    {
        public static IEnumerable<int> ParseToIntegers(IEnumerable<string> values)
        {
            return values.Select(int.Parse).Where(i => i < 1000);
        }
    }
}