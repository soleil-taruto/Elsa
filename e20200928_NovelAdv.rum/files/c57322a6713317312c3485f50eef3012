using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class TreeSet<T>
	{
		private Dictionary<T, object> Inner;

		public TreeSet(IEqualityComparer<T> comp)
		{
			this.Inner = new Dictionary<T, object>(comp);
		}

		public bool Add(T element)
		{
			if (this.Inner.ContainsKey(element) == false)
			{
				this.Inner.Add(element, null);
				return true;
			}
			return false;
		}

		public bool Contains(T element)
		{
			return this.Inner.ContainsKey(element);
		}

		public void Remove(T element)
		{
			this.Inner.Remove(element);
		}

		public void Clear()
		{
			this.Inner.Clear();
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}
	}
}
