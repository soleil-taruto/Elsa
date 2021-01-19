using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class Logo : IDisposable
	{
		// <---- prm

		public static Logo I;

		public Logo()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			// 開発中_暫定
			{
				int endFrame = DDEngine.ProcFrame + 300;

				DDGround.EL.Add(() =>
				{
					int sec = endFrame - DDEngine.ProcFrame;

					DDPrint.SetPrint(180, DDConsts.Screen_H - 32);
					DDPrint.Print("これはクローズドテスト版です。仮リソース・実装されていない機能を含みます。(あと " + (sec / 60.0).ToString("F1") + " 秒で消えます)");

					return 0 < sec;
				});
			}

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
			}

			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain();

				DDDraw.SetAlpha(scene.Rate);
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.Reset();

				DDEngine.EachFrame();
			}

			{
				long endLoopTime = long.MaxValue;

				for (int frame = 0; ; frame++)
				{
					if (endLoopTime < DDEngine.FrameStartTime)
						break;

					if (frame == 1)
					{
						endLoopTime = DDEngine.FrameStartTime + 1500;
						DDTouch.Touch();
					}
					DDCurtain.DrawCurtain();
					DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDEngine.EachFrame();
				}
			}

			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDCurtain.DrawCurtain();

				DDDraw.SetAlpha(1.0 - scene.Rate);
				DDDraw.DrawCenter(Ground.I.Picture.Copyright, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
	}
}
