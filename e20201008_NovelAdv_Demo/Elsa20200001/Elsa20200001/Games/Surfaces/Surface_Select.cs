﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using DxLibDLL;
using Charlotte.Commons;

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
			Game.I.SkipMode = false;

			if (
				this.Options.Count < GameConsts.SELECT_OPTION_MIN ||
				this.Options.Count > GameConsts.SELECT_OPTION_MAX
				)
				throw new DDError("選択肢の個数に問題があります。");

			// ---- 入力ここから

			{
				int moving = 0;

				if (
					DDKey.IsPound(DX.KEY_INPUT_UP) ||
					DDInput.DIR_8.IsPound()
					)
					moving = -1;

				if (
					DDKey.IsPound(DX.KEY_INPUT_DOWN) ||
					DDInput.DIR_2.IsPound()
					)
					moving = 1;

				if (moving != 0)
				{
					int optIndex = this.GetMouseFocusedIndex();

					if (optIndex == -1)
					{
						optIndex = 0;
					}
					else
					{
						optIndex += this.Options.Count + moving;
						optIndex %= this.Options.Count;
					}

					DDMouse.X =
						GameConsts.SELECT_FRAME_L +
						Ground.I.Picture.MessageFrame_Button2.Get_W() -
						10;
					DDMouse.Y =
						GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * optIndex +
						Ground.I.Picture.MessageFrame_Button2.Get_H() -
						10;

					DDMouse.ApplyPos();
				}
			}

			// ---- ここから描画

			for (int index = 0; index < GameConsts.SELECT_FRAME_NUM; index++)
			{
				DDPicture picture = Ground.I.Picture.MessageFrame_Button;

				if (index < this.Options.Count)
				{
					picture = Ground.I.Picture.MessageFrame_Button2;

					if (this.Options[index].MouseFocused)
						picture = Ground.I.Picture.MessageFrame_Button3;
				}

				DDDraw.DrawSimple(
					picture,
					GameConsts.SELECT_FRAME_L,
					GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index
					);
			}
			for (int index = 0; index < this.Options.Count; index++)
			{
				const int title_x = 80;
				const int title_y = 28;

				DDFontUtils.DrawString(
					 GameConsts.SELECT_FRAME_L + title_x,
					 GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index + title_y,
					 this.Options[index].Title,
					 DDFontUtils.GetFont("Kゴシック", 16),
					 false,
					 new I3Color(110, 100, 90)
					 );

				// フォーカスしている選択項目を再設定
				{
					bool mouseOut = DDUtils.IsOut(
						new D2Point(
							DDMouse.X,
							DDMouse.Y
							),
						new D4Rect(
							GameConsts.SELECT_FRAME_L,
							GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index,
							Ground.I.Picture.MessageFrame_Button2.Get_W(),
							Ground.I.Picture.MessageFrame_Button2.Get_H()
							)
						);

					this.Options[index].MouseFocused = !mouseOut;
				}
			}
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			//int c = 0;

			if (command == "選択肢")
			{
				this.Options.Add(new OptionInfo()
				{
					Title = arguments[0],
				});
			}
			else if (command == "分岐先")
			{
				this.Options[this.Options.Count - 1].ScenarioName = arguments[0];
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string[] Serialize_02()
		{
			List<string> lines = new List<string>();

			lines.Add(this.Options.Count.ToString());

			foreach (OptionInfo option in this.Options)
			{
				lines.Add(option.Title);
				lines.Add(option.ScenarioName);
			}
			return lines.ToArray();
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			{
				int count = int.Parse(lines[c++]);

				this.Options.Clear();

				for (int index = 0; index < count; index++)
				{
					OptionInfo option = new OptionInfo();

					option.Title = lines[c++];
					option.ScenarioName = lines[c++];

					this.Options.Add(option);
				}
			}
		}
	}
}
