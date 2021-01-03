﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_ハック0001 : Enemy
	{
		public Enemy_ハック0001(double x, double y)
			: base(x, y, 0, 0, false)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				if (DDUtils.GetDistance(new D2Point(Game.I.Player.X, Game.I.Player.Y), new D2Point(this.X, this.Y)) < 30.0)
				{
					foreach (bool v in this.E_ハック実行())
						yield return v;

					break;
				}

				DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawRotate(DDEngine.ProcFrame / 100.0);
				DDDraw.DrawEnd();

				DDPrint.SetPrint((int)this.X - DDGround.ICamera.X, (int)this.Y - DDGround.ICamera.Y);
				DDPrint.SetBorder(new I3Color(0, 0, 0));
				DDPrint.PrintLine("ハック0001");
				DDPrint.Reset();

				// 当たり判定無し

				yield return true;
			}
		}

		private IEnumerable<bool> E_ハック実行()
		{
			Game.I.UserInputDisabled = true;

			for (int c = 0; c < 30; c++)
				yield return true;

			Game.I.PlayerHacker.DIR_4 = true;

			for (int c = 0; c < 65; c++)
				yield return true;

			for (Game.I.PlayerHacker.Jump = 0; Game.I.PlayerHacker.Jump < 60; Game.I.PlayerHacker.Jump++)
				yield return true;

			for (Game.I.PlayerHacker.Jump = 0; Game.I.PlayerHacker.Jump < 60; Game.I.PlayerHacker.Jump++)
				yield return true;

			Game.I.PlayerHacker.Jump = 0;

			for (int c = 0; c < 60; c++)
				yield return true;

			Game.I.PlayerHacker.DIR_4 = false;

			for (int c = 0; c < 30; c++)
				yield return true;

			Game.I.UserInputDisabled = false;
		}
	}
}
