using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AttachString
	{
		public AttachString()
			: this(':', '$', '.')
		{ }

		public AttachString(char delimiter, char escapeChr, char escapedDelimiter)
			: this(delimiter, new EscapeString(delimiter.ToString(), escapeChr, escapedDelimiter.ToString()))
		{ }

		private char Delimiter;
		private EscapeString ES;

		public AttachString(char delimiter, EscapeString es)
		{
			this.Delimiter = delimiter;
			this.ES = es;
		}

		public string Untokenize(IEnumerable<string> tokens)
		{
			List<string> dest = new List<string>();

			foreach (string token in tokens)
				dest.Add(this.ES.Encode(token));

			dest.Add("");
			return string.Join(this.Delimiter.ToString(), dest);
		}

		public string[] Tokenize(string str)
		{
			string[] tokens = StringTools.Tokenize(str, this.Delimiter.ToString());
			List<string> dest = new List<string>(tokens.Length);

			foreach (string token in tokens)
				dest.Add(this.ES.Decode(token));

			dest.RemoveAt(dest.Count - 1);
			return dest.ToArray();
		}
	}
}
