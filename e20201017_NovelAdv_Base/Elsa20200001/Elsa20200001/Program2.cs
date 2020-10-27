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
			if (ProcMain.ArgsReader.ArgIs("/D-19791231-96")) // 引数は適当な文字列
			{
				Main4_Debug();
			}
			else if (ProcMain.ArgsReader.HasArgs()) // シナリオファイルを指定して実行
			{
				string name = ProcMain.ArgsReader.NextArg();
				string scenarioRootDir = Path.Combine(ProcMain.SelfDir, DDConsts.ResourceDir_InternalRelease_02, Scenario.SCENARIO_FILE_PREFIX);

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

			Main4_Release();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new TitleMenuTest().Test01();
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
