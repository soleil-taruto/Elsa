using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games
{
	public class Map
	{
		private string MapFile;
		private MapCell[,] Table; // 添字：[x,y]
		private int W;
		private int H;

		public Map(string mapFile)
		{
			this.MapFile = mapFile;
			this.Load();
		}

		private void Load()
		{
			string[] lines = SCommon.TextToLines(Encoding.UTF8.GetString(DDResource.Load(this.MapFile)));
			int c = 0;

			int w = int.Parse(lines[c++]);
			int h = int.Parse(lines[c++]);

			if (w < 1 || SCommon.IMAX < w) throw new DDError();
			if (h < 1 || SCommon.IMAX < h) throw new DDError();

			this.Table = new MapCell[w, h];

			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					this.Table[x, y] = new MapCell();

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					if (lines.Length <= c)
						goto endLoad;

					string[] tokens = SCommon.Tokenize(lines[c++], "\t");
					int d = 0;

					MapCell cell = this.Table[x, y];

					cell.Wall = Common.GetElement(tokens, d++, v => int.Parse(v), 0) != 0;
					cell.Tile = Common.GetElement(tokens, d++, v => DDCCResource.GetPicture(MapUtils.GetMapTileFile(v)), MapUtils.MapTileCollection.I.None);
					cell.EnemyName = Common.GetElement(tokens, d++, v => v, Consts.ENEMY_NONE);

					this.Table[x, y] = cell;
				}
			}
			while (c < lines.Length)
			{
				// セーブ時にプロパティ部分も上書きされることに注意すること。
				// -- 読み込まれなかった行、空行は除去される。

				var tokens = lines[c++].Split("=".ToArray(), 2);
				int d = 0;

				string name = tokens[d++].Trim();
				string value = tokens[d++].Trim();

				if (name == "") throw new DDError();
				if (value == "") throw new DDError();

				// TODO
			}
		endLoad:
			this.W = w;
			this.H = h;
		}

		public void Save()
		{
			// TODO
		}

		// TODO
	}
}
