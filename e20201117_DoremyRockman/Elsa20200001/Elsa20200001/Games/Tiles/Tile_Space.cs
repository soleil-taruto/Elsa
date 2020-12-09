using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Tiles
{
	public class Tile_Space : Tile
	{
		private DDPicture Picture;

		public Tile_Space(DDPicture picture)
		{
			this.Picture = picture;
		}

		public override Kind_e GetKind()
		{
			return Kind_e.SPACE;
		}

		public override void Draw(double x, double y)
		{
			DDDraw.DrawCenter(this.Picture, x, y);
		}
	}
}
