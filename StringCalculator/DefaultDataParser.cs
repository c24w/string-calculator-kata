using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class DefaultDataParser : IDataParser
    {
        private readonly string _data;
        private readonly DefaultDataValidator _dataValidator;
        private const char DefaultDelimiter = ',';
        private const char CompulsoryDelimiter = '\n';
        public IEnumerable<int> Numbers { get; protected set; }

        public DefaultDataParser(string data, DefaultDataValidator dataValidator)
        {
            _data = data;
            _dataValidator = dataValidator;
        }

        public DefaultDataParser(string data) : this(data, new DefaultDataValidator()) { }

        public virtual void Parse()
        {
            var delimiters = new[] { DefaultDelimiter, CompulsoryDelimiter };

            Numbers = _data.Split(delimiters).Select(int.Parse).Where(i => i < 1000);

            _dataValidator.Validate(Numbers);
        }

        public virtual bool CanParse()
        {
            return Regex.IsMatch(_data, "^(-?\\d+([,\n]-?\\d+)*)?$");
        }
    }
}