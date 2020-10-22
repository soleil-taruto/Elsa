using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class CSemaphore
	{
		private int Permit;

		public CSemaphore(int permit)
		{
			if (permit < 1 || IntTools.IMAX < permit)
				throw new ArgumentException();

			this.Permit = permit;
		}

		private object Enter_SYNCROOT = new object();
		private object SYNCROOT = new object();
		private int Entry = 0;

		// @ 2019.1.7
		// Monitor.Enter -> Monitor.Exit は同一スレッドでなければならないっぽい。
		// (Enter -> 別スレッドで Leave)出来るように ---> (Monitor.Wait -> Monitor.Pulse)にした。

		public void Enter()
		{
			lock (Enter_SYNCROOT)
			{
				lock (SYNCROOT)
				{
					Entry++;

					if (Entry == Permit + 1)
						Monitor.Wait(SYNCROOT);
				}
			}
		}

		public void Leave()
		{
			lock (SYNCROOT)
			{
				if (Entry == 0)
					throw null; // never

				Entry--;

				if (Entry == Permit)
					Monitor.Pulse(SYNCROOT);
			}
		}

		public T Section_Get<T>(Func<T> routine)
		{
			this.Enter();
			try
			{
				return routine();
			}
			finally
			{
				this.Leave();
			}
		}

		public void Section_A(Action routine)
		{
			this.Enter();
			try
			{
				routine();
			}
			finally
			{
				this.Leave();
			}
		}

		public IDisposable Section()
		{
			this.Enter();
			return new AnonyDisposable(() => this.Leave());
		}

		public T Unsection_Get<T>(Func<T> routine)
		{
			this.Leave();
			try
			{
				return routine();
			}
			finally
			{
				this.Enter();
			}
		}

		public void Unsection_A(Action routine)
		{
			this.Leave();
			try
			{
				routine();
			}
			finally
			{
				this.Enter();
			}
		}

		public IDisposable Unsection()
		{
			this.Leave();
			return new AnonyDisposable(() => this.Enter());
		}

		public void ContextSwitching()
		{
			this.Leave();
			this.Enter();
		}
	}
}
