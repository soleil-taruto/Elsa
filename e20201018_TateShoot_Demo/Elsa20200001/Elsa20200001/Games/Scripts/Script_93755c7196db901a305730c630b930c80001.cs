using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Walls;
using Charlotte.Games.Enemies.鍵山雛s;

namespace Charlotte.Games.Scripts
{
	public class Script_鍵山雛通しテスト0001 : Script
	{
		protected override IEnumerable<bool> E_EachFrame()
		{
			Ground.I.Music.MUS_STAGE_01.Play();

			Game.I.Walls.Add(new Wall_Dark());
			Game.I.Walls.Add(new Wall_3001());

			// ---- BOSS 登場

			{
				Enemy_鍵山雛 boss = new Enemy_鍵山雛();

				Game.I.Enemies.Add(boss);

				for (int c = 0; c < 90; c++)
					yield return true;

				foreach (bool v in ScriptCommon.掛け合い(new Scenario(@"e20200001_res\掛け合いシナリオ\小悪魔_鍵山雛.txt")))
					yield return v;

				boss.NextFlag = true;

				// boss はすぐに消滅することに注意
			}

			Ground.I.Music.MUS_BOSS_01.Play();

			while (1 <= Game.I.Enemies.Count)
				yield return true;

			// ---- BOSS 撃破

			for (int c = 0; c < 90; c++)
				yield return true;
		}
	}
}
