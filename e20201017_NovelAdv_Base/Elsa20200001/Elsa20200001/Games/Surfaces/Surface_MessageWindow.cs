﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_MessageWindow : Surface
	{
		public static bool Hide = false; // Game から制御される。

		private double A = 1.0;

		public Surface_MessageWindow()
		{
			this.Z = 60000;
		}

		public override void Draw()
		{
			const int h = 272;

			DDUtils.Approach(ref this.A, Hide ? 0.0 : 1.0, 0.9);

			DDDraw.SetAlpha(this.A);
			DDDraw.DrawRect(Ground.I.Picture.MessageFrame_Message, 0, DDConsts.Screen_H - h, DDConsts.Screen_W, h);
			DDDraw.Reset();
		}
	}
}
