using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class LongTools
	{
		public const long IMAX_64 = 1000000000000000000L; // 10^18

		public static int Comp(long a, long b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		public static long ToRange(long value, long minval, long maxval)
		{
			return Math.Max(minval, Math.Min(maxval, value));
		}

		public static long ToLong(string str, long minval, long maxval, long defval)
		{
			try
			{
				int value = int.Parse(str);

				if (value < minval || maxval < value)
					throw null;

				return value;
			}
			catch
			{
				return defval;
			}
		}
	}
}
