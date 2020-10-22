using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_磨りガラス : Surface
	{
		private DDSubScreen BokaScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H) { ForPrintFlag = true };

		private double Bokashi = 0.0;
		private double DestBokashi = 0.0;

		public override void Draw()
		{
			DDUtils.Approach(ref this.Bokashi, this.DestBokashi, 0.99);

			if (0.001 < this.Bokashi)
			{
				for (int c = 0; c < 2; c++)
				{
					using (this.BokaScreen.Section())
					{
						DX.DrawExtendGraph(0, 0, DDConsts.Screen_W, DDConsts.Screen_H, DDGround.MainScreen.GetHandle(), 0);

						DX.GraphFilter(
							this.BokaScreen.GetHandle(),
							DX.DX_GRAPH_FILTER_GAUSS,
							16,
							SCommon.ToInt(this.Bokashi * 300.0)
							);
					}
					DDDraw.DrawRect(this.BokaScreen.ToPicture(), 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
				}
			}
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "Rate")
			{
				this.DestBokashi = double.Parse(arguments[c++]);
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Bokashi.ToString("F9"),
				this.DestBokashi.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Bokashi = double.Parse(lines[c++]);
			this.DestBokashi = double.Parse(lines[c++]);
		}
	}
}
