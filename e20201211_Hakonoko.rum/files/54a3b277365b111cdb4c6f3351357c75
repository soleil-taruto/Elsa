using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies.Events
{
	public class Enemy_Event9001 : Enemy
	{
		public Enemy_Event9001(D2Point pos)
			: base(pos)
		{ }

		public override void Draw()
		{
			if (DDUtils.GetDistance(new D2Point(this.X, this.Y), new D2Point(Game.I.Player.X, Game.I.Player.Y)) < 50.0)
			{
				if (!Game.I.Enemies.Iterate().Any(enemy => enemy is Enemy_MeteorLoader)) // ? メテオローダー未設置
				{
					Game.I.Enemies.Add(new Enemy_MeteorLoader(new D2Point(0, 0))); // メテオローダー設置
				}
			}
		}

		public override Enemy GetClone()
		{
			return new Enemy_Event9001(new D2Point(this.X, this.Y));
		}
	}
}
