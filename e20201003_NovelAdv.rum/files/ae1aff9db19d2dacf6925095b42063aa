using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using DxLibDLL;

namespace Charlotte.Games.Surfaces
{
	public class Surface_SystemButtons : Surface
	{
		public static bool Hide = false; // Game から制御される。

		private double A = 1.0;
		private bool LControlSkipMode = false;

		public override void Draw()
		{
			// 入力：スキップモード
			if (
				1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL) ||
				1 <= DDInput.R.GetInput()
				)
			{
				Game.I.SkipMode = true;
				this.LControlSkipMode = true;
			}
			else
			{
				if (this.LControlSkipMode)
				{
					Game.I.SkipMode = false;
					this.LControlSkipMode = false;
				}
			}

			// ---- ここから描画

			DDUtils.Approach(ref this.A, Hide ? 0.0 : 1.0, 0.9);

			var buttons = new[]
			{
				// p: 画像, fp: フォーカス時の画像, oh: オプショナル・ハイライト
				new { p = Ground.I.Picture.MessageFrame_Save, fp = Ground.I.Picture.MessageFrame_Save2, oh = false },
				new { p = Ground.I.Picture.MessageFrame_Load, fp = Ground.I.Picture.MessageFrame_Load2, oh = false },
				new { p = Ground.I.Picture.MessageFrame_Skip, fp = Ground.I.Picture.MessageFrame_Skip2, oh = Game.I.SkipMode },
				new { p = Ground.I.Picture.MessageFrame_Auto, fp = Ground.I.Picture.MessageFrame_Auto2, oh = Game.I.AutoMode },
				new { p = Ground.I.Picture.MessageFrame_Log, fp = Ground.I.Picture.MessageFrame_Log2, oh = false },
				new { p = Ground.I.Picture.MessageFrame_Menu, fp = Ground.I.Picture.MessageFrame_Menu2, oh = false },
			};

			int selSysBtnIdx = -1;

			for (int index = 0; index < buttons.Length; index++)
			{
				bool focused = index == Game.I.SelectedSystemButtonIndex;

				DDDraw.SetAlpha(this.A * (focused ? 1.0 : 0.8));
				DDDraw.DrawCenter(
					focused || buttons[index].oh ? buttons[index].fp : buttons[index].p,
					GameConsts.SYSTEM_BUTTON_X + index * GameConsts.SYSTEM_BUTTON_X_STEP,
					GameConsts.SYSTEM_BUTTON_Y
					);
				DDDraw.Reset();

				if (DDDraw.IsMouseCrashed())
				{
					selSysBtnIdx = index;
				}
			}
			Game.I.SelectedSystemButtonIndex = selSysBtnIdx;

			// 隠しているなら選択出来ない。
			if (Hide)
				Game.I.SelectedSystemButtonIndex = -1;
		}
	}
}
