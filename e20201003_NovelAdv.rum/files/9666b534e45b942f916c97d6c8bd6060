using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// 選択肢
	/// </summary>
	public class Surface_Select : Surface
	{
		public class OptionInfo
		{
			public string Title = "ここに選択肢に表示する文字列を設定します。";
			public string ScenarioName = GameConsts.DUMMY_SCENARIO_NAME;

			// <---- prm

			public bool MouseFocused = false;
		}

		public List<OptionInfo> Options = new List<OptionInfo>();

		public int GetMouseFocusedIndex()
		{
			for (int index = 0; index < this.Options.Count; index++)
				if (this.Options[index].MouseFocused)
					return index;

			return -1; // フォーカス無し
		}

		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			//int c = 0;

			if (command == ScenarioWords.COMMAND_選択肢)
			{
				this.Options.Add(new OptionInfo()
				{
					Title = arguments[0],
				});
			}
			else if (command == ScenarioWords.COMMAND_分岐先)
			{
				this.Options[this.Options.Count - 1].ScenarioName = arguments[0];
			}
			else if (command == ScenarioWords.COMMAND_Fire)
			{
				if (
					this.Options.Count < GameConsts.SELECT_OPTION_MIN ||
					this.Options.Count > GameConsts.SELECT_OPTION_MAX
					)
					throw new DDError("選択肢の個数に問題があります。");

				Game.I.Status.CurrSelect = this;
				this.RemoveMe();
			}
			else
			{
				throw new DDError();
			}
		}
	}
}
