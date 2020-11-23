﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Walls;

namespace Charlotte.Games.Scripts.テストs
{
	public class Script_壁紙テスト0001 : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			Game.I.Walls.Add(new Wall_Dark());

			for (; ; )
			{
				Game.I.Walls.Add(new Wall_B0001());

				for (int c = 0; c < 300; c++)
					yield return true;

				Game.I.Walls.Add(new Wall_B0002());

				for (int c = 0; c < 300; c++)
					yield return true;

				Game.I.Walls.Add(new Wall_B0003());

				for (int c = 0; c < 300; c++)
					yield return true;

				Game.I.Walls.Add(new Wall_B0004());

				for (int c = 0; c < 300; c++)
					yield return true;
			}
		}
	}
}
