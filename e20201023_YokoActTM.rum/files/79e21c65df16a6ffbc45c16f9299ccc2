using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Tama0001 : Enemy
	{
		private D2Point Speed;

		public Enemy_Tama0001(double x, double y)
			: base(x, y, 0)
		{
			this.Speed = DDUtils.AngleToPoint(DDUtils.GetAngle(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y), 8.0);
		}

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				this.X += this.Speed.X;
				this.Y += this.Speed.Y;

				DDDraw.DrawCenter(Ground.I.Picture.Tama0001, this.X, this.Y);

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 16.0);

				yield return !DDUtils.IsOutOfScreen(new D2Point(this.X, this.Y), 16.0);
			}
		}
	}
}
