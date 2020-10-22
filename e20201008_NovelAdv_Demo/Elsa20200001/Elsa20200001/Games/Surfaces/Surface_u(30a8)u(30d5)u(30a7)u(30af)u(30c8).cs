using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_エフェクト : Surface
	{
		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "お菓子エフェクト")
			{
				this.Act.Add(SCommon.Supplier(this.お菓子エフェクト()));
			}
			else if (command == "フラッシュ")
			{
				double startWL = double.Parse(arguments[c++]);
				double destWL = double.Parse(arguments[c++]);
				int frame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.フラッシュ(startWL, destWL, frame)));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> お菓子エフェクト()
		{
			double fall = 0.0;
			double fallAdd = 0.0;

			foreach (DDPicture picture in Ground.I.Picture.お菓子エフェクト)
			{
				for (int c = 0; c < 3; c++)
				{
					DDDraw.SetAlpha(0.7);
					DDDraw.DrawCenter(picture, this.X, this.Y + fall);
					DDDraw.Reset();

					fall += fallAdd;
					fallAdd += 0.1;

					yield return true;
				}
			}
		}

		private IEnumerable<bool> フラッシュ(double startWL, double destWL, int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				DDCurtain.DrawCurtain(DDUtils.AToBRate(startWL, destWL, scene.Rate));

				yield return true;
			}
		}
	}
}
