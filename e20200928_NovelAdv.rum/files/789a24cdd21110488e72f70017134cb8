using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AutoList<T>
	{
		private T[] Buffer;

		/// <summary>
		/// 長さ、但し実際の長さとは関係無い。呼び出し側で管理すること。
		/// </summary>
		public int Count;

		public AutoList(int capacity = 16)
		{
			if (capacity < 0)
				throw new ArgumentException();

			this.Buffer = new T[capacity];
			this.Count = capacity;
		}

		public void EnsureCapacity(int capacity)
		{
			if (this.Buffer.Length < capacity)
			{
				T[] tmp = new T[capacity];

				Array.Copy(this.Buffer, tmp, this.Buffer.Length);

				this.Buffer = tmp;
			}
		}

		public int Capacity
		{
			get
			{
				return this.Buffer.Length;
			}
		}

		public T this[int index]
		{
			get
			{
#if true
				return index < this.Buffer.Length ? this.Buffer[index] : default(T);
#else
				this.EnsureCapacity(index + 1);
				return this.Buffer[index];
#endif
			}

			set
			{
				this.EnsureCapacity(index + 1);
				this.Buffer[index] = value;
			}
		}
	}
}
