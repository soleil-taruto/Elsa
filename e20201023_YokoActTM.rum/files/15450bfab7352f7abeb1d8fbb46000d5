using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Common
	{
		public static S GetElement<S, T>(T[] arr, int index, Func<T, S> conv, S defval)
		{
			if (index < arr.Length)
			{
				return conv(arr[index]);
			}
			else
			{
				return defval;
			}
		}
	}
}
