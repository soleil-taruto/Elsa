using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	/// <summary>
	/// 擬似乱数列
	/// </summary>
	public class DDRandom
	{
		private uint X;
		private uint Y;
		private uint Z;
		private uint A;

		public DDRandom()
			: this(SCommon.CRandom.GetUInt(), SCommon.CRandom.GetUInt(), SCommon.CRandom.GetUInt(), SCommon.CRandom.GetUInt())
		{ }

		public DDRandom(uint x, uint y, uint z, uint a)
		{
			if ((x | y | z | a) == 0u)
				x = 1u;

			this.X = x;
			this.Y = y;
			this.Z = z;
			this.A = a;
		}

		/// <summary>
		/// [0,2^32)
		/// </summary>
		/// <returns>乱数</returns>
		public uint Next()
		{
			// Xorshift-128

			uint t;

			t = this.X;
			t ^= this.X << 11;
			t ^= t >> 8;
			t ^= this.A;
			t ^= this.A >> 19;
			this.X = this.Y;
			this.Y = this.Z;
			this.Z = this.A;
			this.A = t;

			return t;
		}

		/// <summary>
		/// [0,1]
		/// </summary>
		/// <returns>乱数</returns>
		public double Real()
		{
			return this.Next() / (double)uint.MaxValue;
		}

		/// <summary>
		/// [0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double Real2()
		{
			return this.Next() / (double)(uint.MaxValue + 1L);
		}

		/// <summary>
		/// (0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double Real3()
		{
			return this.Next() / (double)(uint.MaxValue + 1L) + 0.5;
		}

		public uint GetUInt(uint modulo)
		{
			if (modulo < 1u)
				throw new ArgumentException("Bad modulo: " + modulo);

			return this.Next() % modulo;
		}

		public int GetInt(int modulo)
		{
			return (int)this.GetUInt((uint)modulo);
		}

		public void Shffle<T>(T[] arr)
		{
			for (int index = arr.Length; 2 <= index; index--)
				SCommon.Swap(arr, this.GetInt(index), index - 1);
		}
	}
}
