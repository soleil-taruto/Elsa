using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	/// <summary>
	/// 1000/1/1 ～ 9999/12/31
	/// </summary>
	public static class DateToDay
	{
		public static int ToDay(int date)
		{
			return (int)(DateTimeToSec.ToSec(date * 1000000L) / 86400L);
		}

		public static int ToDate(int day)
		{
			return (int)(DateTimeToSec.ToDateTime(day * 86400L) / 1000000L);
		}

		/// <summary>
		/// 1/1/1 ～ 9999/12/31
		/// </summary>
		public static class Allow5To7Dig
		{
			public static int ToDay(int date)
			{
				return (int)(DateTimeToSec.Allow11To13Dig.ToSec(date * 1000000L) / 86400L);
			}

			public static int ToDate(int day)
			{
				return (int)(DateTimeToSec.Allow11To13Dig.ToDateTime(day * 86400L) / 1000000L);
			}
		}

		public static class Now
		{
			public static int GetDay()
			{
				return (int)(DateTimeToSec.Now.GetSec() / 86400L);
			}

			public static int GetDate()
			{
				return (int)(DateTimeToSec.Now.GetDateTime() / 1000000L);
			}
		}
	}
}
