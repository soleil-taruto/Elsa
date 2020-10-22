using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	public class GameConsts
	{
		public const string DUMMY_SCENARIO_NAME = "001_ダミーシナリオ";
		public const string FIRST_SCENARIO_NAME = "101_スタートシナリオ";

		public const int MESSAGE_SPEED_MIN = 1; // 遅い
		public const int MESSAGE_SPEED_DEF = 3;
		public const int MESSAGE_SPEED_MAX = 5; // 速い

		public const int SAVE_DATA_SLOT_NUM = 10;

		public const double SYSTEM_BUTTON_X = 1060.0;
		public const double SYSTEM_BUTTON_Y = 824.0;
		public const double SYSTEM_BUTTON_X_STEP = 156.0;

		public const int SELECT_FRAME_L = 580;
		public const int SELECT_FRAME_T = 140;
		public const int SELECT_FRAME_T_STEP = 200;
		public const int SELECT_FRAME_NUM = 3;

		public const int SELECT_OPTION_MIN = 1;
		public const int SELECT_OPTION_MAX = SELECT_FRAME_NUM;
	}
}
