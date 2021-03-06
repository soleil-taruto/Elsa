﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	/// <summary>
	/// テスト用_敵
	/// </summary>
	public class Enemy_B0001 : Enemy
	{
		public Enemy_B0001(double x, double y)
			: base(x, y, 0, 0, false)
		{ }

		private D2Point Speed = new D2Point();

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				double rot = DDUtils.GetAngle(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y);
				rot += DDUtils.Random.Real2() * 0.05;
				D2Point speedAdd = DDUtils.AngleToPoint(rot, 0.1);

				if (DDUtils.GetDistance(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y) < 50.0)
				{
					speedAdd *= -300.0;
				}
				this.Speed += speedAdd;
				this.Speed *= 0.93;

				this.X += this.Speed.X;
				this.Y += this.Speed.Y;

				DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawRotate(DDEngine.ProcFrame / 10.0);
				DDDraw.DrawEnd();

				// 当たり判定ナシ

				yield return true;
			}
		}
	}
}
