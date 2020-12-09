using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public static class 波紋効果
	{
		// HACK: 謎の重さがある。

		private const int PIECE_WH = 60;
		private const int PIECES_W = 16;
		private const int PIECES_H = 9;

		private static DDSubScreen Screen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);
		private static DDSubScreen Piece = new DDSubScreen(PIECE_WH, PIECE_WH);

		private static DDTaskList 波紋s = new DDTaskList();

		public static void Add(double x, double y)
		{
			if (10 <= 波紋s.Count)
			{
				波紋s.RemoveAt(0);
			}
			波紋s.Add(new 波紋Task(x, y).Task);
		}

		public static int Count
		{
			get
			{
				return 波紋s.Count;
			}
		}

		private static D2Point[,] PointTable = new D2Point[PIECES_W + 1, PIECES_H + 1];

		private class 波紋Task : DDTask
		{
			private double Center_X;
			private double Center_Y;

			public 波紋Task(double x, double y)
			{
				this.Center_X = x;
				this.Center_Y = y;
			}

			public override IEnumerable<bool> E_Task()
			{
				foreach (DDScene scene in DDSceneUtils.Create(600))
				{
					for (int x = 0; x <= PIECES_W; x++)
						for (int y = 0; y <= PIECES_H; y++)
							PointTable[x, y] = this.波紋効果による頂点の移動(PointTable[x, y], scene.Rate);

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

				// d の -50 ～ 50 を 0.0 ～ 1.0 にする。
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

		public static void EachFrame()
		{
			for (int x = 0; x <= PIECES_W; x++)
				for (int y = 0; y <= PIECES_H; y++)
					PointTable[x, y] = new D2Point(x * PIECE_WH, y * PIECE_WH);

			波紋s.ExecuteAllTask();

			for (int x = 0; x < PIECES_W; x++)
			{
				for (int y = 0; y < PIECES_H; y++)
				{
#if true // v120607 -- using 不使用 ver -- 多少軽くなった。
					Piece.ChangeDrawScreen();

					DX.DrawRectGraph(0, 0, x * PIECE_WH, y * PIECE_WH, (x + 1) * PIECE_WH, (y + 1) * PIECE_WH, DDGround.MainScreen.GetHandle(), 0);

					Screen.ChangeDrawScreen();

					{
						D2Point lt = PointTable[x + 0, y + 0];
						D2Point rt = PointTable[x + 1, y + 0];
						D2Point rb = PointTable[x + 1, y + 1];
						D2Point lb = PointTable[x + 0, y + 1];

						DDDraw.SetIgnoreError();
						DDDraw.DrawFree(Piece.ToPicture(), lt, rt, rb, lb);
						DDDraw.Reset();
					}
#else // old
					using (Piece.Section())
					{
						DX.DrawRectGraph(0, 0, x * PIECE_WH, y * PIECE_WH, (x + 1) * PIECE_WH, (y + 1) * PIECE_WH, DDGround.MainScreen.GetHandle(), 0);
					}
					using (Screen.Section())
					{
						D2Point lt = PointTable[x + 0, y + 0];
						D2Point rt = PointTable[x + 1, y + 0];
						D2Point rb = PointTable[x + 1, y + 1];
						D2Point lb = PointTable[x + 0, y + 1];

						DDDraw.SetIgnoreError();
						DDDraw.DrawFree(Piece.ToPicture(), lt, rt, rb, lb);
						DDDraw.Reset();
					}
#endif
				}
			}

			DDGround.MainScreen.ChangeDrawScreen(); // v120607

			DDDraw.DrawSimple(Screen.ToPicture(), 0, 0);
		}
	}
}
