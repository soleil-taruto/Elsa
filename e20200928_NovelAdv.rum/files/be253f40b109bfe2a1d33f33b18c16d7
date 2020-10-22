using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class SharedQueue : IDisposable
	{
		private string QueueFile;
		private Mutex MtxHdl;
		private NamedEventUnit EnqueueEv;

		public SharedQueue(string ident)
		{
			ident = SecurityTools.ToFiarIdent(ident);

			this.QueueFile = Path.Combine(Environment.GetEnvironmentVariable("TMP"), ident + ".tmp");
			this.MtxHdl = MutexTools.CreateGlobal(ident + "_M");
			this.EnqueueEv = new NamedEventUnit(NamedEventTools.CreateGlobal(ident + "_E"), true);
		}

		public void Clear()
		{
			using (new MSection(this.MtxHdl))
			{
				FileTools.Delete(this.QueueFile);
			}
		}

		public void Enqueue(byte[] src)
		{
			Enqueue(new byte[][] { src });
		}

		public void Enqueue(IEnumerable<byte[]> src)
		{
			using (new MSection(this.MtxHdl))
			{
				using (FileStream writer = new FileStream(this.QueueFile, FileMode.Append, FileAccess.Write))
				{
					foreach (byte[] value in src)
					{
						FileTools.Write(writer, BinTools.ToBytes(value.Length));
						FileTools.Write(writer, value);
					}
				}
			}
			this.EnqueueEv.Set();
		}

		public byte[][] DequeueAll()
		{
			List<byte[]> dest = new List<byte[]>();
			DequeueAll(value => dest.Add(value));
			return dest.ToArray();
		}

		public int DequeueWaitMillis = 2000;

		public int DequeueAll(Action<byte[]> rtn)
		{
			int count = DequeueAll_NoWait(rtn);

			if (count == 0)
			{
				this.EnqueueEv.WaitForMillis(this.DequeueWaitMillis);
				count = DequeueAll_NoWait(rtn);
			}
			return count;
		}

		private int DequeueAll_NoWait(Action<byte[]> rtn)
		{
			int count = 0;

			using (new MSection(this.MtxHdl))
			{
				if (File.Exists(this.QueueFile))
				{
					try
					{
						using (FileStream reader = new FileStream(this.QueueFile, FileMode.Open, FileAccess.Read))
						{
							for (; ; )
							{
								byte[] bSize = new byte[4];
								int readSize = reader.Read(bSize, 0, 4);

								if (readSize == 0)
									break;

								if (readSize != 4)
									throw new Exception("不正なサイズの読み込みサイズ：" + readSize);

								int size = BinTools.ToInt(bSize);

								if (size < 0 || IntTools.IMAX < size)
									throw new Exception("不正なサイズ：" + size);

								byte[] value = new byte[size];
								readSize = reader.Read(value, 0, size);

								if (readSize != size)
									throw new Exception("不正なデータの読み込みサイズ：" + readSize + ", " + size);

								rtn(value);
								count++;
							}
						}
					}
					finally
					{
						FileTools.Delete(this.QueueFile); // エラーでも全部読み込めた時も削除する。
					}
				}
			}
			return count;
		}

		public void Dispose()
		{
			if (this.MtxHdl != null)
			{
				this.EnqueueEv.Dispose();

				this.MtxHdl.Dispose();
				this.MtxHdl = null;
			}
		}
	}
}
