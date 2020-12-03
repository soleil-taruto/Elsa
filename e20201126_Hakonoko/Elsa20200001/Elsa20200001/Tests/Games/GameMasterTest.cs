using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class GameMasterTest
	{
		public void Test01()
		{
			int startStageIndex;

			// ---- choose one ----

			startStageIndex = 0;
			//startStageIndex = 1;
			//startStageIndex = 2;
			//startStageIndex = 3;
			//startStageIndex = 4;
			//startStageIndex = 5;
			//startStageIndex = 6;
			//startStageIndex = 7;
			//startStageIndex = 8;
			//startStageIndex = 9;

			// ----

			using (new GameMaster())
			{
				GameMaster.I.StartStageIndex = startStageIndex;
				GameMaster.I.Perform();
			}
		}
	}
}
