using System;
using System.Collections.Generic;

namespace StringCalculator
{
	public class UnparseableDataException : FormatException
	{
		private readonly List<string> _reasons = new List<string>();

		public override string Message
		{
			get
			{
				return string.Format("{0}  ({1})", base.Message, string.Join(", ", _reasons));
			}
		}

		public UnparseableDataException(string badData) : base("Cannot parse: " + badData) { }

		public UnparseableDataException ContainsNegatives(params int[] negativeNumbers)
		{
			_reasons.Add(string.Format("cannot contain negative numbers: " + string.Join(",", negativeNumbers)));
			return this;
		}

		public UnparseableDataException InvalidSyntax()
		{
			_reasons.Add("invalid syntax");
			return this;
		}

		public UnparseableDataException UndefinedDelimiters(params string[] undefinedDelims)
		{
			_reasons.Add("undefined delimiters were used: " + string.Join(", ", undefinedDelims));
			return this;
		}

		public override string ToString()
		{
			return Message;
		}

		protected bool Equals(UnparseableDataException other)
		{
			return Equals(Message, other.Message);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((UnparseableDataException)obj);
		}

		public override int GetHashCode()
		{
			return (_reasons != null ? _reasons.GetHashCode() : 0);
		}
	}
}