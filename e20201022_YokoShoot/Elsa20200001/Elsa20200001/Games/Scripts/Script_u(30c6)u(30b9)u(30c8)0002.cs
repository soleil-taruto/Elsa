using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Walls;

namespace Charlotte.Games.Scripts
{
	public class Script_テスト0002 : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			Game.I.Walls.Add(new Wall_Dark());

			for (; ; )
			{
				Game.I.Walls.Add(new Wall_B0003());

				Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

				for (int c = 0; c < 500; c++)
					yield return true;

				Game.I.Walls.Add(new Wall_B0004());

				Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

				for (int c = 0; c < 500; c++)
					yield return true;
			}
		}
	}
}
