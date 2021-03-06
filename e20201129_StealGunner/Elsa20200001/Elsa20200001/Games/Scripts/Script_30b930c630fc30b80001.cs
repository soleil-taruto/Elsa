﻿using System;
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
			DDRandom rand_Sub = new DDRandom(101);

			Ground.I.Music.Stage_01.Play();
			Game.I.Walls.Add(new Wall_B0003());
			yield return 100;

			foreach (DDScene scene in DDSceneUtils.Create((2 * 60 + 35) * 60))
			{
				DDGround.EL.Add(() =>
				{
					DDPrint.SetPrint(DDConsts.Screen_W - 180, 0);
					DDPrint.SetBorder(new I3Color(0, 0, 0));
					DDPrint.Print(scene.Numer + " / " + scene.Denom + " = " + scene.Rate.ToString("F3"));
					DDPrint.Reset();

					return false;
				});

				if (rand.Real2() < scene.Rate * 0.1)
				{
					if (rand.Real2() < 0.1)
					{
						if (rand_Sub.Real2() < 0.1)
						{
							Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50, rand.Real2() * DDConsts.Screen_H).AddKilled(enemy =>
							{
								Game.I.Enemies.Add(new Enemy_Item(enemy.X, enemy.Y, Enemy_Item.効用_e.ZANKI_UP));
							}
							));
						}
						else
						{
							Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50, rand.Real2() * DDConsts.Screen_H).AddKilled(enemy =>
							{
								Game.I.Enemies.Add(new Enemy_Item(enemy.X, enemy.Y, Enemy_Item.効用_e.POWER_UP_WEAPON));
							}
							));
						}
					}
					else if (rand.Real2() < 0.3)
					{
						Game.I.Enemies.Add(new Enemy_B0002(DDConsts.Screen_W + 50, rand.Real2() * DDConsts.Screen_H));
					}
					else
					{
						Game.I.Enemies.Add(new Enemy_B0001(DDConsts.Screen_W + 50, rand.Real2() * DDConsts.Screen_H));
					}
				}
				yield return 1;
			}

			Game.I.システム的な敵クリア();
			yield return 120;
			Ground.I.Music.Boss_01.Play();
			Game.I.Walls.Add(new Wall_B0004());
			yield return 120;

			{
				Enemy boss = new Enemy_Bボス0001();

				Game.I.Enemies.Add(boss);

				while (!boss.DeadFlag)
				{
					DDGround.EL.Add(() =>
					{
						DDPrint.SetPrint(DDConsts.Screen_W - 140, 0);
						DDPrint.SetBorder(new I3Color(0, 0, 0));
						DDPrint.Print("BOSS_HP = " + boss.HP);
						DDPrint.Reset();

						return false;
					});

					yield return 1;
				}
			}

			yield return 120;
			DDMusicUtils.Fade();
			yield return 120;

			Game.I.Script = new Script_ステージ0002(); // 次のステージ

			yield return 1; // Script を差し替えた場合、最後に 1 以上を返す。
		}
	}
}
