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
	/// <summary>
	/// テスト用_敵
	/// </summary>
	public class Enemy_B0002 : Enemy
	{
		public Enemy_B0002(double x, double y)
			: base(x, y, 10, 3, false)
		{ }

		private const int HIT_BACK_FRAME_MAX = 10;

		private int Frame = 0;
		private int HitBackFrame = 0; // 0 == 無効, 1～ ヒットバック中

		public override void Draw()
		{
			double SPEED = 2.0;
			double xBuru = 0.0;
			double yBuru = 0.0;

			if (1 <= this.HitBackFrame)
			{
				int frame = this.HitBackFrame - 1;

				if (HIT_BACK_FRAME_MAX < frame)
				{
					this.HitBackFrame = 0;
					goto endHitBack;
				}
				this.HitBackFrame++;

				// ----

				double rate = (double)frame / HIT_BACK_FRAME_MAX;

				SPEED = 0.0;
				xBuru = (1.0 - rate) * 30.0 * DDUtils.Random.Real2();
				yBuru = (1.0 - rate) * 30.0 * DDUtils.Random.Real2();
			}
		endHitBack:

			switch (this.Frame / 60 % 4)
			{
				case 0: this.X += SPEED; break;
				case 1: this.Y += SPEED; break;
				case 2: this.X -= SPEED; break;
				case 3: this.Y -= SPEED; break;

				default:
					throw null; // never
			}

			if (1 <= this.HitBackFrame)
				DDDraw.SetBright(1.0, 0.8, 1.0);
			else
				DDDraw.SetBright(1.0, 0.5, 0.0);

			DDDraw.DrawBegin(
				DDGround.GeneralResource.WhiteBox,
				this.X - DDGround.ICamera.X + xBuru,
				this.Y - DDGround.ICamera.Y + yBuru
				);
			DDDraw.DrawSetSize(100.0, 100.0);
			DDDraw.DrawEnd();
			DDDraw.Reset();

			this.Crash = DDCrashUtils.Rect_CenterSize(new D2Point(this.X, this.Y), new D2Size(100.0, 100.0));

			this.Frame++;
		}

		public override void Damaged(Shot shot)
		{
			//this.X += 10.0 * (shot.FacingLeft ? -1 : 1); // ヒットバック
			this.HitBackFrame = 1;
			base.Damaged(shot);
		}

		public override Enemy GetClone()
		{
			return new Enemy_B0002(this.X, this.Y)
			{
				Frame = this.Frame,
			};
		}
	}
}
