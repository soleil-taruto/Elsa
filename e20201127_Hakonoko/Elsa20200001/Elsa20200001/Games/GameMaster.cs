using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Designs;

namespace Charlotte.Games
{
	public class GameMaster : IDisposable
	{
		/// <summary>
		/// ステージの番号(インデックス)
		/// 0 == テストステージ
		/// 1～9 == 実際のステージ
		/// </summary>
		public int StartStageIndex;

		// <---- prm

		public static GameMaster I;

		public GameMaster()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private Func<Map>[] MapLoaders = new Func<Map>[]
		{
			() => new Map(@"e20200001_res\Map\0000.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0001.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0002.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0003.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0004.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0005.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0006.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0007.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0008.bmp", new Design_0001()),
			() => new Map(@"e20200001_res\Map\0009.bmp", new Design_0001()),
		};

		public void Perform()
		{
			for (int index = this.StartStageIndex; index < this.MapLoaders.Length; index++)
			{
				using (new Game())
				{
					Game.I.Map = this.MapLoaders[index]();
					Game.I.Perform();

					if (Game.I.ReturnToTitleMenu)
						break;
				}
			}
		}
	}
}
