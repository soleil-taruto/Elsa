using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Shots
{
	public class Shot_B0001 : Shot
	{
		public Shot_B0001(double x, double y, bool facingLeft)
			: base(x, y, facingLeft, 1, true)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				this.X += 8.0 * (this.FacingLeft ? -1 : 1);

				DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawZoom(0.1);
				DDDraw.DrawEnd();

				this.Crash = DDCrashUtils.Circle(new D2Point(this.X, this.Y), 5.0);

				yield return true;
			}
		}
	}
}
