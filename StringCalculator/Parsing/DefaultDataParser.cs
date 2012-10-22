using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringCalculator.Parsing
{
    public class DefaultDataParser : IDataParser
    {
        private readonly string _data;
        private readonly IDataValidator _dataValidator;
        private readonly INumberListParser _defaultNumberListParser;
        private const char DefaultDelimiter = ',';
        public IEnumerable<int> Numbers { get; protected set; }

        public DefaultDataParser(string data, IDataValidator dataValidator, INumberListParser numListParser)
        {
            _defaultNumberListParser = numListParser;
            _data = data;
            _dataValidator = dataValidator;
        }

        public DefaultDataParser(string data) : this(data, new DefaultDataValidator(), new DefaultNumberListParser()) { }

        public virtual void Parse()
        {
            Numbers = _defaultNumberListParser.ParseNumberList(_data, DefaultDelimiter);

            _dataValidator.Validate(Numbers);
        }

        public virtual bool CanParse()
        {
            return Regex.IsMatch(_data, "^(-?\\d+([,\n]-?\\d+)*)?$", RegexOptions.Compiled);
        }
    }
}