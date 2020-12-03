using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Pata : Enemy
	{
		public const double SPEED_NORMAL = 4.0;
		public const double SPEED_SLOW = 2.5;
		public const double SPEED_FAST = 7.0;

		private double XSpeed;
		private double YSpeed;
		private double Highest_Y;

		public Enemy_Pata(D2Point pos, double speed)
			: base(pos)
		{
			this.XSpeed = speed;
			this.YSpeed = 0.0;
			this.Highest_Y = pos.Y;
		}

		public override IEnumerable<bool> E_Draw()
		{
			const double 重力加速度 = 0.8984375; // オリジナルの 0.9 に近い有限桁の値 == 0.1110011(2)
			const double 落下最高速度 = 30.0;

			for (; ; )
			{
				this.X += this.XSpeed;
				this.Y += this.YSpeed;

				if (this.Y < this.Highest_Y)
				{
					DDUtils.Approach(ref this.YSpeed, 重力加速度, 0.9);
				}
				this.YSpeed = Math.Min(落下最高速度, this.YSpeed);

				int ix = SCommon.ToInt(this.X);
				int iy = SCommon.ToInt(this.Y);

				Around a2 = new Around(ix, iy, 2);

				if (
					!a2.Table[0, 0].IsEnemyWall() && a2.Table[0, 1].IsEnemyWall() ||
					!a2.Table[1, 0].IsEnemyWall() && a2.Table[1, 1].IsEnemyWall()
					)
				{
					this.YSpeed = -Math.Abs(this.YSpeed);
					goto endBounce;
				}
				if (
					a2.Table[0, 0].IsEnemyWall() &&
					a2.Table[1, 0].IsEnemyWall()
					)
				{
					this.YSpeed = Math.Abs(this.YSpeed);
					goto endBounce;
				}
				this.YSpeed += 重力加速度;

			endBounce:
				int xDirSign = 0;

				if (
					!a2.Table[0, 0].IsEnemyWall() && a2.Table[1, 0].IsEnemyWall() ||
					!a2.Table[0, 1].IsEnemyWall() && a2.Table[1, 1].IsEnemyWall()
					)
					xDirSign--;

				if (
					a2.Table[0, 0].IsEnemyWall() && !a2.Table[1, 0].IsEnemyWall() ||
					a2.Table[0, 1].IsEnemyWall() && !a2.Table[1, 1].IsEnemyWall()
					)
					xDirSign++;

				if (xDirSign != 0)
					this.XSpeed = Math.Abs(this.XSpeed) * xDirSign;

				DDDraw.SetBright(new I3Color(192, 32, 32));
				DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, SCommon.ToInt(this.X - DDGround.ICamera.X), SCommon.ToInt(this.Y - DDGround.ICamera.Y));
				DDDraw.DrawSetSize(Consts.TILE_W, Consts.TILE_H);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				this.Crash = DDCrashUtils.Rect_CenterSize(new D2Point(this.X, this.Y), new D2Size(Consts.TILE_W, Consts.TILE_H));

				yield return true;
			}
		}
	}
}
