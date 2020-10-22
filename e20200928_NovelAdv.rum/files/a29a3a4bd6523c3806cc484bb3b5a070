using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Games.Surfaces;
using Charlotte.Tools;

namespace Charlotte.Games
{
	/// <summary>
	/// ゲームの現在の状態を保持する。
	/// セーブ・ロード時にこのクラスの内容を保存・再現する。
	/// </summary>
	public class GameStatus
	{
		public Scenario Scenario = new Scenario(GameConsts.DUMMY_SCENARIO_NAME); // 軽量な仮設オブジェクト
		public int CurrPageIndex = 0;
		public List<Surface> Surfaces = new List<Surface>(); // 軽量な仮設オブジェクト

		// <---- prm

		public int GetSurfaceIndex(string instanceName)
		{
			for (int index = 0; index < this.Surfaces.Count; index++)
				if (this.Surfaces[index].InstanceName == instanceName)
					return index;

			throw new DDError("存在しないインスタンス名：" + instanceName);
		}

		public Surface GetSurface(string instanceName)
		{
			return this.Surfaces[this.GetSurfaceIndex(instanceName)];
		}

		/// <summary>
		/// <para>surface を削除する。</para>
		/// <para>this.Surfaces にある surface を削除する処理は全てここに到達する。</para>
		/// </summary>
		/// <param name="index">削除する surface のインデックス</param>
		private void RemoveSurface(int index)
		{
			Surface surface = this.Surfaces[index];

			// surface 削除時にすること
			{
				Game.I.SurfaceEL.Add(surface.Act.Draw);
			}

			ExtraTools.FastDesertElement(this.Surfaces, index);
		}

		public void RemoveSurface(string instanceName)
		{
			this.RemoveSurface(this.GetSurfaceIndex(instanceName));
		}

		public void RemoveSurface(Surface surface)
		{
			this.RemoveSurface(surface.InstanceName);
		}
	}
}
