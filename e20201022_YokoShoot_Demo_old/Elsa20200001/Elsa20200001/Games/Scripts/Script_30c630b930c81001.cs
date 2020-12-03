using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Walls;

namespace Charlotte.Games.Scripts
{
	public class Script_テスト1001 : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			Ground.I.Music.Stage_01.Play();

			Game.I.Walls.Add(new Wall_Dark());
			Game.I.Walls.Add(new Wall_B0003());
			Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Game.I.Walls.Add(new Wall_B0004());
			Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Ground.I.Music.Boss_01.Play();

			// 1ボス戦
			{
				Enemy boss = new Enemy_Bボス0001();

				Game.I.Enemies.Add(boss);

				while (!boss.DeadFlag)
					yield return true;
			}

			Ground.I.Music.Stage_02.Play();

			Game.I.Walls.Add(new Wall_B0001());
			Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Game.I.Walls.Add(new Wall_B0002());
			Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Ground.I.Music.Boss_02.Play();

			// 2ボス戦
			{
				Enemy boss = new Enemy_Bボス0002();

				Game.I.Enemies.Add(boss);

				while (!boss.DeadFlag)
					yield return true;
			}

			Ground.I.Music.Stage_03.Play();

			Game.I.Walls.Add(new Wall_B0004());
			Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Game.I.Walls.Add(new Wall_B0003());
			Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50.0, DDConsts.Screen_H / 2.0));

			for (int c = 0; c < 500; c++)
				yield return true;

			Ground.I.Music.Boss_03.Play();

			// 3ボス戦
			{
				Enemy boss = new Enemy_Bボス0003();

				Game.I.Enemies.Add(boss);

				while (!boss.DeadFlag)
					yield return true;
			}

			for (int c = 0; c < 100; c++)
				yield return true;
		}
	}
}
