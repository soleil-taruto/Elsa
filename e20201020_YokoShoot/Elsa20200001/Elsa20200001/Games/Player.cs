using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Games.Shots;

namespace Charlotte.Games
{
	/// <summary>
	/// プレイヤーに関する情報と機能
	/// 唯一のインスタンスを Game.I.Player に保持する。
	/// </summary>
	public class Player
	{
		public const int SPEED_LEVEL_MIN = 1;
		public const int SPEED_LEVEL_DEF = 3;
		public const int SPEED_LEVEL_MAX = 5;

		public double X;
		public double Y;
		public double Born_X;
		public double Born_Y;

		public int SpeedLevel = SPEED_LEVEL_DEF;

		public int BornFrame = 0; // 0 == 無効, 0< == 登場中
		public int DeadFrame = 0; // 0 == 無効, 0< == 死亡中
		public int InvincibleFrame = 0; // 0 == 無効, 0< == 無敵期間中

		public void Draw()
		{
			if (1 <= this.BornFrame)
			{
				DDDraw.SetAlpha(0.5);
				DDDraw.DrawCenter(Ground.I.Picture.Player, this.Born_X, this.Born_Y);
				DDDraw.Reset();

				return;
			}
			if (1 <= this.DeadFrame)
			{
				// noop // 描画は Game.Perform で行う。

				return;
			}
			if (1 <= this.InvincibleFrame)
			{
				DDDraw.SetAlpha(0.5);
				DDDraw.DrawCenter(Ground.I.Picture.Player, this.X, this.Y);
				DDDraw.Reset();

				return;
			}
			DDDraw.DrawCenter(Ground.I.Picture.Player, this.X, this.Y);
		}

		public void Shoot()
		{
			if (Game.I.Frame % 6 == 0)
			{
				Game.I.Shots.Add(new Shot_B0001(
					this.X + 38.0,
					this.Y
					));
			}
		}
	}
}
