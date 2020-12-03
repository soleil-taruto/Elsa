﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Walls
{
	public class Wall_B0004 : Wall
	{
		public override IEnumerable<bool> E_Draw()
		{
			Func<double> getA = SCommon.Supplier(WallCommon.E_GetA_フェードイン(this));

			for (int frame = 0; ; frame++)
			{
				DDDraw.SetAlpha(getA());

				{
					int slide = (int)((frame * 3L) % 108L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 108)
					{
						for (int dy = 0; dy < DDConsts.Screen_H; dy += 108)
						{
							DDDraw.DrawSimple(Ground.I.Picture.Wall0002, dx, dy);
						}
					}
				}

				{
					int slide = (int)((frame * 13L) % 90L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 90)
					{
						for (int dy = 0; dy < DDConsts.Screen_H; dy += 90)
						{
							DDDraw.DrawSimple(Ground.I.Picture.Wall0003, dx, dy);
						}
					}
				}

				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
