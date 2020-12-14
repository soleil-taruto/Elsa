﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Enemies.神奈子s
{
	public class Enemy_神奈子0001 : Enemy
	{
		public Enemy_神奈子0001(double x, double y)
			: base(x, y, 100, 1, false)
		{ }

		private Func<bool> _hitBack = () => false;

		public override IEnumerable<bool> E_Draw()
		{
			// ---- game_制御 ----

			Ground.I.Music.神さびた古戦場.Play();

			// ----

			int boss_rot = 0;
			double boss_approach_rate = 1.0;

			for (; ; )
			{
				if (!_hitBack())
				{
					double x = Math.Cos(boss_rot / 100.0);
					double y = Math.Sin(boss_rot / 100.0);

					boss_rot++;

					x *= 0.4;
					y *= 0.4;

					x += 0.5;
					y += 0.5;

					x *= Game.I.Map.W * GameConsts.TILE_W;
					y *= Game.I.Map.H * GameConsts.TILE_H;

					DDUtils.Approach(ref this.X, x, boss_approach_rate);
					DDUtils.Approach(ref this.Y, y, boss_approach_rate);
					DDUtils.Approach(ref boss_approach_rate, 0.97, 0.99);

					bool facingLeft = Game.I.Player.X < this.X;

					DDDraw.DrawBegin(Ground.I.Picture2.Enemy_神奈子[0], this.X, this.Y);
					DDDraw.DrawSlide(20.0, 10.0);
					DDDraw.DrawZoom_X(facingLeft ? 1 : -1);
					DDDraw.DrawEnd();
				}

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 80.0);

				yield return true;
			}
		}

		public override void Damaged(Shot shot)
		{
			_hitBack = SCommon.Supplier(this.E_HitBack());
			base.Damaged(shot);
		}

		private IEnumerable<bool> E_HitBack()
		{
			foreach (DDScene scene in DDSceneUtils.Create(20))
			{
				double xBuru = (DDUtils.Random.Real() - 0.5) * 2 * 10.0;
				double yBuru = (DDUtils.Random.Real() - 0.5) * 2 * 10.0;

				bool facingLeft = Game.I.Player.X < this.X;

				DDDraw.DrawBegin(Ground.I.Picture2.Enemy_神奈子[12], this.X + xBuru, this.Y + yBuru);
				DDDraw.DrawSlide(20.0, -10.0);
				DDDraw.DrawZoom_X(facingLeft ? 1 : -1);
				DDDraw.DrawEnd();

				yield return true;
			}
		}

		public override void Killed()
		{
			Game.I.Enemies.Add(new Enemy_神奈子9901(this.X, this.Y));
		}
	}
}
