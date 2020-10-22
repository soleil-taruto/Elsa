using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_0001 : Enemy
	{
		public EnemyCommon.FairyInfo Fairy;
		public int ShotType;
		public int DropItemMode;
		public double TargetX;
		public double TargetY;
		public double Speed;
		public int XDir;
		public double MaxY;
		public double ApproachingRate;

		// <---- prm

		private Enemy_0001()
		{ }

		public static Enemy_0001 Create(double x, double y, int hp, int transFrame, int fairyKind, int shotType, int dropItemType, double speed, int xDir, double maxY, double approachingRate)
		{
			Enemy_0001 enemy = new Enemy_0001()
			{
				X = x,
				Y = y,
				Kind = Kind_e.ENEMY,
				HP = hp,
				TransFrame = transFrame,
				Fairy = new EnemyCommon.FairyInfo()
				{
					//Enemy = enemy, // 後で、
					Kind = fairyKind,
				},
				ShotType = shotType,
				DropItemMode = dropItemType,
				TargetX = x,
				TargetY = y,
				Speed = speed,
				XDir = xDir,
				MaxY = maxY,
				ApproachingRate = approachingRate,
			};

			enemy.Fairy.Enemy = enemy;

			return enemy;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				if (this.TargetY < this.MaxY)
				{
					this.TargetY += this.Speed;
				}
				else
				{
					this.TargetX += this.Speed * this.XDir;
				}

				DDUtils.Approach(ref this.X, this.TargetX, this.ApproachingRate);
				DDUtils.Approach(ref this.Y, this.TargetY, this.ApproachingRate);

				EnemyCommon.Shot(this, this.ShotType);

				yield return EnemyCommon.FairyDraw(this.Fairy);
			}
		}

		public override void Killed()
		{
			EnemyCommon.Killed(this, this.DropItemMode);
		}
	}
}
