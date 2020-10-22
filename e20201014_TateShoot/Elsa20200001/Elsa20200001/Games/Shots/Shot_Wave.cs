﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Shots
{
	public class Shot_Wave : Shot
	{
		private double R;
		private double RAdd;

		public static Shot_Wave Create(double x, double y, double r, double r_add)
		{
			return new Shot_Wave()
			{
				X = x,
				Y = y,
				Kind = Kind_e.STANDARD,
				AttackPoint = 1,
				R = r,
				RAdd = r_add,
			};
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				double ax = 0.0;
				double ay = -15.0;

				DDUtils.Rotate(ref ax, ref ay, this.R);

				this.X += ax;
				this.Y += ay;

				if (DDUtils.IsOut(new D2Point(this.X, this.Y), new D4Rect(0, 0, Consts.FIELD_W, Consts.FIELD_H)))
					break;

				this.R += this.RAdd;

				DDDraw.SetAlpha(Consts.SHOT_A);
				DDDraw.DrawBegin(Ground.I.Picture2.D_WAVESHOT, this.X, this.Y);
				DDDraw.DrawRotate(this.R);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				Game.I.AddShotCrash(DDCrashUtils.Circle(
					new D2Point(this.X, this.Y),
					24.0
					),
					this
					);

				yield return true;
			}
		}
	}
}
