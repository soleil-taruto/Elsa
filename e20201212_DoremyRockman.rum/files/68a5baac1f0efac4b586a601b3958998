using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Novels;

namespace Charlotte.Games
{
	public class WorldGameNovelMaster : IDisposable
	{
		public static WorldGameNovelMaster I;

		public WorldGameNovelMaster()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			// zantei zantei zantei
			// zantei zantei zantei
			// zantei zantei zantei

			using (new Novel())
			{
				Novel.I.Status.Scenario = new Scenario("101_ゲームスタート");
				Novel.I.Perform();
			}
			using (new WorldGameMaster())
			{
				WorldGameMaster.I.World = new World()
				{
					StartMapName = "t0001", // 仮？
				};

				WorldGameMaster.I.Status = new GameStatus();
				WorldGameMaster.I.Perform();
			}
		}
	}
}
