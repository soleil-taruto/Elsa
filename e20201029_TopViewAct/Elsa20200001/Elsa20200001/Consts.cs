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
		/// </summary>
		public const string WALL_DEFAULT = "Default";

		public const int TILE_W = 32;
		public const int TILE_H = 32;

		public const int PLAYER_DEAD_FRAME_MAX = 180;
		public const int PLAYER_DAMAGE_FRAME_MAX = 20;
		public const int PLAYER_INVINCIBLE_FRAME_MAX = 60;
	}
}
