﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Walls
{
	public class Wall_1001 : Wall
	{
		protected override IEnumerable<bool> E_Draw()
		{
			return WallCommon.Standard(this, Ground.I.Picture.P_BLUETILE_02_REDUCT4, 0, 2, -128, 0, 0.01, 1.0, true);
		}
	}
}
