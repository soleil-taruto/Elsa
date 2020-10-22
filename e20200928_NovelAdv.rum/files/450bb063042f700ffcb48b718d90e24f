using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class BitList
	{
		private AutoList<uint> Buffer;

		public BitList(long capacity = 0)
		{
			this.Buffer = new AutoList<uint>((int)((capacity + 31L) >> 5));
		}

		public void EnsureCapacity(long capacity)
		{
			this.Buffer.EnsureCapacity((int)((capacity + 31L) >> 5));
		}

		public bool this[long index]
		{
			get
			{
				return this.GetBit(index);
			}

			set
			{
				this.SetBit(index, value);
			}
		}

		public bool GetBit(long index)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31L);

			return ((this.Buffer[i] >> b) & 1u) == 1u;
		}

		public void SetBit(long index, bool value)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31);

			uint c = this.Buffer[i];

			if (value)
				c |= 1u << b;
			else
				c &= ~(1u << b);

			this.Buffer[i] = c;
		}

		public void SetBits(long index, long size, bool value)
		{
			long bgn = index;
			long end = index + size;

			while (bgn < end && (bgn & 31L) != 0L)
			{
				this.SetBit(bgn, value);
				bgn++;
			}
			while (bgn < end && (end & 31L) != 0L)
			{
				end--;
				this.SetBit(end, value);
			}
			if (bgn < end)
			{
				int ib = (int)(bgn >> 5);
				int ie = (int)(end >> 5);

				uint c = value ? 0xffffffffu : 0u;

				for (int i = ib; ib < ie; i++)
					this.Buffer[i] = c;
			}
		}

		public void InvBit(long index)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31);

			uint c = this.Buffer[i];

			c ^= 1u << b;

			this.Buffer[i] = c;
		}

		public void InvBits(long index, long size)
		{
			long bgn = index;
			long end = index + size;

			while (bgn < end && (bgn & 31L) != 0L)
			{
				this.InvBit(bgn);
				bgn++;
			}
			while (bgn < end && (end & 31L) != 0L)
			{
				end--;
				this.InvBit(end);
			}
			if (bgn < end)
			{
				int ib = (int)(bgn >> 5);
				int ie = (int)(end >> 5);

				for (int i = ib; ib < ie; i++)
					this.Buffer[i] ^= 0xffffffffu;
			}
		}
	}
}
