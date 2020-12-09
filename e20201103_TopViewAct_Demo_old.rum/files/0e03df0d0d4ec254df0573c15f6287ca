using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.Games;
using Charlotte.Games.Tiles;

namespace Charlotte
{
	public static class Common
	{
		// ==== Map 関連 ====

		public const string MAP_FILE_PREFIX = @"e20200001_res\World\Map\";
		public const string MAP_FILE_SUFFIX = ".txt";

		/// <summary>
		/// マップ名からマップファイル名を得る。
		/// </summary>
		/// <param name="mapName">マップ名</param>
		/// <returns>マップファイル名</returns>
		public static string GetMapFile(string mapName)
		{
			return MAP_FILE_PREFIX + mapName + MAP_FILE_SUFFIX;
		}

		/// <summary>
		/// マップファイル名からマップ名を得る。
		/// </summary>
		/// <param name="mapFile">マップファイル名</param>
		/// <returns>マップ名</returns>
		public static string GetMapName(string mapFile, string defval)
		{
			if (!SCommon.StartsWithIgnoreCase(mapFile, MAP_FILE_PREFIX))
				return defval;

			mapFile = mapFile.Substring(MAP_FILE_PREFIX.Length);

			if (!SCommon.EndsWithIgnoreCase(mapFile, MAP_FILE_SUFFIX))
				return defval;

			mapFile = mapFile.Substring(0, mapFile.Length - MAP_FILE_SUFFIX.Length);

			if (mapFile == "")
				return defval;

			return mapFile;
		}

		/// <summary>
		/// マップの(ドット単位の)座標からマップセルの座標を得る。
		/// </summary>
		/// <param name="pt">マップの(ドット単位の)座標</param>
		/// <returns>マップセルの座標</returns>
		public static I2Point ToTablePoint(D2Point pt)
		{
			return ToTablePoint(pt.X, pt.Y);
		}

		/// <summary>
		/// マップの(ドット単位の)座標からマップセルの座標を得る。
		/// </summary>
		/// <param name="x">マップの(ドット単位の)X_座標</param>
		/// <param name="y">マップの(ドット単位の)Y_座標</param>
		/// <returns>マップセルの座標</returns>
		public static I2Point ToTablePoint(double x, double y)
		{
			return new I2Point(
				(int)(x / Consts.TILE_W),
				(int)(y / Consts.TILE_H)
				);
		}

		private static MapCell _defaultMapCell = null;

		/// <summary>
		/// デフォルトのマップセル
		/// マップ外を埋め尽くすマップセル
		/// </summary>
		public static MapCell DefaultMapCell
		{
			get
			{
				if (_defaultMapCell == null)
				{
					_defaultMapCell = new MapCell()
					{
						TileName = Consts.TILE_NONE,
						Tile = new Tile_None(),
						EnemyName = Consts.ENEMY_NONE,
					};
				}
				return _defaultMapCell;
			}
		}

		// ====

		public static T GetElement<T>(T[] arr, int index, T defval)
		{
			if (index < arr.Length)
			{
				return arr[index];
			}
			else
			{
				return defval;
			}
		}

		/// <summary>
		/// 方向転換する。
		/// 方向：{ 1, 2, 3, 4, 6, 7, 8, 9 } == { 左下, 下, 右下, 左, 右, 左上, 上, 右上 }
		/// </summary>
		/// <param name="direction">回転前の方向</param>
		/// <param name="count">回転する回数(1回につき時計回りに45度転換する,負の値ok)</param>
		/// <returns>回転後の方向</returns>
		public static int Rotate(int direction, int count)
		{
			if (count <= -8 || 8 <= count)
				count %= 8;

			int[] ROT_CLW = new int[] { -1, 4, 1, 2, 7, -1, 3, 8, 9, 6 }; // 時計回り
			int[] ROT_CCW = new int[] { -1, 2, 3, 6, 1, -1, 9, 4, 7, 8 }; // 反時計回り

			for (; 0 < count; count--)
				direction = ROT_CLW[direction];

			for (; count < 0; count++)
				direction = ROT_CCW[direction];

			if (direction == -1)
				throw null; // never

			return direction;
		}

		public static D2Point GetSpeed(int direction, double speed)
		{
			double nanameSpeed = speed / Consts.ROOT_OF_2;

			switch (direction)
			{
				case 4: return new D2Point(-speed, 0.0);
				case 6: return new D2Point(speed, 0.0);
				case 8: return new D2Point(0.0, -speed);
				case 2: return new D2Point(0.0, speed);

				case 1: return new D2Point(-nanameSpeed, nanameSpeed);
				case 3: return new D2Point(nanameSpeed, nanameSpeed);
				case 7: return new D2Point(-nanameSpeed, -nanameSpeed);
				case 9: return new D2Point(nanameSpeed, -nanameSpeed);

				default:
					throw null; // never
			}
		}
	}
}
