﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		//public Script Script = new Script_ダミー0001(); // 軽量なダミー初期オブジェクト

		// <---- prm

		public static Game I;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public void Perform()
		{
			throw null; // TODO
		}
	}
}
