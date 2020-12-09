using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Tiles
{
	public class Tile_B0002 : Tile
	{
		public override Tile.Kind_e GetKind()
		{
			return Kind_e.WALL;
		}

		public override void Draw(double x, double y)
		{
			DDDraw.DrawCenter(Ground.I.Picture.Tile_B0002, x, y);
		}
	}
}
