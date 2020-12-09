using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public class PlayerTracer
	{
		public static bool Recording = false;
		public static DDList<Action> Motions = new DDList<Action>(); // null 要素 == フレームの切れ目

		public static void Reset()
		{
			Recording = false;
			Motions.Clear();
		}

		public static void Start()
		{
			if (Recording)
				throw new DDError("Already started");

			Recording = true;
			Motions.Clear();
		}

		public static bool IsStarted()
		{
			return Recording;
		}

		public static Action[] End()
		{
			if (!Recording)
				throw new DDError("Already ended");

			Recording = false;
			return Motions.Iterate().ToArray();
		}

		public static void Motion(Action motion)
		{
			if (Recording)
				Motions.Add(motion);

			motion();
		}

		public static void EachFrame()
		{
			if (Recording)
				Motions.Add(null); // フレームの切れ目
		}
	}
}
