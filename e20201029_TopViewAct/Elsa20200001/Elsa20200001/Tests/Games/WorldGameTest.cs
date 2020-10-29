using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games;

namespace Charlotte.Tests.Games
{
	public class WorldGameTest
	{
		public void Test01()
		{
			using (new WorldGame())
			{
				WorldGame.I.Perform();
			}
		}

		public void Test02()
		{
			using (new WorldGame())
			{
				WorldGame.I.World = new World()
				{
					StartMapName = "t0001",
				};

				WorldGame.I.Status = new GameStatus();
				WorldGame.I.Perform();
			}
		}

		public void Test03()
		{
			string startMapName;

			// ---- choose one ----

			startMapName = "t0001";
			//startMapName = "t0002";
			//startMapName = "t0003";

			// ----

			using (new WorldGame())
			{
				WorldGame.I.World = new World()
				{
					StartMapName = startMapName,
				};

				WorldGame.I.Status = new GameStatus();
				WorldGame.I.Perform();
			}
		}
	}
}
