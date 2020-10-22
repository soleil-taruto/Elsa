using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ObjectMap
	{
		private OrderedMap<string, object> Inner;

		public static ObjectMap Create()
		{
			return new ObjectMap()
			{
				Inner = DictionaryTools.CreateOrdered<object>(),
			};
		}

		public static ObjectMap CreateIgnoreCase()
		{
			return new ObjectMap()
			{
				Inner = DictionaryTools.CreateOrderedIgnoreCase<object>(),
			};
		}

		private ObjectMap()
		{ }

		public void Add(Dictionary<object, object> dict)
		{
			foreach (KeyValuePair<object, object> pair in dict)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Add(object key, object value)
		{
			this.Inner.Add("" + key, value);
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public object this[object key]
		{
			get
			{
				return this.Inner["" + key];
			}

			set
			{
				this.Inner["" + key] = value;
			}
		}

		public IEnumerable<string> GetKeySet()
		{
			return this.Inner.GetKeySet();
		}

		public IEnumerable<KeyValuePair<string, object>> GetEntrySet()
		{
			return this.Inner.GetEntrySet();
		}

		public IEnumerable<object> GetValueSet()
		{
			return this.Inner.GetValueSet();
		}

		public IEnumerable<string> GetKeys()
		{
			return this.Inner.GetKeys();
		}

		public IEnumerable<KeyValuePair<string, object>> GetEntries()
		{
			return this.Inner.GetEntries();
		}

		public IEnumerable<object> GetValues()
		{
			return this.Inner.GetValues();
		}

		public OrderedMap<string, object> Direct()
		{
			return this.Inner;
		}
	}
}
