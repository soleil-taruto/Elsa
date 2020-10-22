using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class LimitCounter
	{
		private int Count;

		public LimitCounter(int count)
		{
			this.Count = count;
		}

		public bool Issue()
		{
			if (1 <= this.Count)
			{
				this.Count--;
				return true;
			}
			return false;
		}

		public static LimitCounter One()
		{
			return new LimitCounter(1);
		}
	}
}
