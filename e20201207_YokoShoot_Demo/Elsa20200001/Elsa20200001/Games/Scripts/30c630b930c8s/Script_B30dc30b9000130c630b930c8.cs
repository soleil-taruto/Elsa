using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Enemies;
using Charlotte.Games.Walls;

namespace Charlotte.Games.Scripts.テストs
{
	public class Script_Bボス0001テスト : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			Game.I.Walls.Add(new Wall_Dark());

			Game.I.Enemies.Add(new Enemy_Bボス0001());

			for (; ; )
			{
				yield return true;
			}
		}
	}
}
