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

				波紋効果.INIT();

				// < アプリ固有
			}

			if (ProcMain.ArgsReader.ArgIs("//D")) // 引数は適当な文字列
			{
				Main4_Debug();
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
			//new Test0001().Test01(); // NG !!!
			//new Test0001().Test02(); // NG !!!
			//new Test0001().Test03(); // NG !!!
			//new Test0001().Test04();
			//new Test0001().Test05();
			//new DDRandomTest().Test01();
			//new TitleMenuTest().Test01();
			new GameMasterTest().Test01(); // 開始ステージを選択

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
