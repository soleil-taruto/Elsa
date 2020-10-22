using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class FilingCase3 : IDisposable
	{
		private string Domain;
		private int PortNo;
		private string BasePath;

		private Thread Th;
		private NamedEventPair EvStop = new NamedEventPair();
		private object SYNCROOT = new object();
		private FilingCase3Client Client = null;
		private int ClientAliveCount = -1;

		public FilingCase3(string domain = "localhost", int portNo = 65123, string basePath = "Common")
		{
			this.Domain = domain;
			this.PortNo = portNo;
			this.BasePath = basePath;

			this.Th = new Thread(() =>
			{
				while (this.EvStop.WaitForMillis(3000) == false)
				{
					lock (SYNCROOT)
					{
						if (this.Client != null)
						{
							if (6 / 3 < ++this.ClientAliveCount) // 6 sec
							{
								this.Client.Dispose();
								this.Client = null;
							}
						}
					}
				}
			});

			this.Th.Start();
		}

		private T Perform<T>(Func<T> rtn)
		{
			lock (SYNCROOT)
			{
				try
				{
					if (this.Client == null)
						this.Client = new FilingCase3Client(this.Domain, this.PortNo, this.BasePath);

					this.ClientAliveCount = 0;

					return rtn();
				}
				catch
				{
					if (this.Client != null)
					{
						this.Client.Dispose();
						this.Client = null;
					}
					throw;
				}
			}
		}

		public byte[] Get(string path)
		{
			return this.Perform(() => this.Client.Get(path));
		}

		public int Post(string path, byte[] data)
		{
			return this.Perform(() => this.Client.Post(path, data));
		}

		public byte[] GetPost(string path, byte[] data)
		{
			return this.Perform(() => this.Client.GetPost(path, data));
		}

		public string[] List(string path)
		{
			return this.Perform(() => this.Client.List(path));
		}

		public int Delete(string path)
		{
			return this.Perform(() => this.Client.Delete(path));
		}

		public void Dispose()
		{
			if (this.Th != null)
			{
				this.EvStop.Set();

				this.Th.Join();
				this.Th = null;

				this.EvStop.Dispose();

				if (this.Client != null)
					this.Client.Dispose();
			}
		}
	}
}
