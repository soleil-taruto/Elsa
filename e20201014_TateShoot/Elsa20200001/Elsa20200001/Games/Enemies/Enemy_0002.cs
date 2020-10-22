using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_0002 : Enemy
	{
		public EnemyCommon.FairyInfo Fairy;
		public int ShotType;
		public int DropItemMode;
		public double TargetX;
		public double TargetY;
		public double XAdd;
		public double YAdd;
		public double ApproachingRate;

		// <---- prm

		private Enemy_0002()
		{ }

		public static Enemy_0002 Create(double x, double y, int hp, int transFrame, int fairyKind, int shotType, int dropItemType, double targetX, double targetY, double xAdd, double yAdd, double approachingRate)
		{
			Enemy_0002 enemy = new Enemy_0002()
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
				TargetX = targetX,
				TargetY = targetY,
				XAdd = xAdd,
				YAdd = yAdd,
				ApproachingRate = approachingRate,
			};

			enemy.Fairy.Enemy = enemy;

			return enemy;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				this.TargetX += this.XAdd;
				this.TargetY += this.YAdd;

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
