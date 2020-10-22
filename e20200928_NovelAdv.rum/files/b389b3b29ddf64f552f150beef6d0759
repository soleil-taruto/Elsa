using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class OrderedMap<K, V>
	{
		// see also:
		// DictionaryTools.CreateOrdered
		// DictionaryTools.CreateOrderedIgnoreCase

		private class ValueInfo
		{
			public V Value;
			public long Index;
			public K Key;
		}

		private Dictionary<K, ValueInfo> Inner;
		private long Counter = 0L;

		public OrderedMap(IEqualityComparer<K> comp)
		{
			this.Inner = new Dictionary<K, ValueInfo>(comp);
		}

		public void Add(Dictionary<K, V> dict)
		{
			foreach (KeyValuePair<K, V> pair in dict)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		public void Add(K key, V value)
		{
			this.Inner.Add(key, new ValueInfo()
			{
				Value = value,
				Index = this.Counter++,
				Key = key,
			});
		}

		public void Remove(K key)
		{
			this.Inner.Remove(key);
		}

		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		public V this[K key]
		{
			get
			{
				return this.Inner[key].Value;
			}

			set
			{
				if (this.Inner.ContainsKey(key))
					this.Inner[key].Value = value;
				else
					this.Add(key, value);
			}
		}

		public IEnumerable<K> GetKeySet()
		{
			return this.Inner.Keys;
		}

		public IEnumerable<KeyValuePair<K, V>> GetEntrySet()
		{
			foreach (ValueInfo info in this.Inner.Values)
			{
				yield return new KeyValuePair<K, V>(info.Key, info.Value);
			}
		}

		public IEnumerable<V> GetValueSet()
		{
			foreach (ValueInfo info in this.Inner.Values)
			{
				yield return info.Value;
			}
		}

		private List<ValueInfo> GetInfos()
		{
			List<ValueInfo> infos = new List<ValueInfo>(this.Inner.Values);

			infos.Sort((a, b) => LongTools.Comp(a.Index, b.Index));

			return infos;
		}

		public IEnumerable<K> GetKeys()
		{
			return GetInfos().Select(info => info.Key);
		}

		public IEnumerable<KeyValuePair<K, V>> GetEntries()
		{
			return GetInfos().Select(info => new KeyValuePair<K, V>(info.Key, info.Value));
		}

		public IEnumerable<V> GetValues()
		{
			return GetInfos().Select(info => info.Value);
		}
	}
}
