using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class DictionaryTools
	{
		public static Dictionary<string, V> Create<V>()
		{
			return new Dictionary<string, V>(new StringTools.IEComp());
		}

		public static Dictionary<string, V> CreateIgnoreCase<V>()
		{
			return new Dictionary<string, V>(new StringTools.IECompIgnoreCase());
		}

		public static OrderedMap<string, V> CreateOrdered<V>()
		{
			return new OrderedMap<string, V>(new StringTools.IEComp());
		}

		public static OrderedMap<string, V> CreateOrderedIgnoreCase<V>()
		{
			return new OrderedMap<string, V>(new StringTools.IECompIgnoreCase());
		}

		public static TreeSet<string> CreateSet()
		{
			return new TreeSet<string>(new StringTools.IEComp());
		}

		public static TreeSet<string> CreateSetIgnoreCase()
		{
			return new TreeSet<string>(new StringTools.IECompIgnoreCase());
		}

		public static CacheMap<string, V> CreateCache<V>(Func<string, V> createValue)
		{
			return new CacheMap<string, V>(Create<V>(), createValue);
		}

		public static CacheMap<string, V> CreateCacheIgnoreCase<V>(Func<string, V> createValue)
		{
			return new CacheMap<string, V>(CreateIgnoreCase<V>(), createValue);
		}
	}
}
