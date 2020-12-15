using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies;

namespace Charlotte.Games
{
	/// <summary>
	/// プレイヤーに関する情報と機能
	/// 唯一のインスタンスを Game.I.Player に保持する。
	/// </summary>
	public class Player
	{
		public double X;
		public double Y;
		public double XSpeed;
		public double YSpeed;
		public int MoveFrame;
		public bool MoveSlow; // ? 低速移動
		public int JumpFrame;
		public int AirborneFrame; // 0 == 接地状態, 1～ == 滞空状態
		public int DeadFrame = 0; // 0 == 無効, 1～ == 死亡中
		public int RebornFrame = 0; // 0 == 無効, 1～ == 登場中

		public void Draw()
		{
			DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, SCommon.ToInt(this.X - DDGround.ICamera.X), SCommon.ToInt(this.Y - DDGround.ICamera.Y));
			DDDraw.DrawSetSize(Consts.TILE_W, Consts.TILE_H);
			DDDraw.DrawEnd();
		}

		public void Attack()
		{
			// noop
		}
	}
}
