using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FileCommunicator : IDisposable
	{
		private string Ident;
		private Mutex Mutex;
		private NamedEventUnit EvSent;
		private string MessageDir;
		private string R_IndexFile;
		private string W_IndexFile;

		public FileCommunicator(string ident)
		{
			this.Ident = SecurityTools.ToFiarIdent(ident);
			this.Mutex = MutexTools.Create(this.Ident + "_M");
			this.EvSent = new NamedEventUnit(this.Ident + "_ES");
			this.MessageDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), this.Ident);
			this.R_IndexFile = Path.Combine(this.MessageDir, "_r");
			this.W_IndexFile = Path.Combine(this.MessageDir, "_w");
		}

		public void Dispose()
		{
			if (this.Mutex != null)
			{
				this.Mutex.Dispose();
				this.Mutex = null;
			}
		}

		public void Clear()
		{
			using (new MSection(this.Mutex))
			{
				FileTools.Delete(this.MessageDir);
			}
		}

		public void Send(byte[] message)
		{
			if (message == null)
				throw new ArgumentException("message == null");

			this.TransferMain(message, true);
			this.EvSent.Set();
		}

		public byte[] Recv(int millis) // ret: null == メッセージ無し
		{
			byte[] message = this.Recv();

			if (message == null && this.EvSent.WaitForMillis(millis))
				message = this.Recv();

			return message;
		}

		public byte[] Recv() // ret: null == メッセージ無し
		{
			return this.TransferMain(null, false);
		}

		private byte[] TransferMain(byte[] message, bool sendFlag)
		{
			using (new MSection(this.Mutex))
			{
				long rIndex;
				long wIndex;

				// Index 読み込み
				{
					if (Directory.Exists(this.MessageDir))
					{
						rIndex = long.Parse(File.ReadAllText(this.R_IndexFile));
						wIndex = long.Parse(File.ReadAllText(this.W_IndexFile));

						if (rIndex < 0L)
							throw null;

						if (wIndex <= rIndex) // ? 不正な Index || メッセージ無し
							throw null;

						if (LongTools.IMAX_64 < wIndex) // カンスト, HACK: 不要か
							throw null;
					}
					else
					{
						FileTools.CreateDir(this.MessageDir);

						rIndex = 0L;
						wIndex = 0L;
					}
				}

				// message 読み書き
				{
					if (sendFlag) // ? 送信
					{
						if (message == null)
							throw null;

						File.WriteAllBytes(Path.Combine(this.MessageDir, wIndex.ToString()), message);
						wIndex++;

						if (LongTools.IMAX_64 < wIndex) // カンスト, HACK: 不要か
							throw null;

						message = null; // 何も返さない。
					}
					else // ? 受信
					{
						if (message != null)
							throw null;

						if (rIndex < wIndex)
						{
							string file = Path.Combine(this.MessageDir, rIndex.ToString());

							message = File.ReadAllBytes(file);
							FileTools.Delete(file);

							rIndex++;
						}
						else
							message = null; // メッセージ無し
					}
				}

				// Index 書き出し
				{
					if (rIndex < wIndex)
					{
						File.WriteAllText(this.R_IndexFile, rIndex.ToString(), Encoding.ASCII);
						File.WriteAllText(this.W_IndexFile, wIndex.ToString(), Encoding.ASCII);
					}
					else // ? メッセージ無し
					{
						if (wIndex < rIndex) // ? 不正な Index
							throw null;

						FileTools.Delete(this.MessageDir);
					}
				}
			}
			return message;
		}
	}
}
