using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Scripts;

namespace Charlotte.Games
{
	public static class GameMaster
	{
		public static bool RestartFlag = false;

		public static void Start(Player.PlayerWho_e plWho)
		{
			do
			{
				RestartFlag = false;

				using (new Game())
				{
					Game.I.Script = new Script_ステージ_01();
					Game.I.Player.PlayerWho = plWho;
					Game.I.Perform();
				}
			}
			while (RestartFlag);
		}
	}
}
