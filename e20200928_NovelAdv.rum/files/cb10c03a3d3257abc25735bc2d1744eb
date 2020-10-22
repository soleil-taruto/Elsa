using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncQueue<T>
	{
		private object SYNCROOT = new object();
		private Queue<T> Inner = new Queue<T>();

		public void Enqueue(T element)
		{
			lock (SYNCROOT)
			{
				this.Inner.Enqueue(element);
			}
		}

		public T[] Dequeue(int count = 1)
		{
			List<T> dest = new List<T>();

			lock (SYNCROOT)
			{
				while (dest.Count < count && 1 <= this.Inner.Count)
					dest.Add(this.Inner.Dequeue());
			}
			return dest.ToArray();
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

		public void Rotate(Predicate<T> match, int count = 1)
		{
			foreach (T element in this.Dequeue(count))
				if (match(element))
					this.Enqueue(element);
		}

		public void Invoke(Action<Queue<T>> routine)
		{
			lock (SYNCROOT)
			{
				routine(this.Inner);
			}
		}
	}
}
