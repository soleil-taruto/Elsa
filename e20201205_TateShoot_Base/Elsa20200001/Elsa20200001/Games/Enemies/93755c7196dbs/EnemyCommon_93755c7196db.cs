using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies.鍵山雛s
{
	public static class EnemyCommon_鍵山雛
	{
		public static void Draw(double x, double y, bool spinning, double a_mahoujin = 0.0)
		{
			DDDraw.SetTaskList(Game.I.EL_AfterDrawWalls);
			DDDraw.SetAlpha(0.3 * a_mahoujin);
			DDDraw.DrawBegin(Ground.I.Picture2.D_MAHOJIN_HAJIKE_00[5], x, y);
			DDDraw.DrawZoom(3.0);
			DDDraw.DrawRotate(DDEngine.ProcFrame / 60.0);
			DDDraw.DrawEnd();
			DDDraw.Reset();

			if (spinning)
			{
				DDDraw.DrawCenter(Ground.I.Picture2.D_HINA_00[(DDEngine.ProcFrame / 5) % 3], x, y);
			}
			else
			{
				DDDraw.DrawCenter(Ground.I.Picture2.D_HINA, x, y);
			}
		}

		/// <summary>
		/// 暫定
		/// </summary>
		/// <param name="enemy">鍵山雛</param>
		public static void DrawStatus(Enemy enemy)
		{
			DDGround.EL.Add(() =>
			{
				DDPrint.SetPrint(525, 350, 20);
				DDPrint.SetBorder(new I3Color(192, 0, 0));
				DDPrint.PrintLine("KAGIYAMA-HINA-HP = " + enemy.HP);
				DDPrint.Reset();

				DDPrint.SetPrint(SCommon.ToInt(GameConsts.FIELD_L + enemy.X - 8 * 3), DDConsts.Screen_H - 16);
				DDPrint.SetBorder(new I3Color(255, 0, 0));
				DDPrint.Print("<BOSS>");
				DDPrint.Reset();

				return false;
			});
		}
	}
}
