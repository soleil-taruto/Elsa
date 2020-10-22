﻿using System;
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
				case "Select": surface = new Surface_Select(); break; // システム・選択肢
				case "サンプル0001": surface = new Surface_サンプル0001(); break; // サンプル
				case "ゆかりUFO": surface = new Surface_ゆかりUFO(); break; // キャラクタ
				case "わたおきば": surface = new Surface_わたおきば(); break; // キャラクタ・セット
				case "暗幕": surface = new Surface_暗幕(); break; // 背景
				case "旧テシ音楽": surface = new Surface_旧テシ音楽(); break; // システム・音楽
				case "旧テシ背景": surface = new Surface_旧テシ背景(); break; // 背景・セット
				case "からい": surface = new Surface_からい(); break; // キャラクタ・セット
				case "結月ゆかり": surface = new Surface_結月ゆかり(); break; // キャラクタ
				case "弦巻マキ": surface = new Surface_弦巻マキ(); break; // キャラクタ
				case "東北ずん子": surface = new Surface_東北ずん子(); break; // キャラクタ
				case "背景": surface = new Surface_背景(); break; // 背景・セット

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
