﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Tiles
{
	public abstract class Tile_森林系 : Tile
	{
		private DrawTask _draw;

		/// <summary>
		/// 森林系のタイルを生成する。
		/// </summary>
		/// <param name="pictbl">画像テーブル(画像はタイル(32x32)であること)</param>
		/// <param name="密集_l">「密集」画像の左上(X-座標)</param>
		/// <param name="密集_t">「密集」画像の左上(Y-座標)</param>
		/// <param name="単独_l">「単独」画像の左上(X-座標)</param>
		/// <param name="単独_t">「単独」画像の左上(Y-座標)</param>
		/// <param name="groundPicture">地面の画像</param>
		/// <param name="単独セルPicture">密集・単独を使用出来ないマップセル用の画像</param>
		public Tile_森林系(DDPicture[,] pictbl, int 密集_l, int 密集_t, int 単独_l, int 単独_t, DDPicture groundPicture, DDPicture 単独セルPicture)
		{
			_draw = new DrawTask()
			{
				PictureTable = pictbl,
				単独_L = 単独_l,
				単独_T = 単独_t,
				密集_L = 密集_l,
				密集_T = 密集_t,
				GroundPicture = groundPicture,
				単独セルPicture = 単独セルPicture,
				F_IsFriend = this.IsFriend,
			};
		}

		protected abstract bool IsFriend(Tile tile);

		public override void Draw(double x, double y)
		{
			_draw.Point = new D2Point(x, y);

			if (!_draw.Execute())
				throw null; // never
		}

		private class DrawTask : DDTask
		{
			public DDPicture[,] PictureTable;
			public int 単独_L;
			public int 単独_T;
			public int 密集_L;
			public int 密集_T;
			public DDPicture GroundPicture;
			public DDPicture 単独セルPicture;
			public Predicate<Tile> F_IsFriend;

			// <---- prm

			public D2Point Point;

			// <---- Execute() prm

			public D2Point MapCellCenterPoint;
			public I2Point MapTablePoint;

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					this.MapCellCenterPoint = new D2Point(DDGround.ICamera.X + this.Point.X, DDGround.ICamera.Y + this.Point.Y);
					this.MapTablePoint = new I2Point((int)this.MapCellCenterPoint.X / GameConsts.TILE_W, (int)this.MapCellCenterPoint.Y / GameConsts.TILE_H);

					// 密集画像テーブルの位置
					bool 左列 = this.MapTablePoint.X % 2 == 0;
					bool 上段 = this.MapTablePoint.Y % 2 == 0;

					DDPicture picture;

					// 密集画像テーブルのどの位置に対応するか
					if (左列)
					{
						if (上段) // 左上
						{
							bool m左下 = this.IsFriend2x2(this.MapTablePoint.X - 1, this.MapTablePoint.Y - 0);
							bool m右上 = this.IsFriend2x2(this.MapTablePoint.X - 0, this.MapTablePoint.Y - 1);

							if (m左下)
							{
								if (m右上)
									picture = this.PictureTable[this.密集_L + 0, this.密集_T + 0];
								else
									picture = this.PictureTable[this.単独_L + 1, this.単独_T + 0];
							}
							else
							{
								if (m右上)
									picture = this.PictureTable[this.単独_L + 0, this.単独_T + 1];
								else
									picture = this.単独セルPicture;
							}
						}
						else // 左下
						{
							bool m左上 = this.IsFriend2x2(this.MapTablePoint.X - 1, this.MapTablePoint.Y - 1);
							bool m右下 = this.IsFriend2x2(this.MapTablePoint.X - 0, this.MapTablePoint.Y - 0);

							if (m左上)
							{
								if (m右下)
									picture = this.PictureTable[this.密集_L + 0, this.密集_T + 1];
								else
									picture = this.PictureTable[this.単独_L + 1, this.単独_T + 1];
							}
							else
							{
								if (m右下)
									picture = this.PictureTable[this.単独_L + 0, this.単独_T + 0];
								else
									picture = this.単独セルPicture;
							}
						}
					}
					else // 右列
					{
						if (上段) // 右上
						{
							bool m左上 = this.IsFriend2x2(this.MapTablePoint.X - 1, this.MapTablePoint.Y - 1);
							bool m右下 = this.IsFriend2x2(this.MapTablePoint.X - 0, this.MapTablePoint.Y - 0);

							if (m左上)
							{
								if (m右下)
									picture = this.PictureTable[this.密集_L + 1, this.密集_T + 0];
								else
									picture = this.PictureTable[this.単独_L + 1, this.単独_T + 1];
							}
							else
							{
								if (m右下)
									picture = this.PictureTable[this.単独_L + 0, this.単独_T + 0];
								else
									picture = this.単独セルPicture;
							}
						}
						else // 右下
						{
							bool m左下 = this.IsFriend2x2(this.MapTablePoint.X - 1, this.MapTablePoint.Y - 0);
							bool m右上 = this.IsFriend2x2(this.MapTablePoint.X - 0, this.MapTablePoint.Y - 1);

							if (m左下)
							{
								if (m右上)
									picture = this.PictureTable[this.密集_L + 1, this.密集_T + 1];
								else
									picture = this.PictureTable[this.単独_L + 1, this.単独_T + 0];
							}
							else
							{
								if (m右上)
									picture = this.PictureTable[this.単独_L + 0, this.単独_T + 1];
								else
									picture = this.単独セルPicture;
							}
						}
					}

					DDDraw.DrawCenter(this.GroundPicture, this.Point.X, this.Point.Y);
					DDDraw.DrawCenter(picture, this.Point.X, this.Point.Y);

					yield return true;
				}
			}

			private bool IsFriend2x2(int l, int t)
			{
				return
					this.IsFriend(l + 0, t + 0) &&
					this.IsFriend(l + 0, t + 1) &&
					this.IsFriend(l + 1, t + 0) &&
					this.IsFriend(l + 1, t + 1);
			}

			private bool IsFriend(int x, int y)
			{
				return this.F_IsFriend(Game.I.Map.GetCell(x, y).Tile);
			}
		}
	}
}