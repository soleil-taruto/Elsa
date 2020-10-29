﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Enemies
{
	/// <summary>
	/// 敵
	/// </summary>
	public abstract class Enemy
	{
		// Game.I.ReloadEnemies() からロードされた場合、初期位置として「配置されたマップセルの中心座標」が与えられる。

		public double X;
		public double Y;

		/// <summary>
		/// 体力
		/// 0 == 無敵
		/// -1 == 死亡
		/// </summary>
		public int HP;

		/// <summary>
		/// 攻撃力
		/// 1～
		/// </summary>
		public int AttackPoint;

		public Enemy(double x, double y, int hp, int attackPoint)
		{
			this.X = x;
			this.Y = y;
			this.HP = hp;
			this.AttackPoint = attackPoint;
		}

		/// <summary>
		/// この敵を消滅させるか
		/// 撃破された場合などこの敵を消滅させたい場合 true をセットすること。
		/// これにより「フレームの最後に」敵リストから除去される。
		/// </summary>
		public bool DeadFlag
		{
			set
			{
				if (value)
					this.HP = -1;
				else
					throw null; // never
			}

			get
			{
				return this.HP == -1;
			}
		}

		/// <summary>
		/// 現在のフレームにおける当たり判定を保持する。
		/// -- E_Draw によって設定される。
		/// </summary>
		public DDCrash Crash = DDCrashUtils.None();

		private Func<bool> _draw = null;

		public void Draw()
		{
			if (_draw == null)
				_draw = SCommon.Supplier(this.E_Draw());

			if (!_draw())
				this.DeadFlag = true;
		}

		/// <summary>
		/// 現在のフレームにおける描画を行う。
		/// するべきこと：
		/// -- 行動
		/// -- 描画
		/// -- Crash を設定する。
		/// -- 必要に応じて DeadFlag を設定する。
		/// </summary>
		/// <returns>列挙：この敵は生存しているか</returns>
		public abstract IEnumerable<bool> E_Draw();

		/// <summary>
		/// 被弾した。
		/// </summary>
		/// <param name="shot">この敵が被弾したプレイヤーの弾</param>
		public virtual void Damaged(Shot shot)
		{
			// none
		}

		/// <summary>
		/// 撃破されて消滅した。
		/// </summary>
		public virtual void Killed()
		{
			DDGround.EL.Add(SCommon.Supplier(Effects.中爆発(this.X, this.Y)));
		}
	}
}
