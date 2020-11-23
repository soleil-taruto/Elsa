using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Walls;
using Charlotte.Commons;

namespace Charlotte.Games.Scripts
{
	public class Script_ステージ0001 : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			return ScriptCommon.Wrapper(SCommon.Supplier(this.E_EachFrame2()));
		}

		private IEnumerable<int> E_EachFrame2()
		{
			DDRandom rand = new DDRandom(1);

			//Ground.I.Music.Stage_01.Play();

			Game.I.Walls.Add(new Wall_Dark());
			Game.I.Walls.Add(new Wall_B0001());
			yield return 100;

			Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50, DDConsts.Screen_H / 2));
			yield return 100;

			Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50, DDConsts.Screen_H / 2));
			yield return 300;

			// ---- ここからボス ----

			//Ground.I.Music.Boss_01.Play();

			Game.I.Enemies.Add(new Enemy_Bボス0001());

			for (; ; )
				yield return 1; // 以降何もしない。
		}
	}
}
