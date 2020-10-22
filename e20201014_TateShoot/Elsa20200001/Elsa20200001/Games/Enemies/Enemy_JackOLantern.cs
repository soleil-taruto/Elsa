﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_JackOLantern : Enemy
	{
		public int ShotType;
		public int DropItemMode;
		public double XRate;
		public double YAdd;
		public double Rot;
		public double RotAdd;

		// <---- prm

		private Enemy_JackOLantern()
		{ }

		public static Enemy_JackOLantern Create(double x, double y, int hp, int transFrame, int shotType, int dropItemType, double xRate, double yAdd, double rot, double rotAdd)
		{
			return new Enemy_JackOLantern()
			{
				X = x,
				Y = y,
				Kind = Kind_e.ENEMY,
				HP = hp,
				TransFrame = transFrame,
				ShotType = shotType,
				DropItemMode = dropItemType,
				XRate = xRate,
				YAdd = yAdd,
				Rot = rot,
				RotAdd = rotAdd,
			};
		}

		protected override IEnumerable<bool> E_Draw()
		{
			double axisX = this.X;

			for (int frame = 0; ; frame++)
			{
				this.X = axisX + Math.Sin(this.Rot) * this.XRate;
				this.Y += this.YAdd;
				this.Rot += this.RotAdd;

				EnemyCommon.Shot(this, this.ShotType);

				int koma = frame / 7;
				koma %= 2;

				DDDraw.SetMosaic();
				DDDraw.DrawBegin(Ground.I.Picture2.D_PUMPKIN_00[koma], this.X, this.Y);
				DDDraw.DrawZoom(2.0);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				Game.I.AddEnemyCrash(DDCrashUtils.Circle(new D2Point(this.X - 1.0, this.Y + 3.0), 30.0), this);

				yield return !EnemyCommon.IsEvacuated(this);
			}
		}

		public override void Killed()
		{
			EnemyCommon.Killed(this, this.DropItemMode);
		}
	}
}
