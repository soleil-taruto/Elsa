﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
		public const int FIELD_L = 20;
		public const int FIELD_T = 10;
		public const int FIELD_W = 480;
		public const int FIELD_H = 580;

		public const int ITEM_GET_BORDER_Y = 200;
		public const int ITEM_GET_MAX_Y = FIELD_H + 200;

		public const int BARAN_DIV = 32;

		// プレイヤーレベル == 0 ～ PLAYER_LEVEL_MAX

		public const int PLAYER_LEVEL_MAX = 5;
		public const int PLAYER_POWER_PER_LEVEL = 100;
		public const int PLAYER_POWER_MAX = PLAYER_LEVEL_MAX * PLAYER_POWER_PER_LEVEL;

		public const int PLAYER_BORN_FRAME_MAX = 300;
		public const int PLAYER_DEAD_FRAME_MAX = 60;
		public const int PLAYER_BOMB_FRAME_MAX = 180;

		public const int DEFAULT_ZANKI = 5;
		public const int DEFAULT_ZAN_BOMB = 5;

		public const double SHOT_A = 0.3;
	}
}
