using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games
{
	public static class 獣道効果
	{
		public static void Perform()
		{
			List<Func<bool>> a_routines = new List<Func<bool>>();

			a_routines.Add(SCommon.Supplier(E_効果_01()));
			a_routines.Add(SCommon.Supplier(E_効果_02()));

			while (1 <= a_routines.Count)
			{
				a_routines.RemoveAll(v => !v());

				DDEngine.EachFrame();
			}
		}

		private static IEnumerable<bool> E_効果_01()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDDraw.DrawBegin(
					Ground.I.Picture.獣道,
					DDConsts.Screen_W / 2 - 50.0 * (1.0 - scene.Rate),
					DDConsts.Screen_H / 2 + 50.0 * (1.0 - scene.Rate)
					);
				DDDraw.DrawRotate(-0.3 + 0.2 * scene.Rate);
				DDDraw.DrawZoom(3.5 + 1.0 * scene.Rate);
				DDDraw.DrawEnd();

				yield return true;
			}
		}

		private static IEnumerable<bool> E_効果_02()
		{
			foreach (DDScene scene in DDSceneUtils.Create(30))
				yield return true;

			double a = 0.0;

			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDUtils.Approach(ref a, 1.0, 0.9);

				DDDraw.SetAlpha(a);
				DDDraw.DrawBegin(
					Ground.I.Picture.獣道,
					DDConsts.Screen_W / 2 + 50.0 * (1.0 - scene.Rate),
					DDConsts.Screen_H / 2 + 50.0 * (1.0 - scene.Rate)
					);
				DDDraw.DrawRotate(0.3 - 0.2 * scene.Rate);
				DDDraw.DrawZoom(3.5 + 1.5 * scene.Rate);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
