﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Tama_01 : Enemy
	{
		public EnemyCommon.TAMA_KIND_e TamaKind;
		public EnemyCommon.TAMA_COLOR_e TamaColor;
		public double Speed;
		public double Angle;

		// <---- prm

		private Enemy_Tama_01()
		{ }

		public static Enemy_Tama_01 Create(double x, double y, EnemyCommon.TAMA_KIND_e tamaKind, EnemyCommon.TAMA_COLOR_e tamaColor, double speed, double angle)
		{
			// x
			// y
			// tamaKind
			// tamaColor
			if (speed < 0.1 || 100.0 < speed) throw new DDError();
			if (angle < -3.0 * Math.PI || 3.0 * Math.PI < angle) throw new DDError();

			return new Enemy_Tama_01()
			{
				X = x,
				Y = y,
				Kind = Kind_e.TAMA,
				HP = 0,
				TransFrame = 0,
				TamaKind = tamaKind,
				TamaColor = tamaColor,
				Speed = speed,
				Angle = angle,
			};
		}

		protected override IEnumerable<bool> E_Draw()
		{
			double r;

			switch (this.TamaKind)
			{
				case EnemyCommon.TAMA_KIND_e.NORMAL: r = 8.0; break;
				case EnemyCommon.TAMA_KIND_e.BIG: r = 16.0; break;

				// TODO: その他の敵弾についてもここへ追加..

				default:
					throw null; // never
			}
			double xAdd;
			double yAdd;

			DDUtils.MakeXYSpeed(this.X, this.Y, Game.I.Player.X, Game.I.Player.Y, this.Speed, out xAdd, out yAdd);
			DDUtils.Rotate(ref xAdd, ref yAdd, this.Angle);

			DDPicture picture = GetTamaPicture(this.TamaKind, this.TamaColor);

			for (; ; )
			{
				this.X += xAdd;
				this.Y += yAdd;

				DDDraw.DrawCenter(picture, this.X, this.Y);

				Game.I.AddEnemyCrash(DDCrashUtils.Circle(new D2Point(this.X, this.Y), r), this);

				yield return !EnemyCommon.IsEvacuated(this);
			}
		}

		private static DDPicture GetTamaPicture(EnemyCommon.TAMA_KIND_e kind, EnemyCommon.TAMA_COLOR_e color)
		{
			return Ground.I.Picture2.D_TAMA_00[(int)kind][(int)color];
		}

		public override void Killed()
		{
			EnemyCommon.Killed(this, 0);
		}
	}
}
