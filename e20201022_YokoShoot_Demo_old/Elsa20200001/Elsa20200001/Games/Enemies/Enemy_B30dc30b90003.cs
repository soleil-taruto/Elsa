using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Bボス0003 : Enemy
	{
		public Enemy_Bボス0003()
			: base(DDConsts.Screen_W + 96.0, DDConsts.Screen_H / 2.0, 100)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			throw null; // TODO
		}
	}
}
