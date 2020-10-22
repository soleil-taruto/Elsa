using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Item : Enemy
	{
		private EnemyCommon.DROP_ITEM_TYPE_e DropItemType;

		public Enemy_Item(double x, double y, EnemyCommon.DROP_ITEM_TYPE_e dropItemType)
			: base(x, y, Kind_e.ITEM, 0, 0)
		{
			this.DropItemType = dropItemType;
		}

		private double YAdd = -4.0;
		private double Rot = DDUtils.Random.Real2() * Math.PI * 2.0;
		private double RotAdd = DDUtils.Random.Real2() * 0.01;
		private int VacuumMode = 0; // 0-2 == { NONE, SLOW, FAST }

		protected override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				if (this.VacuumMode == 0)
				{
					if (Game.I.Input.Slow && DDUtils.GetDistance(new D2Point(Game.I.Player.X, Game.I.Player.Y), new D2Point(this.X, this.Y)) < 100.0) // ? 低速移動中に接近した。
					{
						this.VacuumMode = 1;
					}
					else if (Game.I.Player.Y < Consts.ITEM_GET_BORDER_Y && this.Y < Consts.ITEM_GET_MAX_Y)
					{
						this.VacuumMode = 2;
					}
				}
				if (this.VacuumMode == 0)
				{
					this.Y += this.YAdd;
					this.YAdd += 0.1;

					if (this.X < 0.0)
						this.X += 1.0;
					else if (Consts.FIELD_W < this.X)
						this.X -= 1.0;
				}
				else
				{
					double speed;

					if (this.VacuumMode == 2)
						speed = 12.0;
					else
						speed = 6.0;

					double targetX;
					double targetY;

					if (1 <= Game.I.Player.BornFrame)
					{
						targetX = Game.I.Player.BornFollowX;
						targetY = Game.I.Player.BornFollowY;
					}
					else
					{
						targetX = Game.I.Player.X;
						targetY = Game.I.Player.Y;
					}
					double xa;
					double ya;

					DDUtils.MakeXYSpeed(this.X, this.Y, targetX, targetY, speed, out xa, out ya);

					this.X += xa;
					this.Y += ya;
				}
				this.Rot += this.RotAdd;

				if (0.0 < this.YAdd)
					this.YAdd *= 0.97;

				DDPicture picture;

				switch (this.DropItemType)
				{
					case EnemyCommon.DROP_ITEM_TYPE_e.STAR: picture = Ground.I.Picture2.D_ITEM_STAR; break;
					case EnemyCommon.DROP_ITEM_TYPE_e.HEART: picture = Ground.I.Picture2.D_ITEM_HEART; break;
					case EnemyCommon.DROP_ITEM_TYPE_e.CANDY: picture = Ground.I.Picture2.D_ITEM_CANDY; break;
					case EnemyCommon.DROP_ITEM_TYPE_e.BOMB: picture = Ground.I.Picture2.D_ITEM_BOMB; break;

					default:
						throw null; // never
				}

				DDDraw.DrawBegin(picture, this.X, this.Y);
				DDDraw.DrawRotate(this.Rot);
				DDDraw.DrawEnd();

				Game.I.AddEnemyCrash(DDCrashUtils.Circle(new D2Point(this.X, this.Y), 16.0), this);

				yield return this.Y < Consts.ITEM_GET_MAX_Y;
			}
		}

		public override void Killed()
		{
			Ground.I.SE.SE_ITEMGOT.Play();

			switch (this.DropItemType)
			{
				case EnemyCommon.DROP_ITEM_TYPE_e.STAR:
					Game.I.Score += 100;
					break;

				case EnemyCommon.DROP_ITEM_TYPE_e.HEART:
					Game.I.Zanki++;
					break;

				case EnemyCommon.DROP_ITEM_TYPE_e.CANDY:
					Game.I.Player.Power += 17;
					//Game.I.Player.Power += 7;
					//Game.I.Player.Power++;
					Game.I.Player.Power = Math.Min(Game.I.Player.Power, Consts.PLAYER_POWER_MAX);
					break;

				case EnemyCommon.DROP_ITEM_TYPE_e.BOMB:
					Game.I.ZanBomb++;
					break;

				default:
					throw null; // never
			}
		}
	}
}
