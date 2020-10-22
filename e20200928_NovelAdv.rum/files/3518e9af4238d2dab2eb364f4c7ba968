using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class LimitedReader
	{
		private long Remaining;
		private FileTools.Read_d AnotherReader;

		public LimitedReader(long limit, FileTools.Read_d reader)
		{
			this.Remaining = limit;
			this.AnotherReader = reader;
		}

		public int Read(byte[] buff, int offset, int count)
		{
			if (this.Remaining <= 0L)
				return -1;

			if (this.Remaining < (long)count)
				count = (int)this.Remaining;

			int readSize = this.AnotherReader(buff, offset, count);

			if (readSize < 0)
				this.Remaining = 0L;
			else
				this.Remaining -= (long)readSize;

			return readSize;
		}
	}
}
