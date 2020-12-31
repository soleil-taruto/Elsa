using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies.ルーミアs
{
	public static class EnemyCommon_ルーミア
	{
		public static void PutCrash(Enemy enemy, int frame)
		{
			if (frame == 0)
			{
				Game.I.Shots.Add(new Shot_BossBomb());
			}
			else if (frame < EnemyConsts_ルーミア.BOSS_BOMB_FRAME)
			{
				// noop
			}
			else if (frame < EnemyConsts_ルーミア.TRANS_FRAME)
			{
				Game.I.Shots.RemoveAll(v => v.Kind == Shot.Kind_e.BOMB); // ボム消し
			}
			else
			{
				enemy.Crash = DDCrashUtils.Circle(new D2Point(enemy.X, enemy.Y), 25.0);
			}

			// ついでに、ステータス表示
			{
				DDGround.EL.Add(() =>
				{
					DDPrint.SetPrint(20, 20, 20);
					DDPrint.SetBorder(new I3Color(128, 0, 0));
					DDPrint.PrintLine("[RUMIA-HP=" + enemy.HP + "]");
					//DDPrint.PrintLine("[RUMIA=" + enemy.X.ToString("F1") + "," + enemy.Y.ToString("F1") + "]");
					DDPrint.Reset();

					return false;
				});
			}
		}

		private static double Last_X = GameConsts.FIELD_W / 2;

		public static void Draw(double x, double y)
		{
			int picIndex;
			double xZoom;

			{
				const double MARGIN = 2.0;

				if (x < Last_X - MARGIN)
				{
					picIndex = 1;
					xZoom = 1.0;
				}
				else if (Last_X + MARGIN < x)
				{
					picIndex = 1;
					xZoom = -1.0;
				}
				else
				{
					picIndex = 0;
					xZoom = 1.0;
				}
			}

			Last_X = x;

			DDDraw.DrawBegin(Ground.I.Picture2.ルーミア[picIndex], x, y);
			DDDraw.DrawZoom_X(xZoom);
			DDDraw.DrawEnd();
		}
	}
}
