﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Bボス0002 : Enemy
	{
		public Enemy_Bボス0002()
			: base(DDConsts.Screen_W + 96.0, DDConsts.Screen_H / 2.0, 100)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			Func<bool> f_移動 = SCommon.Supplier(this.移動());

			for (; ; )
			{
				f_移動();

				DDDraw.DrawCenter(Ground.I.Picture.Boss0002, this.X, this.Y);

				{
					const double WH = 192.0;
					const double CORNER_R = 30.0;

					this.Crash = DDCrashUtils.Multi(
						DDCrashUtils.Rect(new D4Rect(
							this.X - WH / 2.0 + CORNER_R,
							this.Y - WH / 2.0,
							WH - CORNER_R * 2.0,
							WH
							)),
						DDCrashUtils.Rect(new D4Rect(
							this.X - WH / 2.0,
							this.Y - WH / 2.0 + CORNER_R,
							WH,
							WH - CORNER_R * 2.0
							)),
						DDCrashUtils.Circle(new D2Point(this.X - (WH / 2.0 - CORNER_R), this.Y - (WH / 2.0 - CORNER_R)), CORNER_R),
						DDCrashUtils.Circle(new D2Point(this.X + (WH / 2.0 - CORNER_R), this.Y - (WH / 2.0 - CORNER_R)), CORNER_R),
						DDCrashUtils.Circle(new D2Point(this.X + (WH / 2.0 - CORNER_R), this.Y + (WH / 2.0 - CORNER_R)), CORNER_R),
						DDCrashUtils.Circle(new D2Point(this.X - (WH / 2.0 - CORNER_R), this.Y + (WH / 2.0 - CORNER_R)), CORNER_R)
						);
				}

				yield return true;
			}
		}

		private IEnumerable<bool> 移動()
		{
			for (int c = 0; c < 40; c++)
			{
				this.X -= 5.0;
				yield return true;
			}
			for (; ; )
			{
				for (int c = 0; c < 30; c++)
				{
					this.Y += 3.0;
					yield return true;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X -= 3.0;
					yield return true;
				}
				for (int c = 0; c < 60; c++)
				{
					this.Y -= 3.0;
					yield return true;
				}
				for (int c = 0; c < 40; c++)
				{
					this.X += 3.0;
					yield return true;
				}
				for (int c = 0; c < 30; c++)
				{
					this.Y += 3.0;
					yield return true;
				}
			}
		}
	}
}
