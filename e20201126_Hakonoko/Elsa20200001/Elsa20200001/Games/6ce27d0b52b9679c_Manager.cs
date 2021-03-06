﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games
{
	public static class 波紋効果_Manager
	{
		private static DDTaskList _el = new DDTaskList();

		public static void Add(Func<bool> task)
		{
			if (4 <= _el.Count)
			{
				_el.RemoveAt(0);
			}
			_el.Add(task);
		}

		public static void EachFrame()
		{
			_el.ExecuteAllTask();
		}

		public static int Count
		{
			get
			{
				return _el.Count;
			}
		}
	}
}
