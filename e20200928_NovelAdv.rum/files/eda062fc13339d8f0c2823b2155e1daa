using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class LimitedWriter
	{
		private long Remaining;
		private FileTools.Write_d AnotherWriter;

		public LimitedWriter(long limit, FileTools.Write_d writer)
		{
			this.Remaining = limit;
			this.AnotherWriter = writer;
		}

		public void Write(byte[] buff, int offset, int count)
		{
			if (this.Remaining < (long)count)
				throw new Exception("Over the limit !!!");

			this.Remaining -= (long)count;
			this.AnotherWriter(buff, offset, count);
		}
	}
}
