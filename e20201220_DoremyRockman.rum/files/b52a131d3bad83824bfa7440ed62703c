using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Tiles
{
	public class Tile_Ladder : Tile
	{
		public override Tile.Kind_e GetKind()
		{
			return Kind_e.LADDER;
		}

		public override void Draw(double x, double y)
		{
			DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, x, y);
			DDDraw.DrawSetSize(32, 32);
			DDDraw.DrawEnd();

			DDPrint.SetPrint((int)x, (int)y);
			DDPrint.SetBorder(new I3Color(0, 0, 0));
			DDPrint.Print("梯子");
			DDPrint.Reset();
		}
	}
}
