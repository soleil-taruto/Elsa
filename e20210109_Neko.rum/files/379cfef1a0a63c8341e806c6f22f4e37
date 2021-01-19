using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games
{
	public static class GameCommon
	{
		public static string WrapNullOrString(string value)
		{
			return value == null ? GameConsts.SERIALIZED_NULL : GameConsts.SERIALIZED_NOT_NULL_PREFIX + value;
		}

		public static string UnwrapNullOrString(string value)
		{
			return value == GameConsts.SERIALIZED_NULL ? null : value.Substring(GameConsts.SERIALIZED_NOT_NULL_PREFIX.Length);
		}
	}
}
