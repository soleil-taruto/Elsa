using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	/// <summary>
	/// 敵
	/// </summary>
	public abstract class Enemy
	{
		public double X;
		public double Y;

		public enum Kind_e
		{
			ENEMY = 1, // 敵
			TAMA, // 敵弾(敵の弾)
			ITEM, // アイテム
		}

		/// <summary>
		/// 敵の種類
		/// </summary>
		public Kind_e Kind;

		/// <summary>
		/// 敵の体力
		/// 0 == 無敵 | 破壊できない敵弾 | アイテム
		/// -1 == 死亡
		/// </summary>
		public int HP;

		/// <summary>
		/// 無敵時間
		/// 画面に登場してから毎フレーム 0になるまで カウントダウンする。
		/// 0 == 無効
		/// 0&lt; == 無敵
		/// </summary>
		public int TransFrame;

		public Enemy(double x, double y, Kind_e kind, int hp, int transFrame)
		{
			this.X = x;
			this.Y = y;
			this.Kind = kind;
			this.HP = hp;
			this.TransFrame = transFrame;
		}

		/// <summary>
		/// 画面(フィールド)に登場してから毎フレーム インクリメントする。
		/// </summary>
		public int OnFieldFrame = 0;

		private Func<bool> _draw = null;

		public Func<bool> Draw
		{
			get
			{
				if (_draw == null)
					_draw = SCommon.Supplier(this.E_Draw());

				return _draw;
			}
		}

		/// <summary>
		/// 真を返し続けること。
		/// 偽を返すと、この敵は消滅する。
		/// 処理すべきこと：
		/// -- 行動
		/// -- 描画
		/// -- 当たり判定の設置 -&gt; Game.I.AddEnemyCrash(crash, this);
		/// </summary>
		/// <returns>この敵は生存しているか</returns>
		protected abstract IEnumerable<bool> E_Draw();

		/// <summary>
		/// 撃破された時に呼び出される。
		/// -- アイテムの場合はプレイヤーが取得した時に呼び出される。
		/// 処理すべきこと(例)：
		/// -- 爆死エフェクトの追加 -&gt; Game.I.EnemyEffects
		/// -- ドロップアイテムを出現させる。
		/// -- スコア加算
		/// </summary>
		public abstract void Killed();

		/// <summary>
		/// 自弾(プレイヤーの弾)によってダメージを受けた時に呼び出される。
		/// 撃破された時は呼ばれない。
		/// -- 1撃で倒された場合、1度も呼び出されないことになる。
		/// </summary>
		/// <param name="shot">ダメージを受けたプレイヤーの弾</param>
		public virtual void Damaged()
		{
			EnemyCommon.Damaged(this);
		}
	}
}
