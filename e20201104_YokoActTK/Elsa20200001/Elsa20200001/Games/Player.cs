﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.Games.Enemies;
using Charlotte.Games.Shots;

namespace Charlotte.Games
{
	/// <summary>
	/// プレイヤーに関する情報と機能
	/// 唯一のインスタンスを Game.I.Player に保持する。
	/// </summary>
	public class Player
	{
		public enum Chara_e
		{
			HOMURA = 1,
			SAYAKA,
		}

		public Chara_e Chara;
		public double X;
		public double Y;
		public double YSpeed;
		public bool FacingLeft;
		public int MoveFrame;
		public bool MoveSlow; // ? 低速移動
		public int JumpFrame;
		public int JumpCount;
		public bool TouchGround;
		public int AirborneFrame;
		public int ShagamiFrame;
		public int AttackFrame;
		public int DeadFrame = 0; // 0 == 無効, 1～ == 死亡中
		public int DamageFrame = 0; // 0 == 無効, 1～ == ダメージ中
		public int InvincibleFrame = 0; // 0 == 無効, 1～ == 無敵期間中
		public int HP = 1;

		public void Draw()
		{
			double xZoom = this.FacingLeft ? -1 : 1;
			DDPicture picture = null;
			double xa = 0.0;
			double ya = 0.0;

			// 立ち >

			switch (Game.I.Status.Chara)
			{
				case Chara_e.HOMURA:
					{
						if (1 <= Game.I.Player.ShagamiFrame)
						{
							picture = Ground.I.Picture2.ほむら死亡[4];
							xa = 14;
							ya = 6;
						}
						else if (!Game.I.Player.TouchGround)
						{
							if (1 <= this.MoveFrame)
							{
								picture = Ground.I.Picture2.ほむら走り[5];
							}
							else
							{
								picture = Ground.I.Picture2.ほむら滞空攻撃[0];
								xa = 12;
							}
						}
						else if (1 <= this.MoveFrame)
						{
							picture = Ground.I.Picture2.ほむら走り[Game.I.Frame / 5 % Ground.I.Picture2.ほむら走り.Length];
						}
						else
						{
							picture = Ground.I.Picture2.ほむら立ち[Game.I.Frame / 10 % Ground.I.Picture2.ほむら立ち.Length];
						}
					}
					break;

				case Chara_e.SAYAKA:
					{
						// TODO
					}
					break;

				default:
					throw null; // never
			}

			// < 立ち

			// 攻撃中 >

			/*
			if (1 <= this.AttackFrame)
			{
				picture = Ground.I.Picture.PlayerAttack;

				if (1 <= this.MoveFrame)
				{
					if (this.MoveSlow)
					{
						picture = Ground.I.Picture.PlayerAttackWalk[(DDEngine.ProcFrame / 10) % 2];
					}
					else
					{
						picture = Ground.I.Picture.PlayerAttackDash[(DDEngine.ProcFrame / 5) % 2];
					}
				}
				if (!this.TouchGround)
				{
					picture = Ground.I.Picture.PlayerAttackJump;
				}
				if (1 <= this.ShagamiFrame)
				{
					picture = Ground.I.Picture.PlayerAttackShagami;
				}
			}

			// < 攻撃中

			if (1 <= this.DeadFrame)
			{
				int koma = SCommon.ToRange(this.DeadFrame / 20, 0, 1);

				if (this.TouchGround)
					koma *= 2;

				koma *= 2;
				koma++;

				picture = Ground.I.Picture.PlayerDamage[koma];

				DDDraw.SetTaskList(DDGround.EL);
			}
			if (1 <= this.DamageFrame)
			{
				picture = Ground.I.Picture.PlayerDamage[0];
				xZoom *= -1;
			}

			if (1 <= this.DamageFrame || 1 <= this.InvincibleFrame)
			{
				DDDraw.SetTaskList(DDGround.EL);
				DDDraw.SetAlpha(0.5);
			}
			DDDraw.DrawBegin(
				picture,
				SCommon.ToInt(this.X - DDGround.ICamera.X),
				SCommon.ToInt(this.Y - DDGround.ICamera.Y) - 16
				);
			DDDraw.DrawZoom_X(xZoom);
			DDDraw.DrawEnd();
			DDDraw.Reset();
			 * */

			DDDraw.DrawBegin(
				picture,
				this.X - DDGround.ICamera.X + (xa * xZoom),
				this.Y - DDGround.ICamera.Y + ya
				);
			DDDraw.DrawZoom_X(xZoom);
			DDDraw.DrawEnd();

			// debug 当たり判定表示
			{
				DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawZoom(0.1);
				DDDraw.DrawRotate(DDEngine.ProcFrame * 0.01);
				DDDraw.DrawEnd();
			}
		}

		public void Attack()
		{
			// 将来的に武器毎にコードが実装され、メソッドがでかくなると思われる。

			if (this.AttackFrame % 6 == 1)
			{
				double x = this.X;
				double y = this.Y;

				x += 30.0 * (this.FacingLeft ? -1 : 1);

				if (1 <= this.ShagamiFrame)
					y += 10.0;
				else
					y -= 4.0;

				Game.I.Shots.Add(new Shot_B0001(x, y, this.FacingLeft));
			}
		}
	}
}
