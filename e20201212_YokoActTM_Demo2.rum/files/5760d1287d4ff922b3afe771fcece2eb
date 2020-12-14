using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Tiles
{
	public class Tile_ボス部屋Shutter : Tile
	{
		public override bool IsWall()
		{
			return true;
		}

		public override void Draw(double x, double y)
		{
			DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, x, y);
			DDDraw.DrawSetSize(GameConsts.TILE_W, GameConsts.TILE_H);
			DDDraw.DrawEnd();

			DDPrint.SetBorder(new I3Color(0, 0, 0));
			DDPrint.SetPrint((int)x, (int)y);
			DDPrint.Print("扉");
			DDPrint.Reset();
		}
	}
}
