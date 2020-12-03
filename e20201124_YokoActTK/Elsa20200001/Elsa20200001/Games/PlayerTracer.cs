using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Games.Enemies;

namespace Charlotte.Games
{
	public class PlayerTracer
	{
		/// <summary>
		/// 最長記録時間
		/// 単位：フレーム
		/// </summary>
		public const int MOMENTS_LEN_MAX = 180;

		/// <summary>
		/// この一瞬(このフレーム)の情報
		/// 重い！
		/// </summary>
		public class MomentInfo
		{
			public int HP = Game.I.Player.HP;
			public double X = Game.I.Player.X;
			public double Y = Game.I.Player.Y;
			public Enemy[] Enemies = Game.I.Enemies.Iterate().Select(v => v.GetClone()).ToArray();
			public List<Action> Motions = new List<Action>(16);
		}

		public static DDList<MomentInfo> Moments = new DDList<MomentInfo>();

		public static void Reset()
		{
			Moments.Clear();
		}

		public static MomentInfo[] GetMoments()
		{
			MomentInfo[] ret = Moments.Iterate().ToArray();
			//Moments.Clear(); // moved -> Clear()
			return ret;
		}

		public static void Clear()
		{
			Moments.Clear();
		}

		public static void Motion(Action motion)
		{
			if (1 <= Moments.Count) // ? モーション記録を開始している。
			{
				Moments[Moments.Count - 1].Motions.Add(motion);
			}
			motion();
		}

		public static void EachFrame()
		{
			if (Game.I != null) // ? ゲーム中
			{
				Moments.Add(new MomentInfo());

				if (MOMENTS_LEN_MAX < Moments.Count)
				{
					Moments.RemoveAt(0);
				}
			}
		}
	}
}
