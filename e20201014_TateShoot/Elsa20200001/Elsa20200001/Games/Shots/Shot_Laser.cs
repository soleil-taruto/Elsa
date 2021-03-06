﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;

namespace Charlotte.Games.Shots
{
	public class Shot_Laser : Shot
	{
		private Shot_Laser()
		{ }

		private int Level; // 0 ～ Consts.PLAYER_LEVEL_MAX

		public static Shot_Laser Create(double x, double y, int level)
		{
			return new Shot_Laser()
			{
				X = x,
				Y = y,
				Kind = Kind_e.STANDARD,
				AttackPoint = new int[] { 3, 6, 7, 13, 15, 21 }[level],
				Level = level,
			};
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				this.Y -= 18.0;

				if (this.Y < 0.0)
					break;

				DDDraw.SetAlpha(Consts.SHOT_A);
				DDDraw.DrawBegin(Ground.I.Picture2.D_LASER, this.X, this.Y);
				DDDraw.DrawZoom(1.0 + (0.5 * this.Level) / Consts.PLAYER_LEVEL_MAX);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				Game.I.AddShotCrash(DDCrashUtils.Circle(
					new D2Point(this.X, this.Y),
					32.0 + (16.0 * this.Level) / Consts.PLAYER_LEVEL_MAX
					),
					this
					);

				yield return true;
			}
		}
	}
}
