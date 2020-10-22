using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class ThreadEx : IDisposable
	{
		private Thread Th;
		private Exception Ex = null;

		public ThreadEx(Action routine)
		{
			Th = new Thread(() =>
			{
				try
				{
					routine();
				}
				catch (Exception e)
				{
					Ex = e;
				}
			});

			Th.Start();
		}

		public bool IsEnded(int millis = 0)
		{
			if (Th != null && Th.Join(millis))
				Th = null;

			return Th == null;
		}

		public void WaitToEnd()
		{
			if (Th != null)
			{
				Th.Join();
				Th = null;
			}
		}

		public void WaitToEnd(Critical critical)
		{
			if (Th != null)
			{
				critical.Unsection_A(() => Th.Join());
				Th = null;
			}
		}

		public void RelayThrow()
		{
			this.WaitToEnd();

			if (this.Ex != null)
				throw new AggregateException("Relay", this.Ex);
		}

		public Exception GetException()
		{
			this.WaitToEnd();
			return this.Ex;
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
