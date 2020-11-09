﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Shots;

namespace Charlotte.Games.Attacks
{
	public class Attack_ほむら接地攻撃 : Attack
	{
		public override bool IsInvincibleMode()
		{
			return false;
		}

		protected override IEnumerable<bool> E_Draw()
		{
			for (int frame = 0; ; frame++)
			{
				int koma = frame / 4;

				if (Ground.I.Picture2.ほむらバズーカ.Length <= koma)
					break;

				double xZoom = Game.I.Player.FacingLeft ? -1.0 : 1.0;

				if (frame == 6 * 4)
					Game.I.Shots.Add(new Shot_ほむら接地攻撃(
						Game.I.Player.X + 50.0 * xZoom,
						Game.I.Player.Y - 18.0,
						Game.I.Player.FacingLeft
						));

				DDDraw.SetTaskList(Game.I.Player.Draw_EL);
				DDDraw.DrawBegin(
					Ground.I.Picture2.ほむらバズーカ[koma],
					Game.I.Player.X - DDGround.ICamera.X,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom_X(xZoom);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
