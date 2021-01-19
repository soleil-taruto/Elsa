using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
		public const string SERIALIZED_NULL = "<SZ-NULL>";
		public const string SERIALIZED_NOT_NULL_PREFIX = "<SZ-NOT-NULL>:";

		public const int MESSAGE_SPEED_MIN = 1; // 遅い
		public const int MESSAGE_SPEED_DEF = 3;
		public const int MESSAGE_SPEED_MAX = 5; // 速い

		public const int GAME_SAVE_DATA_SLOT_NUM = 10;
	}
}
