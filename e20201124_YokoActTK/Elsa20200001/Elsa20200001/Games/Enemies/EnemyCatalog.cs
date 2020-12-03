﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies
{
	/// <summary>
	/// 敵のカタログ
	/// </summary>
	public static class EnemyCatalog
	{
		private class EnemyInfo
		{
			public string Name;
			public Func<Enemy> Creator;

			public EnemyInfo(string name, Func<Enemy> creator)
			{
				this.Name = name;
				this.Creator = creator;
			}
		}

		// Creator 用
		// -- 初期値は適当な値
		private static double X = 300.0;
		private static double Y = 300.0;

		private static EnemyInfo[] Tiles = new EnemyInfo[]
		{
			new EnemyInfo(Consts.ENEMY_NONE, () => { throw new DDError("敵「無」を生成しようとしました。"); }),
			new EnemyInfo("スタート地点", () => new Enemy_スタート地点(X, Y, 5)),
			new EnemyInfo("上から入場", () => new Enemy_スタート地点(X, Y, 8)),
			new EnemyInfo("下から入場", () => new Enemy_スタート地点(X, Y, 2)),
			new EnemyInfo("左から入場", () => new Enemy_スタート地点(X, Y, 4)),
			new EnemyInfo("右から入場", () => new Enemy_スタート地点(X, Y, 6)),
			new EnemyInfo("敵01", () => new Enemy_B0001(X, Y)),
			new EnemyInfo("敵02", () => new Enemy_B0002(X, Y)),
			new EnemyInfo("敵03", () => new Enemy_B0003(X, Y)),

			// 新しい敵をここへ追加..
		};

		public static string[] GetNames()
		{
			return Tiles.Select(enemy => enemy.Name).ToArray();
		}

		public static Enemy Create(string name, double x, double y)
		{
			X = x;
			Y = y;

			return SCommon.FirstOrDie(Tiles, enemy => enemy.Name == name, () => new DDError(name)).Creator();
		}
	}
}
