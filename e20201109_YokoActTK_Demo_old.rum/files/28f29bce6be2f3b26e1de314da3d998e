using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Walls
{
	public static class WallCreator
	{
		public static Wall Create(string name)
		{
			Wall wall;

			switch (name)
			{
				case Consts.WALL_DEFAULT: wall = new Wall_Dark(); break;
				case "Dark": wall = new Wall_Dark(); break;
				case "R0001": wall = new Wall_Simple(Ground.I.Picture.Wall_R0001); break;
				case "R0002": wall = new Wall_Simple(Ground.I.Picture.Wall_R0002); break;

				// 新しい壁紙をここへ追加..

				default:
					throw new DDError("name: " + name);
			}
			return wall;
		}
	}
}
