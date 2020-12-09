using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Enemies
{
	public class Enemy_Death : Enemy
	{
		public Enemy_Death(D2Point pos)
			: base(pos)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				double bright = Math.Sin(DDEngine.ProcFrame / 10.0 + this.X + this.Y) * 0.4 + 0.6;

				DDDraw.SetBright(
					bright * 1.0,
					bright * 0.0,
					bright * 0.0
					);
				DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, SCommon.ToInt(this.X - DDGround.ICamera.X), SCommon.ToInt(this.Y - DDGround.ICamera.Y));
				DDDraw.DrawSetSize(GameConsts.TILE_W, GameConsts.TILE_H);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				this.Crash = DDCrashUtils.Rect_CenterSize(new D2Point(this.X, this.Y), new D2Size(GameConsts.TILE_W, GameConsts.TILE_H));

				yield return true;
			}
		}
	}
}
