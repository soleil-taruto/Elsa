using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons.Options;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Shots
{
	/// <summary>
	/// ボス登場時などに使用するシステム・ボム
	/// 画面上の敵を一掃する。
	/// </summary>
	public class Shot_BossBomb : Shot
	{
		public Shot_BossBomb()
			: base(0, 0, Kind_e.BOMB, SCommon.IMAX)
		{ }

		protected override IEnumerable<bool> E_Draw()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				this.Crash = DDCrashUtils.Rect(D4Rect.LTRB(
					0,
					0,
					GameConsts.FIELD_W,
					GameConsts.FIELD_H
					));

				yield return true;
			}
		}
	}
}
