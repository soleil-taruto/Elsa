using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	public class RandomUnit : IDisposable
	{
		public interface IRandomNumberGenerator : IDisposable
		{
			byte[] GetBlock();
		}

		private IRandomNumberGenerator Rng;

		public RandomUnit(IRandomNumberGenerator rng)
		{
			this.Rng = rng;
		}

		public void Dispose()
		{
			if (this.Rng != null)
			{
				this.Rng.Dispose();
				this.Rng = null;
			}
		}

		private byte[] Cache = BinTools.EMPTY;
		private int RIndex = 0;

		public byte GetByte()
		{
			if (this.Cache.Length <= this.RIndex)
			{
				this.Cache = this.Rng.GetBlock();
				this.RIndex = 0;
			}
			return this.Cache[this.RIndex++];
		}

		public byte[] GetBytes(int length)
		{
			byte[] dest = new byte[length];

			for (int index = 0; index < length; index++)
				dest[index] = this.GetByte();

			return dest;
		}

		public uint GetUInt16()
		{
			byte[] r = GetBytes(2);

			return
				((uint)r[0] << 0) |
				((uint)r[1] << 8);
		}

		public uint GetUInt24()
		{
			byte[] r = GetBytes(3);

			return
				((uint)r[0] << 0) |
				((uint)r[1] << 8) |
				((uint)r[2] << 16);
		}

		public uint GetUInt()
		{
			byte[] r = GetBytes(4);

			return
				((uint)r[0] << 0) |
				((uint)r[1] << 8) |
				((uint)r[2] << 16) |
				((uint)r[3] << 24);
		}

		public ulong GetUInt64()
		{
			byte[] r = GetBytes(8);

			return
				((ulong)r[0] << 0) |
				((ulong)r[1] << 8) |
				((ulong)r[2] << 16) |
				((ulong)r[3] << 24) |
				((ulong)r[4] << 32) |
				((ulong)r[5] << 40) |
				((ulong)r[6] << 48) |
				((ulong)r[7] << 56);
		}

		public ulong GetRandom64(ulong modulo)
		{
			if (modulo == 0UL)
				throw new ArgumentOutOfRangeException("modulo == 0");

			ulong r_mod = (ulong.MaxValue % modulo + 1UL) % modulo;
			ulong r;

			do
			{
				r = this.GetUInt64();
			}
			while (r < r_mod);

			r %= modulo;

			return r;
		}

		public uint GetRandom(uint modulo)
		{
			return (uint)this.GetRandom64((ulong)modulo);
		}

		public long GetRange64(long minval, long maxval)
		{
			return (long)this.GetRandom64((ulong)(maxval + 1L - minval)) + minval;
		}

		public int GetRange(int minval, int maxval)
		{
			return (int)this.GetRandom((uint)(maxval + 1 - minval)) + minval;
		}

		public long GetInt64(long modulo)
		{
			return (long)this.GetRandom64((ulong)modulo);
		}

		public int GetInt(int modulo)
		{
			return (int)this.GetRandom((uint)modulo);
		}

		/// <summary>
		/// [0,1]
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal()
		{
			return this.GetUInt() / (double)uint.MaxValue;
		}

		/// <summary>
		/// [0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal2()
		{
			return this.GetUInt() / (double)(uint.MaxValue + 1L);
		}

		/// <summary>
		/// (0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal3()
		{
			return this.GetUInt() / (double)(uint.MaxValue + 1L) + 0.5;
		}

		public void Shuffle<T>(T[] arr)
		{
			for (int index = arr.Length; 1 < index; index--)
			{
				ArrayTools.Swap(arr, GetInt(index), index - 1);
			}
		}

		public T ChooseOne<T>(T[] arr)
		{
			return arr[GetInt(arr.Length)];
		}

		public T[] ChooseSome<T>(T[] arr, int count)
		{
			List<T> src = new List<T>(arr);
			T[] dest = new T[count];

			for (int index = 0; index < count; index++)
				dest[index] = ExtraTools.FastDesertElement(src, GetInt(src.Count));

			return dest;
		}
	}
}
