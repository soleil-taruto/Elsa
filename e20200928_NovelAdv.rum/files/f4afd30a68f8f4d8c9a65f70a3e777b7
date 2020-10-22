using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectList
	{
		public static ObjectList Create(IEnumerable<object> src)
		{
			ObjectList dest = new ObjectList();
			dest.AddRange(src);
			return dest;
		}

		private List<object> Inner = new List<object>();

		public ObjectList()
		{ }

		public ObjectList(params object[] arr)
		{
			this.AddRange(arr);
		}

		public void AddRange(IEnumerable<object> src)
		{
			this.Inner.AddRange(src);
		}

		public void Add(object element)
		{
			this.Inner.Add(element);
		}

		public int Count
		{
			get { return this.Inner.Count; }
		}

		public object this[int index]
		{
			get { return this.Inner[index]; }
		}

		public List<object> Direct()
		{
			return this.Inner;
		}
	}
}
