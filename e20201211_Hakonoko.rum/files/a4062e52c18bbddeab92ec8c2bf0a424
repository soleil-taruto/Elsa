using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Meteor : Enemy
	{
		private double Angle;
		private double Speed;
		private I3Color Color;

		public Enemy_Meteor(D2Point pos)
			: base(pos)
		{
			this.Angle = DDUtils.GetAngle(Game.I.Player.X - pos.X, Game.I.Player.Y - pos.Y);
			this.Speed = 0.0;
			//this.Color = new I3Color(
			//    128 + DDUtils.Random.GetInt(128),
			//    128 + DDUtils.Random.GetInt(128),
			//    128 + DDUtils.Random.GetInt(128)
			//    );
			int clr = DDUtils.Random.GetInt(256);
			this.Color = new I3Color(255, clr, clr);
		}

		public override void Draw()
		{
			DDUtils.Approach(ref this.Speed, 10.0, 0.975);

			D2Point currSpeed = DDUtils.AngleToPoint(this.Angle, this.Speed);

			this.X += currSpeed.X;
			this.Y += currSpeed.Y;

			DDDraw.SetBright(this.Color);
			DDDraw.DrawBegin(Ground.I.Picture.WhiteBox, SCommon.ToInt(this.X - DDGround.ICamera.X), SCommon.ToInt(this.Y - DDGround.ICamera.Y));
			DDDraw.DrawSetSize(GameConsts.TILE_W, GameConsts.TILE_H);
			DDDraw.DrawEnd();
			DDDraw.Reset();

			this.Crash = DDCrashUtils.Rect_CenterSize(new D2Point(this.X, this.Y), new D2Size(GameConsts.TILE_W, GameConsts.TILE_H));

			if (DDUtils.IsOutOfCamera(new D2Point(this.X, this.Y), 50.0))
				this.DeadFlag = true;
		}

		public override Enemy GetClone()
		{
			return new Enemy_Meteor(new D2Point(this.X, this.Y));
		}
	}
}
