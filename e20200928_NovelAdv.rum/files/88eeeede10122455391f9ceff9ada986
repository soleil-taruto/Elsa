using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncList<T>
	{
		private object SYNCROOT = new object();
		private List<T> Inner = new List<T>();

		public void Clear()
		{
			lock (SYNCROOT)
			{
				this.Inner.Clear();
			}
		}

		public void Add(T value)
		{
			lock (SYNCROOT)
			{
				this.Inner.Add(value);
			}
		}

		public void AddRange(IEnumerable<T> values)
		{
			lock (SYNCROOT)
			{
				this.Inner.AddRange(values);
			}
		}

		public void RemoveAt(int index)
		{
			lock (SYNCROOT)
			{
				this.Inner.RemoveAt(index);
			}
		}

		public void RemoveAll(Predicate<T> match)
		{
			lock (SYNCROOT)
			{
#if true
				this.Inner.RemoveAll(match);
#else
				int count = 0;

				for (int index = 0; index < this.Inner.Count; index++)
					if (match(this.Inner[index]) == false)
						this.Inner[count++] = this.Inner[index];

				this.Inner.RemoveRange(count, this.Inner.Count - count);
#endif
			}
		}

		public T this[int index]
		{
			get
			{
				lock (SYNCROOT)
				{
					return this.Inner[index];
				}
			}

			set
			{
				lock (this.SYNCROOT)
				{
					this.Inner[index] = value;
				}
			}
		}

		public T GetPost(int index, T value)
		{
			lock (SYNCROOT)
			{
				T ret = this.Inner[index];
				this.Inner[index] = value;
				return ret;
			}
		}

		public int Count
		{
			get
			{
				lock (SYNCROOT)
				{
					return this.Inner.Count;
				}
			}
		}

		public T[] ToArray()
		{
			lock (SYNCROOT)
			{
				return this.Inner.ToArray();
			}
		}

		public void Invoke(Action<List<T>> routine)
		{
			lock (SYNCROOT)
			{
				routine(this.Inner);
			}
		}
	}
}
