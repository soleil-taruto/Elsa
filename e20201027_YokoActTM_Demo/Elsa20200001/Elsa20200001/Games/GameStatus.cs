﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの状態を保持する。
	/// プレイヤーのレベルとか保有アイテムといった概念が入ってくることを想定して、独立したクラスとする。
	/// </summary>
	public class GameStatus
	{
		// (テスト時など)特にフィールドを設定せずにインスタンスを生成する使い方を想定して、
		// 全てのパラメータはデフォルト値で初期化すること。

		/// <summary>
		/// プレイヤーの最大HP
		/// </summary>
		public int MaxHP = 10;

		/// <summary>
		/// (マップ入場時の)プレイヤーのHP
		/// </summary>
		public int StartHP = 10;

		/// <summary>
		/// スタート地点の Direction 値
		/// 5 == 中央(デフォルト) == ゲームスタート時
		/// 2 == 下から入場
		/// 4 == 左から入場
		/// 6 == 右から入場
		/// 8 == 上から入場
		/// </summary>
		public int StartPointDirection = 5;

		/// <summary>
		/// (マップ入場時に)プレイヤーが左を向いているか
		/// </summary>
		public bool StartFacingLeft = false;

		/// <summary>
		/// 最後のマップを退場した方向
		/// 5 == 中央(デフォルト) == 死亡・ゲーム終了時
		/// 2 == 下から退場
		/// 4 == 左から退場
		/// 6 == 右から退場
		/// 8 == 上から退場
		/// </summary>
		public int ExitDirection = 5;

		/// <summary>
		/// (マップ入場時に)プレイヤーが装備している武器
		/// </summary>
		public Player.武器_e Start_武器 = Player.武器_e.NORMAL;

		// <---- prm
	}
}
