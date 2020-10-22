using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CacheMap<K, V>
	{
		// see also:
		// DictionaryTools.CreateCache
		// DictionaryTools.CreateCacheIgnoreCase

		private Dictionary<K, V> Inner;
		private Func<K, V> CreateValue;

		public CacheMap(Dictionary<K, V> inner, Func<K, V> createValue)
		{
			this.Inner = inner;
			this.CreateValue = createValue;
		}

		public V this[K key]
		{
			get
			{
				if (this.Inner.ContainsKey(key) == false)
				{
					V value = this.CreateValue(key);
					this.Inner[key] = value;
					return value;
				}
				return this.Inner[key];
			}
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public void Clear()
		{
			this.Inner.Clear();
		}

		public void Remove(K key)
		{
			this.Inner.Remove(key);
		}

		public IEnumerable<K> Keys
		{
			get
			{
				return this.Inner.Keys;
			}
		}

		public IEnumerable<V> Values
		{
			get
			{
				return this.Inner.Values;
			}
		}
	}
}
