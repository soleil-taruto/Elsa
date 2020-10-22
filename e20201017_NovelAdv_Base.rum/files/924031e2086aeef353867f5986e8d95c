using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public static class SurfaceCreator
	{
		public static Surface Create(string typeName, string instanceName)
		{
			Surface surface;

			// typeName はクラス名 Surface_<typeName> と対応する。難読化するのでリフレクションにすることは出来ない。

			switch (typeName)
			{
				case "MessageWindow": surface = new Surface_MessageWindow(); break;
				case "Select": surface = new Surface_Select(); break;
				case "System": surface = new Surface_System(); break;
				case "SystemButtons": surface = new Surface_SystemButtons(); break;
				case "キャラクタ": surface = new Surface_キャラクタ(); break;
				case "スクリーン": surface = new Surface_スクリーン(); break;
				case "音楽": surface = new Surface_音楽(); break;
				case "効果音": surface = new Surface_効果音(); break;

				// 新しいキャラクタをここへ追加..

				default:
					throw new DDError("不明なタイプ名：" + typeName);
			}
			surface.TypeName = typeName;
			surface.InstanceName = instanceName;

			return surface;
		}
	}
}
