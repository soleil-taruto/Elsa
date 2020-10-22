﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Shots
{
	public class Shot_Normal : Shot
	{
		private Shot_Normal()
		{ }

		private double R;

		public static Shot_Normal Create(double x, double y, double r)
		{
			return new Shot_Normal()
			{
				X = x,
				Y = y,
				Kind = Kind_e.STANDARD,
				AttackPoint = 2,
				R = r,
			};
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				double ax = 0.0;
				double ay = -20.0;

				DDUtils.Rotate(ref ax, ref ay, this.R);

				this.X += ax;
				this.Y += ay;

				if (DDUtils.IsOut(new D2Point(this.X, this.Y), new D4Rect(0, 0, Consts.FIELD_W, Consts.FIELD_H)))
					break;

				DDDraw.SetAlpha(Consts.SHOT_A);
				DDDraw.DrawBegin(Ground.I.Picture2.D_SHOT, this.X, this.Y);
				DDDraw.DrawRotate(this.R);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				Game.I.AddShotCrash(DDCrashUtils.Circle(
					new D2Point(this.X, this.Y),
					12.0
					),
					this
					);

				yield return true;
			}
		}
	}
}
