﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
		/// <summary>
		/// 何もない空間のタイル名
		/// </summary>
		public const string TILE_NONE = "None";

		/// <summary>
		/// 何も配置しない場合の敵の名前
		/// </summary>
		public const string ENEMY_NONE = "None";

		/// <summary>
		/// デフォルトの壁紙の名前
		/// デフォルトの音楽の名前
		/// </summary>
		public const string NAME_DEFAULT = "Default";

		public const int TILE_W = 32;
		public const int TILE_H = 32;

		public const int PLAYER_DEAD_FRAME_MAX = 180;
		public const int PLAYER_DAMAGE_FRAME_MAX = 20;
		public const int PLAYER_INVINCIBLE_FRAME_MAX = 60;

		/// <summary>
		/// ジャンプ回数の上限
		/// 1 == 通常
		/// 2 == 2-段ジャンプまで可能
		/// 3 == 3-段ジャンプまで可能
		/// ...
		/// </summary>
		public const int JUMP_MAX = 2;

		/// <summary>
		/// プレイヤーキャラクタの重力加速度
		/// </summary>
		public const double PLAYER_GRAVITY = 0.6;
		//public const double PLAYER_GRAVITY = 0.8; // 速すぎる。@ 2020.11.4
		//public const double PLAYER_GRAVITY = 0.4; // 遅すぎる。@ 2020.11.4

		/// <summary>
		/// プレイヤーキャラクタの落下最高速度
		/// </summary>
		public const double PLAYER_FALL_SPEED_MAX = 12.0;
		//public const double PLAYER_FALL_SPEED_MAX = 16.0;

		/// <summary>
		/// プレイヤーキャラクタの(横移動)速度
		/// </summary>
		public const double PLAYER_SPEED = 6.0;

		/// <summary>
		/// プレイヤーキャラクタの低速移動時の(横移動)速度
		/// </summary>
		public const double PLAYER_SLOW_SPEED = 2.0;

		public const double PLAYER_側面判定Pt_X = 10.0;
		public const double PLAYER_側面判定Pt_Y = TILE_H - 1.0;
		public const double PLAYER_脳天判定Pt_X = 9.0;
		public const double PLAYER_脳天判定Pt_Y = 40.0;
		//public const double PLAYER_脳天判定Pt_Y = 48.0;
		public const double PLAYER_接地判定Pt_X = 9.0;
		public const double PLAYER_接地判定Pt_Y = 48.0;
	}
}
