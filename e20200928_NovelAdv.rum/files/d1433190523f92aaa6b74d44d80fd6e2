using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MultiThreadTaskInvoker : IDisposable
	{
		public int ThreadCountMax = Math.Max(1, Environment.ProcessorCount);
		public int ExceptionCountMax = 10;

		// <---- prop

		private object SYNCROOT = new object();
		private List<Thread> Ths = new List<Thread>();
		private Queue<Action> Tasks = new Queue<Action>();
		private List<Exception> Exs = new List<Exception>();

		public void AddTask(Action task)
		{
			lock (SYNCROOT)
			{
				this.Tasks.Enqueue(task);

				if (this.Ths.Count < this.ThreadCountMax)
				{
					Thread th = new Thread(() =>
					{
						for (; ; )
						{
							Action nextTask;

							lock (SYNCROOT)
							{
								if (this.Tasks.Count <= 0)
								{
									this.Ths.Remove(Thread.CurrentThread);
									return;
								}
								nextTask = this.Tasks.Dequeue();
							}

							try
							{
								nextTask();
							}
							catch (Exception e)
							{
								lock (SYNCROOT)
								{
									if (this.Exs.Count < this.ExceptionCountMax)
										this.Exs.Add(e);
								}
							}
						}
					});

					th.Start();

					this.Ths.Add(th);
				}
			}
		}

		public bool IsEnded()
		{
			lock (SYNCROOT)
			{
				return this.Ths.Count <= 0;
			}
		}

		public void WaitToEnd()
		{
			for (; ; )
			{
				Thread th;

				lock (SYNCROOT)
				{
					if (this.Ths.Count <= 0)
						break;

					th = this.Ths[0];
				}
				th.Join();
			}
		}

		public void RelayThrow()
		{
			this.WaitToEnd();

			if (1 <= this.Exs.Count)
				throw new AggregateException("Relay", this.Exs);
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
