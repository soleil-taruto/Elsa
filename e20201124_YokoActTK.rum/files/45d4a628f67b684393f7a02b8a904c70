using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_B0003_Egg : Enemy
	{
		private double StartX;
		private double StartY;
		private double DestX;
		private double DestY;

		public Enemy_B0003_Egg(double x, double y, double destX, double destY)
			: base(x, y, 0, SCommon.IMAX, false)
		{
			this.StartX = x;
			this.StartY = y;
			this.DestX = destX;
			this.DestY = destY;
		}

		private const int FRAME_MAX = 40;
		private int Frame = 0;

		public override void Draw()
		{
			if (FRAME_MAX < this.Frame)
			{
				Game.I.Enemies.Add(new Enemy_B0003(this.DestX, this.DestY));
				this.DeadFlag = true;
			}

			this.X = DDUtils.AToBRate(this.StartX, this.DestX, DDUtils.SCurve(this.Frame * 1.0 / FRAME_MAX));
			this.Y = DDUtils.AToBRate(this.StartY, this.DestY, DDUtils.SCurve(this.Frame * 1.0 / FRAME_MAX));

			this.DrawOnly();

			this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 50.0);

			this.Frame++;
		}

		public override Enemy GetClone()
		{
			return new Enemy_B0003_Egg(this.X, this.Y, this.DestX, this.DestY)
			{
				Frame = this.Frame,
			};
		}

		public override void DrawOnly()
		{
			DDDraw.SetBright(1.0, 0.0, 0.0);
			DDDraw.DrawBegin(Ground.I.Picture.Enemy_B0003, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
			DDDraw.DrawRotate(this.Frame * 10.0 / FRAME_MAX);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}
	}
}
