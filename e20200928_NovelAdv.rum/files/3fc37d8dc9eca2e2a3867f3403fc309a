using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games
{
	public class Scenario
	{
		private const string SCENARIO_FILE_PREFIX = "Etoile\\G4NovelAdv\\Scenario\\";
		private const string SCENARIO_FILE_SUFFIX = ".txt";

		public string Name;
		public List<ScenarioPage> Pages = new List<ScenarioPage>();

		public Scenario(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new DDError();

			this.Name = name;
			this.Pages.Clear();

			byte[] fileData;

			{
				const string DEVENV_SCENARIO_DIR = "シナリオデータ";
				const string DEVENV_SCENARIO_SUFFIX = ".txt";

				if (Directory.Exists(DEVENV_SCENARIO_DIR))
				{
					string file = Path.Combine(DEVENV_SCENARIO_DIR, name + DEVENV_SCENARIO_SUFFIX);

					fileData = File.ReadAllBytes(file);
				}
				else
				{
					string file = SCENARIO_FILE_PREFIX + name + SCENARIO_FILE_SUFFIX;

					fileData = DDResource.Load(file);
				}
			}

			string text = JString.ToJString(fileData, true, true, true, true);

			text = text.Replace('\t', ' '); // タブスペースと空白 -> 空白に統一

			string[] lines = FileTools.TextToLines(text);
			ScenarioPage page = null;

			foreach (string fLine in lines)
			{
				string line = fLine.Trim();

				if (line == "")
					continue;

				if (line[0] == ';') // ? コメント行
					continue;

				if (line[0] == '/')
				{
					page = new ScenarioPage()
					{
						Subtitle = line.Substring(1)
					};

					this.Pages.Add(page);
				}
				else if (page == null)
				{
					throw new DDError("シナリオの先頭は /xxx でなければなりません。");
				}
				else if (line[0] == '!')
				{
					string[] tokens = line.Substring(1).Split(' ').Where(v => v != "").ToArray();

					page.Commands.Add(new ScenarioCommand(tokens));
				}
				else
				{
					page.Lines.Add(line);
				}
			}
			this.各ページの各行の長さ調整();
		}

		private void 各ページの各行の長さ調整()
		{
			foreach (ScenarioPage page in this.Pages)
			{
				for (int index = 0; index < page.Lines.Count; index++)
				{
					if (ScenarioPage.LINE_LEN_MAX < page.Lines[index].Length)
					{
						page.Lines.Insert(index + 1, page.Lines[index].Substring(ScenarioPage.LINE_LEN_MAX));
						page.Lines[index] = page.Lines[index].Substring(0, ScenarioPage.LINE_LEN_MAX);
					}
				}
			}
		}
	}
}
