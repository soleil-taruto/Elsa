using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

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
				Game.I.Status.Scenario = new Scenario(GameConsts.FIRST_SCENARIO_NAME);
				Game.I.Perform();
			}
		}

		public void Test03()
		{
			string name;

			// ---- choose one ----

			//name = "サンプル0001";
			//name = "サンプル0002";
			//name = "サンプル0003";
			//name = "テスト0001";
			name = "テスト0002";
			//name = @"旧テストシナリオ\101_スタートシナリオ";

			// ----

			using (new Game())
			{
				Game.I.Status.Scenario = new Scenario(name);
				Game.I.Perform();
			}
		}
	}
}
