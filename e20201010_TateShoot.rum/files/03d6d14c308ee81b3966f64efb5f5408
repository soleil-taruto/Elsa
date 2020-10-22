using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Shots
{
	public class Shot_Strong : Shot
	{
		private Shot_Strong()
		{ }

		private double R;

		public static Shot_Strong Create(double x, double y, double r)
		{
			return new Shot_Strong()
			{
				X = x,
				Y = y,
				Kind = Kind_e.STANDARD,
				AttackPoint = 3,
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
				DDDraw.DrawZoom(1.2);
				DDDraw.DrawRotate(this.R);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				Game.I.AddShotCrash(DDCrashUtils.Circle(
					new D2Point(this.X, this.Y),
					14.4
					),
					this
					);

				yield return true;
			}
		}
	}
}
