﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.Games.Tiles;

namespace Charlotte.Games.Enemies.神奈子s
{
	/// <summary>
	/// 神奈子
	/// 登場
	/// </summary>
	public class Enemy_神奈子 : Enemy
	{
		public Enemy_神奈子(double x, double y)
			: base(x, y, 0, 0, false)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			// ---- game_制御 ----

			if (Game.I.Status.神奈子を倒した)
				yield break;

			// ----

			Func<bool> f_閉鎖 = SCommon.Supplier(this.E_閉鎖());

			double targ_x = this.X;
			double targ_y = this.Y;

			this.X = Game.I.Map.W / 2.0;
			this.Y = -100.0;

			foreach (DDScene scene in DDSceneUtils.Create(120))
			{
				f_閉鎖();

				DDUtils.Approach(ref this.X, targ_x, 0.9);
				DDUtils.Approach(ref this.Y, targ_y, 0.9);

				bool facingLeft = Game.I.Player.X < this.X;

				DDDraw.DrawBegin(Ground.I.Picture2.Enemy_神奈子[0], this.X, this.Y);
				DDDraw.DrawZoom_X(facingLeft ? 1 : -1);
				DDDraw.DrawEnd();

				// 当たり判定無し

				yield return true;
			}
			Game.I.Enemies.Add(new Enemy_神奈子0001(this.X, this.Y));
		}

		private IEnumerable<bool> E_閉鎖()
		{
			Game.I.UserInputDisabled = true;

			for (int x = 0; x < Game.I.Map.W; x++)
			{
				this.閉鎖(x, 0);
				this.閉鎖(x, Game.I.Map.H - 1);

				yield return true;
			}
			for (int y = 0; y < Game.I.Map.H; y++)
			{
				this.閉鎖(0, y);
				this.閉鎖(Game.I.Map.W - 1, 0);

				yield return true;
			}
			Game.I.UserInputDisabled = false;
		}

		private void 閉鎖(int x, int y)
		{
			MapCell cell = Game.I.Map.GetCell(x, y);

			if (cell.Tile is Tile_None)
				cell.Tile = new Tile_ボス部屋Shutter();
		}
	}
}