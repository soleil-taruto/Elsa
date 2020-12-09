using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Tiles
{
	public abstract class Tile
	{
		/// <summary>
		/// このタイルは壁か
		/// </summary>
		/// <returns>このタイルは壁か</returns>
		public abstract bool IsWall();

		/// <summary>
		/// このタイルを描画する。
		/// タイルが画面内に入り込んだときのみ実行される。
		/// 座標はタイルの中心座標
		/// -- マップセルを埋めるには LTWH (x - Consts.TILE_W / 2, y - Consts.TILE_H / 2, Consts.TILE_W, Consts.TILE_H) の領域に描画すれば良い。
		/// </summary>
		/// <param name="x">タイルの中心座標(X軸)</param>
		/// <param name="y">タイルの中心座標(Y軸)</param>
		public abstract void Draw(double x, double y);
	}
}
