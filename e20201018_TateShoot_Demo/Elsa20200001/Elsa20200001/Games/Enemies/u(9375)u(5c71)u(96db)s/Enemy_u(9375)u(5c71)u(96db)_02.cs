﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies.鍵山雛s
{
	public class Enemy_鍵山雛_02 : Enemy
	{
		public Enemy_鍵山雛_02(double x, double y)
			: base(x, y, Kind_e.ENEMY, 2000, 0)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			double a_mahoujin = 0.0;

			for (int frame = 0; ; frame++)
			{
				DDUtils.Approach(ref this.X, Consts.FIELD_W / 2 + Math.Sin(frame / 101.0) * 20.0, 0.993);
				DDUtils.Approach(ref this.Y, Consts.FIELD_H / 2 + Math.Sin(frame / 103.0) * 20.0, 0.993);

				if (30 < frame && frame % 3 == 0)
				{
					for (int c = 0; c < 2; c++)
					{
						double rad = frame / 27.0 + (Math.PI * 2.0) * c / 2.0;
						D2Point pt = DDUtils.AngleToPoint(rad, 20.0);

						Game.I.Enemies.Add(new Enemy_鍵山雛_Tama_02(this.X + pt.X, this.Y + pt.Y, rad, 0.9, EnemyCommon.TAMA_COLOR_e.RED));
						Game.I.Enemies.Add(new Enemy_鍵山雛_Tama_02(this.X + pt.X, this.Y + pt.Y, rad, 0.0, EnemyCommon.TAMA_COLOR_e.GREEN));
						Game.I.Enemies.Add(new Enemy_鍵山雛_Tama_02(this.X + pt.X, this.Y + pt.Y, rad, -0.9, EnemyCommon.TAMA_COLOR_e.BLUE));
					}
				}

				if (30 < frame && frame % 20 == 0)
				{
					for (int c = 0; c < 3; c++)
					{
						double rad = DDUtils.GetAngle(Game.I.Player.X - this.X, Game.I.Player.Y - this.Y) + (Math.PI * 2.0) * (c * 2 + 1) / 6.0;
						D2Point pt = DDUtils.AngleToPoint(rad, 50.0);

						Game.I.Enemies.Add(new Enemy_鍵山雛_Tama_03(this.X, this.Y, rad, EnemyCommon.TAMA_COLOR_e.PINK));
					}
				}

				{
					const int TRANS_FRAME = 60;

					// ボム消し
					// このフレームにおけるボム当たり判定は既に展開しているので、ボスの当たり判定を展開する前のフレームで消す。
					if (frame == TRANS_FRAME)
						Game.I.Shots.RemoveAll(v => v.Kind == Shots.Shot.Kind_e.BOMB);

					if (TRANS_FRAME < frame)
					{
						DDUtils.Approach(ref a_mahoujin, 1.0, 0.99);
						Game.I.AddEnemyCrash(DDCrashUtils.Circle(new D2Point(this.X, this.Y), 25.0), this);
					}
				}

				EnemyCommon_鍵山雛.Draw(this.X, this.Y, true, a_mahoujin);
				EnemyCommon_鍵山雛.DrawStatus(this);

				yield return true;
			}
		}

		public override void Killed()
		{
			EnemyCommon.Killed(this, 0);
		}
	}
}
