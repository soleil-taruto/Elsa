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

		// this.X, this.Y はマップの座標(マップの左上を0,0とする)
		// -- 描画する際は DDGround.ICamera.X, DDGround.ICamera.Y をそれぞれ減じること。

		// this.X, this.Y の Game.I.ReloadEnemies() によってセットされる初期位置はマップセルの中央である。

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

		/// <summary>
		/// 自機に当たると消滅する。
		/// -- 敵弾を想定する。
		/// </summary>
		public bool 自機に当たると消滅する;

		public Enemy(double x, double y, int hp, int attackPoint, bool 自機に当たると消滅する)
		{
			this.X = x;
			this.Y = y;
			this.HP = hp;
			this.AttackPoint = attackPoint;
			this.自機に当たると消滅する = 自機に当たると消滅する;
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

		/// <summary>
		/// 現在のフレームにおける描画を行う。
		/// するべきこと：
		/// -- 行動
		/// -- 描画
		/// -- Crash を設定する。
		/// -- 必要に応じて DeadFlag を設定する。
		/// </summary>
		/// <returns>列挙：この敵は生存しているか</returns>
		public abstract void Draw();

		/// <summary>
		/// 被弾した。
		/// 体力の減少などは呼び出し側でやっている。
		/// </summary>
		/// <param name="shot">この敵が被弾したプレイヤーの弾</param>
		public virtual void Damaged(Shot shot)
		{
			// none
		}

		/// <summary>
		/// 撃破されて消滅した。
		/// 死亡フラグ立てなどは呼び出し側でやっている。
		/// </summary>
		public virtual void Killed()
		{
			DDGround.EL.Add(SCommon.Supplier(Effects.中爆発(this.X, this.Y)));
		}

		/// <summary>
		/// このインスタンスのコピーを生成する。
		/// </summary>
		/// <returns>このインスタンスのコピー</returns>
		public abstract Enemy GetClone();
	}
}
