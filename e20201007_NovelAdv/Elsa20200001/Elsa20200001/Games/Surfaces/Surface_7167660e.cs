﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_照明 : Surface
	{
		private const int DARK_BOX_WH = 30;
		//private const int DARK_BOX_WH = 60; // 最大値, この値の約数でなければならない。
		private const int DARK_BOX_X_NUM = DDConsts.Screen_W / DARK_BOX_WH;
		private const int DARK_BOX_Y_NUM = DDConsts.Screen_H / DARK_BOX_WH;

		private const double A_APPR_RATE_NORMAL = 0.93;
		private const double A_APPR_RATE_FAST = 0.8;

		private static double AApprRate = A_APPR_RATE_NORMAL;

		private class DarkBoxInfo
		{
			public double TargetA = 0.0;
			public int X;
			public int Y;

			// <---- prm

			private double A = 0.0;

			public void Draw()
			{
				DDUtils.Approach(ref this.A, this.TargetA, AApprRate);

				if (this.A < 0.003) // ? 描画する必要無し -- DarkBox は沢山あるので、負荷軽減のため
					return;

				DDDraw.SetAlpha(this.A);
				DDDraw.SetBright(0, 0, 0);
				DDDraw.DrawRect(
					DDGround.GeneralResource.WhiteBox,
					this.X * DARK_BOX_WH,
					this.Y * DARK_BOX_WH,
					DARK_BOX_WH,
					DARK_BOX_WH
					);
				DDDraw.Reset();
			}
		}

		private DarkBoxInfo[,] DarkBoxTable = new DarkBoxInfo[DARK_BOX_X_NUM, DARK_BOX_Y_NUM];

		public Surface_照明()
		{
			for (int x = 0; x < DARK_BOX_X_NUM; x++)
			{
				for (int y = 0; y < DARK_BOX_Y_NUM; y++)
				{
					this.DarkBoxTable[x, y] = new DarkBoxInfo()
					{
						X = x,
						Y = y,
					};
				}
			}
		}

		public override void Draw()
		{
			for (int x = 0; x < DARK_BOX_X_NUM; x++)
			{
				for (int y = 0; y < DARK_BOX_Y_NUM; y++)
				{
					this.DarkBoxTable[x, y].Draw();
				}
			}
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			//int c = 0;

			if (command == "暗転開始")
			{
				this.Act.Add(SCommon.Supplier(this.暗転開始()));
			}
			else if (command == "暗転終了")
			{
				this.Act.Add(SCommon.Supplier(this.暗転終了()));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> 暗転開始()
		{
			return this.暗転(true);
		}

		private IEnumerable<bool> 暗転終了()
		{
			return this.暗転(false);
		}

		private IEnumerable<bool> 暗転(bool startFlag)
		{
			switch (DDUtils.Random.GetInt(3))
			{
				case 0:
					{
						const int D = 8;

						for (int x = 0; x < DARK_BOX_X_NUM / D; x++)
						{
							for (int y = 0; y < DARK_BOX_Y_NUM; y++)
							{
								for (int c = 0; c < D; c++)
									this.DarkBoxTable[x + c * (DARK_BOX_X_NUM / D), y].TargetA = startFlag ? 1.0 : 0.0;

								this.Draw();
								yield return true;
							}
						}
					}
					break;

				case 1:
					{
						const int D = 6;

						for (int y = 0; y < DARK_BOX_Y_NUM / D; y++)
						{
							for (int x = 0; x < DARK_BOX_X_NUM; x++)
							{
								for (int c = 0; c < D; c++)
									this.DarkBoxTable[x, y + c * (DARK_BOX_Y_NUM / D)].TargetA = startFlag ? 1.0 : 0.0;

								this.Draw();
								yield return true;
							}
						}
					}
					break;

				case 2:
					{
						int[] ps = Enumerable.Range(0, DARK_BOX_X_NUM * DARK_BOX_Y_NUM).ToArray();

						DDUtils.Random.Shuffle(ps);

						for (int index = 0; index < ps.Length; index++)
						{
							int p = ps[index];
							int x = p % DARK_BOX_X_NUM;
							int y = p / DARK_BOX_X_NUM;

							this.DarkBoxTable[x, y].TargetA = startFlag ? 1.0 : 0.0;

							if (index % 7 == 0)
							{
								this.Draw();
								yield return true;
							}
						}
					}
					break;

				default:
					throw null; // never
			}
			AApprRate = A_APPR_RATE_FAST;

			for (int c = 0; c < 20; c++) // 完全に切り替わるまで少し待つ
			{
				this.Draw();
				yield return true;
			}
			AApprRate = A_APPR_RATE_NORMAL; // restore
		}
	}
}
