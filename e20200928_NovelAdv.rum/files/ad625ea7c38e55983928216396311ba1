using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class DoubleTools
	{
		public static int Comp(double a, double b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		private static void CheckNaN(double value)
		{
			if (double.IsNaN(value))
				throw new Exception("NaN");
		}

		public static double ToRange(double value, double minval, double maxval)
		{
			CheckNaN(value);

			return Math.Max(minval, Math.Min(maxval, value));
		}

		public static double ToDouble(string str, double minval, double maxval, double defval)
		{
			try
			{
				return ToRange(double.Parse(str), minval, maxval);
			}
			catch
			{
				return defval;
			}
		}

		public static int ToInt(double value)
		{
			CheckNaN(value);

			if (value < 0.0)
				return (int)(value - 0.5);
			else
				return (int)(value + 0.5);
		}

		public static long ToLong(double value)
		{
			CheckNaN(value);

			if (value < 0.0)
				return (long)(value - 0.5);
			else
				return (long)(value + 0.5);
		}
	}
}
