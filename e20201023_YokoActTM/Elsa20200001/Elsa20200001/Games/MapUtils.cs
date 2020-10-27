using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games
{
	public static class MapUtils
	{
		// World.csv の各セルにはには「マップ名」を記述する。

		// DDResource.Load/Save によってロード・セーブするには「マップファイル名」を使用する。

		public const string MAP_FILE_PREFIX = @"e20200001_res\World\Map\";
		public const string MAP_FILE_SUFFIX = ".txt";

		/// <summary>
		/// マップ名からマップファイル名を返す。
		/// </summary>
		/// <param name="mapName">マップ名</param>
		/// <returns>マップファイル名</returns>
		public static string GetMapFile(string mapName)
		{
			return MAP_FILE_PREFIX + mapName + MAP_FILE_SUFFIX;
		}

		// マップは「マップセル」によって埋め尽くされている。
		// マップセルに表示する画像を「マップタイル」と呼ぶ。

		public const string MAP_TILE_FILE_PREFIX = @"e20201023_YokoActTM\MapTile\";
		public const string MAP_TILE_FILE_SUFFIX = ".png";

		/// <summary>
		/// マップタイル名からマップタイルの画像ファイル名を返す。
		/// </summary>
		/// <param name="mapTileName">マップタイル名</param>
		/// <returns>マップタイルの画像ファイル名</returns>
		public static string GetMapTileFile(string mapTileName)
		{
			return MAP_TILE_FILE_PREFIX + mapTileName + MAP_TILE_FILE_SUFFIX;
		}

		public const int MAP_TILE_W = 32;
		public const int MAP_TILE_H = 32;

		/// <summary>
		/// 透明なマップタイル名
		/// </summary>
		public const string MAP_TILE_NONE = "None";

		/// <summary>
		/// マップタイル・コレクション
		/// </summary>
		public class MapTileCollection
		{
			private static MapTileCollection _i = null;

			public static MapTileCollection I
			{
				get
				{
					if (_i == null)
						_i = new MapTileCollection();

					return _i;
				}
			}

			/// <summary>
			/// マップタイル名のリスト
			/// </summary>
			public string[] Names;

			/// <summary>
			/// 透明なマップタイル
			/// 頻繁に使うかもしれないので、ここにロードしておく
			/// </summary>
			public DDPicture None;

			private MapTileCollection()
			{
				List<string> names = new List<string>();

				foreach (string file in DDResource.GetFiles())
				{
					string name = file;

					if (!name.StartsWith(MAP_TILE_FILE_PREFIX))
						continue;

					name = name.Substring(MAP_TILE_FILE_PREFIX.Length);

					if (!name.EndsWith(MAP_TILE_FILE_SUFFIX))
						continue;

					name = name.Substring(0, name.Length - MAP_TILE_FILE_SUFFIX.Length);

					if (name == "")
						continue;

					names.Add(name);
				}
				this.Names = names.ToArray();
				this.None = DDCCResource.GetPicture(GetMapTileFile(MAP_TILE_NONE)); // マップタイル画像(None)のロード // マップタイル画像のロードはこのようにやること。
			}
		}
	}
}
