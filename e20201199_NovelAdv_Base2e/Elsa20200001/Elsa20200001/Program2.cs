﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games;
using Charlotte.Tests;
using Charlotte.Tests.Games;
using System.IO;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			DDMain2.Perform(Main4);
		}

		private void Main4()
		{
			// *.INIT
			{
				// アプリ固有 >

				//RippleEffect.INIT();
				//画面分割.INIT();
				//画面分割_Effect.INIT();

				// < アプリ固有
			}

			#region Charge To DDTouch

			// DDCCResource 等のための Touch
			//DDTouch.Add(TitleMenu.TouchWallDrawerResources);

			// 個別に設定
			//DDTouch.Add(Ground.I.Picture.XXX);
			//DDTouch.Add(Ground.I.Music.XXX);
			//DDTouch.Add(Ground.I.SE.XXX);

			// 全部設定
			//DDTouch.AddAllPicture();
			//DDTouch.AddAllMusic();
			//DDTouch.AddAllSE();

			#endregion

			//DDTouch.Touch(); // moved -> Logo

			if (DDConfig.LOG_ENABLED)
			{
				DDEngine.DispDebug = () =>
				{
					DDPrint.SetPrint();
					DDPrint.SetBorder(new I3Color(0, 0, 0));

					DDPrint.Print(string.Join(" ",
						DDEngine.FrameProcessingMillis,
						DDEngine.FrameProcessingMillis_Worst

						// デバッグ表示する情報をここへ追加..
						));

					DDPrint.Reset();
				};
			}

			if (ProcMain.ArgsReader.ArgIs("//D")) // 引数は適当な文字列
			{
				Main4_Debug();
			}
			else if (ProcMain.ArgsReader.HasArgs()) // シナリオファイルを指定して実行
			{
				string name = ProcMain.ArgsReader.NextArg();
				string scenarioRootDir = Path.Combine(ProcMain.SelfDir, DDConsts.ResourceDir_InternalRelease, Scenario.SCENARIO_FILE_PREFIX);

				scenarioRootDir = SCommon.MakeFullPath(scenarioRootDir);

				name = SCommon.MakeFullPath(name);
				name = SCommon.ChangeRoot(name, scenarioRootDir);
				name = Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)); // remove extention

				using (new Game())
				{
					Game.I.Status.Scenario = new Scenario(name);
					Game.I.Perform();
				}
			}
			else
			{
				Main4_Release();
			}
		}

		private void Main4_Debug()
		{
			// ---- choose one ----

			//Main4_Release();
			//new Test0001().Test01();
			//new Test0001().Test02();
			new TitleMenuTest().Test01();
			//new GameTest().Test01();
			//new GameTest().Test02();
			//new GameTest().Test03(); // シナリオ

			// ----
		}

		private void Main4_Release()
		{
			using (new Logo())
			{
				Logo.I.Perform();
			}
			using (new TitleMenu())
			{
				TitleMenu.I.Perform();
			}
		}
	}
}