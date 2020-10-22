﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Surfaces;
using Charlotte.Commons;

namespace Charlotte.Games
{
	/// <summary>
	/// <para>ゲームの現在の状態を保持する。</para>
	/// <para>セーブ・ロード時にこのクラスの内容を保存・再現する。</para>
	/// </summary>
	public class GameStatus
	{
		public Scenario Scenario = new Scenario(GameConsts.DUMMY_SCENARIO_NAME); // 軽量な仮設オブジェクト
		public int CurrPageIndex = 0;
		public List<Surface> Surfaces = new List<Surface>(); // 軽量な仮設オブジェクト

		// <---- prm

		// 特別なサーフェス >

		public bool HasSelect()
		{
			return this.Surfaces.Any(v => v is Surface_Select);
		}

		public Surface_Select GetSelect()
		{
			return (Surface_Select)this.Surfaces.First(v => v is Surface_Select);
		}

		public void RemoveSelect()
		{
			this.Surfaces.RemoveAll(v => v is Surface_Select);
		}

		// < 特別なサーフェス

		public int GetSurfaceIndex(string instanceName, int defval)
		{
			for (int index = 0; index < this.Surfaces.Count; index++)
				if (this.Surfaces[index].InstanceName == instanceName)
					return index;

			return defval;
		}

		public int GetSurfaceIndex(string instanceName)
		{
			int index = this.GetSurfaceIndex(instanceName, -1);

			if (index == -1)
				throw new DDError("存在しないインスタンス名：" + instanceName);

			return index;
		}

		public Surface GetSurface(string instanceName)
		{
			return this.Surfaces[this.GetSurfaceIndex(instanceName)];
		}

		/// <summary>
		/// <para>サーフェスを削除する。</para>
		/// <para>this.Surfaces に含まれるサーフェスを削除する処理は全てここに到達する。</para>
		/// </summary>
		/// <param name="index">削除するサーフェスのインデックス</param>
		private void RemoveSurface(int index)
		{
			Surface surface = this.Surfaces[index];

			// サーフェス削除時にすること
			{
				//Game.I.SurfaceEL.Add(surface.Act.Draw); // 廃止
			}

			SCommon.FastDesertElement(this.Surfaces, index);
		}

		public void RemoveSurfaceIfExist(string instanceName)
		{
			int index = this.GetSurfaceIndex(instanceName, -1);

			if (index != -1)
				this.RemoveSurface(index);
		}

		public void RemoveSurface(string instanceName)
		{
			this.RemoveSurface(this.GetSurfaceIndex(instanceName));
		}

		public void RemoveSurface(Surface surface)
		{
			this.RemoveSurface(surface.InstanceName);
		}

		public string Serialize()
		{
			return AttachString.I.Untokenize(new string[]
			{
				this.Scenario.Name,
				this.CurrPageIndex.ToString(),
				AttachString.I.Untokenize(this.Surfaces.Select(v => AttachString.I.Untokenize(new string[]
				{
					v.TypeName,
					v.InstanceName,
					v.Serialize(),
				}
				))),
			});
		}

		private void S_Deserialize(string value)
		{
			string[] lines = AttachString.I.Tokenize(value);
			int c = 0;

			this.Scenario = new Scenario(lines[c++]);
			this.CurrPageIndex = int.Parse(lines[c++]);
			this.Surfaces = AttachString.I.Tokenize(lines[c++]).Select(v =>
			{
				string[] lines2 = AttachString.I.Tokenize(v);
				int c2 = 0;

				string typeName = lines2[c2++];
				string instanceName = lines2[c2++];
				string value2 = lines2[c2++];

				Surface surface = SurfaceCreator.Create(typeName, instanceName);
				surface.Deserialize(value2);
				return surface;
			})
			.ToList();
		}

		public static GameStatus Deserialize(string value)
		{
			GameStatus gameStatus = new GameStatus();
			gameStatus.S_Deserialize(value);
			return gameStatus;
		}
	}
}
