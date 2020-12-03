using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using DxLibDLL;
using Charlotte.Commons;

namespace Charlotte.Games
{
	public class 波紋効果 : DDTask
	{
		private const int PIECE_WH = 60;
		private const int PIECES_W = 16;
		private const int PIECES_H = 9;

		private static bool S_Inited = false;
		private static DDSubScreen Screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
		private static DDSubScreen[,] Pieces = new DDSubScreen[PIECES_W, PIECES_H];

		private double Center_X;
		private double Center_Y;

		public 波紋効果(double center_x, double center_y)
		{
			if (!S_Inited)
			{
				S_Inited = true;

				for (int x = 0; x < PIECES_W; x++)
					for (int y = 0; y < PIECES_H; y++)
						Pieces[x, y] = new DDSubScreen(PIECE_WH, PIECE_WH);
			}

			this.Center_X = center_x;
			this.Center_Y = center_y;
		}

		public override IEnumerable<bool> E_Task()
		{
			foreach (DDScene scene in DDSceneUtils.Create(600))
			{
				for (int x = 0; x < PIECES_W; x++)
					for (int y = 0; y < PIECES_H; y++)
						using (Pieces[x, y].Section())
							DX.DrawRectGraph(0, 0, x * PIECE_WH, y * PIECE_WH, (x + 1) * PIECE_WH, (y + 1) * PIECE_WH, DDGround.MainScreen.GetHandle(), 0);

				using (Screen.Section())
				{
					for (int x = 0; x < PIECES_W; x++)
					{
						for (int y = 0; y < PIECES_H; y++)
						{
							D2Point lt = new D2Point((x + 0) * PIECE_WH, (y + 0) * PIECE_WH);
							D2Point rt = new D2Point((x + 1) * PIECE_WH, (y + 0) * PIECE_WH);
							D2Point rb = new D2Point((x + 1) * PIECE_WH, (y + 1) * PIECE_WH);
							D2Point lb = new D2Point((x + 0) * PIECE_WH, (y + 1) * PIECE_WH);

							lt = this.波紋効果による頂点の移動(lt, scene.Rate);
							rt = this.波紋効果による頂点の移動(rt, scene.Rate);
							rb = this.波紋効果による頂点の移動(rb, scene.Rate);
							lb = this.波紋効果による頂点の移動(lb, scene.Rate);

							DDDraw.DrawFree(Pieces[x, y].ToPicture(), lt, rt, rb, lb);
						}
					}
				}

				DDDraw.DrawSimple(Screen.ToPicture(), 0, 0);

				yield return true;
			}
		}

		private D2Point 波紋効果による頂点の移動(D2Point pt, double rate)
		{
			// pt == 画面上の座標

			// マップ上の座標に変更
			pt.X += DDGround.ICamera.X;
			pt.Y += DDGround.ICamera.Y;

			// 波紋の中心からの相対座標に変更
			pt.X -= this.Center_X;
			pt.Y -= this.Center_Y;

			double wave_r = DDUtils.Parabola(rate / 2.0) * 2500.0;
			double distance = DDUtils.GetDistance(pt);
			double d = distance;

			d -= wave_r;

			// distance の -50 ～ 50 を 0.0 ～ 1.0 にする。
			d /= 50.0;
			d += 1.0;
			d /= 2.0;

			if (0.0 < d && d < 1.0)
			{
				d *= 2.0;

				if (1.0 < d)
					d = 2.0 - d;

				distance += DDUtils.SCurve(d) * 50.0 * (1.0 - rate);

				DDUtils.MakeXYSpeed(0.0, 0.0, pt.X, pt.Y, distance, out pt.X, out pt.Y); // distance を pt に反映する。
			}

			// restore -- 波紋の中心からの相対座標 -> マップ上の座標
			pt.X += this.Center_X;
			pt.Y += this.Center_Y;

			// restore -- マップ上の座標 -> 画面上の座標
			pt.X -= DDGround.ICamera.X;
			pt.Y -= DDGround.ICamera.Y;

			return pt;
		}
	}
}
