﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_暗幕 : Surface
	{
		public override void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
