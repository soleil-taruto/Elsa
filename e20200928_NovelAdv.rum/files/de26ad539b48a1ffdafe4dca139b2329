using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FilingCase3Client : IDisposable
	{
		private SockClient Client;
		private string BasePath;

		public FilingCase3Client(string domain = "localhost", int portNo = 65123, string basePath = "Common")
		{
			this.BasePath = basePath;
			this.Connect(domain, portNo);
		}

		private void Connect(string domain, int portNo)
		{
			for (int c = 1; ; c++)
			{
				if (this.TryConnect(domain, portNo))
					break;

				if (3 <= c)
					throw new Exception("接続エラー");

				Thread.Sleep(5000);
			}
		}

		private bool TryConnect(string domain, int portNo)
		{
			try
			{
				this.Client = new SockClient();
				this.Client.Connect(domain, portNo, 5000);

				this.Hello();

				return true;
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}

			if (this.Client != null)
			{
				this.Client.Dispose();
				this.Client = null;
			}
			return false;
		}

		public byte[] Get(string path)
		{
			this.Send("GET", path);
			byte[] ret = this.Read64();
			this.ReadLineCheck("/GET/e");
			return ret;
		}

		public int Post(string path, byte[] data)
		{
			this.Send("POST", path, data);
			this.ReadLineCheck("/POST/e");
			return 1;
		}

		public byte[] GetPost(string path, byte[] data)
		{
			this.Send("GET-POST", path, data);
			byte[] ret = this.Read64();
			this.ReadLineCheck("/GET/e");
			this.ReadLineCheck("/GET-POST/e");
			return ret;
		}

		public string[] List(string path)
		{
			this.Send("LIST", path);

			{
				List<string> dest = new List<string>();

				for (; ; )
				{
					string line = this.ReadLine();

					if (line == "/LIST/e")
						break;

					dest.Add(line);
				}
				return dest.ToArray();
			}
		}

		public int Delete(string path)
		{
			this.Send("DELETE", path);
			this.ReadLineCheck("/DELETE/e");
			return 1;
		}

		public int Hello()
		{
			this.Client.IdleTimeoutMillis = 5000;

			this.Send("HELLO", "$");
			this.ReadLineCheck("/HELLO/e");

			this.Client.IdleTimeoutMillis = -1; // タイムアウトはサーバー側に任せる。

			return 1;
		}

		private void Send(string command, string path)
		{
			this.Send(command, path, new byte[0]);
		}

		private void Send(string command, string path, byte[] data)
		{
			this.WriteLine(command);
			this.WriteLine(Path.Combine(this.BasePath, path));
			this.WriteLine("" + data.Length);
			this.Client.Send(data);
			this.WriteLine("/SEND/e");
		}

		private void WriteLine(string line)
		{
			this.Client.Send(StringTools.ENCODING_SJIS.GetBytes(line + "\r\n"));
		}

		private void ReadLineCheck(string line)
		{
			if (this.ReadLine() != line)
			{
				throw new Exception("受信データが間違っています。" + line);
			}
		}

		private string ReadLine()
		{
			return StringTools.ENCODING_SJIS.GetString(this.Read());
		}

		private byte[] Read()
		{
			return this.Read(ToInt(this.Read(4)));
		}

		private byte[] Read64()
		{
			return this.Read(ToInt(this.Read(8)));
		}

		private byte[] Read(int size)
		{
			return this.Client.Recv(size);
		}

		private static int ToInt(byte[] src)
		{
			return
				((int)src[0] << 0) |
				((int)src[1] << 8) |
				((int)src[2] << 16) |
				((int)src[3] << 24);
		}

		/// <summary>
		/// このメソッドは例外を投げないこと。
		/// </summary>
		public void Dispose()
		{
			if (this.Client != null)
			{
				this.Client.Dispose();
				this.Client = null;
			}
		}
	}
}
