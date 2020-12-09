using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Enemies
{
	public class Enemy_B0002 : Enemy
	{
		public Enemy_B0002(double x, double y)
			: base(x, y, 10, 3)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				const double SPEED = 2.0;

				switch (frame / 60 % 4)
				{
					case 0: this.X += SPEED; break;
					case 1: this.Y += SPEED; break;
					case 2: this.X -= SPEED; break;
					case 3: this.Y -= SPEED; break;

					default:
						throw null; // never
				}

				DDDraw.SetBright(1.0, 0.5, 0.0);
				DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawSetSize(100.0, 100.0);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				this.Crash = DDCrashUtils.Rect_CenterSize(new D2Point(this.X, this.Y), new D2Size(100.0, 100.0));

				yield return true;
			}
		}

		public override void Damaged(Shot shot)
		{
			this.X += 10.0 * (shot.FacingLeft ? -1 : 1); // ヒットバック
			base.Damaged(shot);
		}
	}
}
