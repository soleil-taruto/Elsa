using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class ArgsReader
	{
		public ArgsReader()
		{ }

		public ArgsReader(string[] args, int argIndex = 0)
		{
			this.SetArgs(args, argIndex);
		}

		private string[] Args = null;
		private int ArgIndex;

		public void SetArgs(string[] args, int argIndex = 0)
		{
			this.Args = args;
			this.ArgIndex = argIndex;

			this.ReadSysArgs();
		}

		private void ReadSysArgs()
		{
			while (this.HasArgs())
			{
				if (this.ArgIs("//$")) // 読み込み中止
				{
					break;
				}
				if (this.ArgIs("//F"))
				{
					string text = File.ReadAllText(this.NextArg(), StringTools.ENCODING_SJIS);
					string[] subArgs = TokenizeArgs(text);

					this.Args = this.Args.Concat(subArgs).ToArray();
					continue;
				}
				if (this.ArgIs("//R"))
				{
					string[] subArgs = File.ReadAllLines(this.NextArg(), StringTools.ENCODING_SJIS);

					this.Args = this.Args.Concat(subArgs).ToArray();
					continue;
				}
				break;
			}
		}

		private static string[] TokenizeArgs(string str)
		{
			List<string> args = new List<string>();
			StringBuilder buff = new StringBuilder();
			bool literalMode = false;

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (literalMode)
				{
					if (chr == '"')
					{
						literalMode = false;
						continue;
					}
				}
				else
				{
					if (chr <= ' ')
					{
						args.Add(buff.ToString());
						buff = new StringBuilder();
						continue;
					}
					if (chr == '"')
					{
						literalMode = true;
						continue;
					}
				}

				if (chr == '\\')
					chr = str[++index];

				buff.Append(chr);
			}
			args.Add(buff.ToString());
			return args.ToArray();
		}

		private class SpellInfo
		{
			public string Spell;
			public Action SpellAction;
		}

		private List<SpellInfo> SpellInfos = new List<SpellInfo>();

		public void Add(string spell, Action action)
		{
			this.SpellInfos.Add(new SpellInfo()
			{
				Spell = spell,
				SpellAction = action,
			});
		}

		public void Perform(string[] args, int argIndex = 0)
		{
			this.SetArgs(args, argIndex);
			this.Perform();
		}

		public void Perform()
		{
			while (this.Perform_Main())
			{ }
		}

		private bool Perform_Main()
		{
			foreach (SpellInfo info in this.SpellInfos)
			{
				if (this.ArgIs(info.Spell))
				{
					info.SpellAction();
					return true;
				}
			}
			return false;
		}

		public bool HasArgs(int count = 1)
		{
			return count <= this.Args.Length - this.ArgIndex;
		}

		public bool ArgIs(string spell)
		{
			if (this.HasArgs() && this.GetArg().ToUpper() == spell.ToUpper())
			{
				this.ArgIndex++;
				return true;
			}
			return false;
		}

		public string GetArg(int index = 0)
		{
			return this.Args[this.ArgIndex + index];
		}

		public string NextArg()
		{
			string arg = this.GetArg();
			this.ArgIndex++;
			return arg;
		}
	}
}
