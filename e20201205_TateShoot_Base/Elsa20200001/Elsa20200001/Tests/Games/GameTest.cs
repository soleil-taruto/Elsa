﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;
using Charlotte.Games.Scripts;

namespace Charlotte.Tests.Games
{
	public class GameTest
	{
		public void Test01()
		{
			using (new Game())
			{
				Game.I.Perform();
			}
		}

		public void Test02()
		{
			using (new Game())
			{
				Game.I.Script = new Script_テスト0001();
				Game.I.Perform();
			}
		}

		public void Test03()
		{
			Script script;

			// ---- chooese one ----

			//script = new Script_ダミー0001();
			//script = new Script_テスト0001();
			//script = new Script_テスト0002();
			//script = new Script_テスト1001(); // サンプルゲーム用メイン0001
			//script = new Script_鍵山雛テスト0001();
			//script = new Script_鍵山雛テスト0002();
			script = new Script_鍵山雛通しテスト0001();

			// ----

			using (new Game())
			{
				Game.I.Script = script;
				Game.I.Perform();
			}
		}
	}
}
