using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Tiles
{
	public static class TileCatalog
	{
		private class TileInfo
		{
			public string Name;
			public Func<Tile> Creator;

			public TileInfo(string name, Func<Tile> creator)
			{
				this.Name = name;
				this.Creator = creator;
			}
		}

		private static TileInfo[] Tiles = new TileInfo[]
		{
			new TileInfo(Consts.TILE_NONE, () => new Tile_None()),
			new TileInfo("ブロック01", () => new Tile_B0001()),
			new TileInfo("ブロック02", () => new Tile_B0002()),
			new TileInfo("ブロック03", () => new Tile_B0003()),
			new TileInfo("ブロック04", () => new Tile_B0004()),

			// 新しいタイルをここへ追加..
		};

		public static string[] GetNames()
		{
			return Tiles.Select(tile => tile.Name).ToArray();
		}

		public static Tile Create(string name)
		{
			return Tiles.First(tile => tile.Name == name).Creator();
		}
	}
}
