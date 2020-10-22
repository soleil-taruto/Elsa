using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class ArrayTools
	{
		public static int Comp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return IntTools.Comp(a.Length, b.Length);
		}

		public static void Swap<T>(T[] arr, int a, int b)
		{
			T tmp = arr[a];
			arr[a] = arr[b];
			arr[b] = tmp;
		}

		public static int IndexOf<T>(T[] arr, T target, Comparison<T> comp, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (comp(arr[index], target) == 0)
					return index;

			return defval;
		}

		public static int IndexOf<T>(T[] arr, Predicate<T> match, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (match(arr[index]))
					return index;

			return defval;
		}

		public static int LastIndexOf<T>(T[] arr, T target, Comparison<T> comp, int defval = -1)
		{
			for (int index = arr.Length - 1; 0 <= index; index--)
				if (comp(arr[index], target) == 0)
					return index;

			return defval;
		}

		public static int LastIndexOf<T>(T[] arr, Predicate<T> match, int defval = -1)
		{
			for (int index = arr.Length - 1; 0 <= index; index--)
				if (match(arr[index]))
					return index;

			return defval;
		}

		public static bool Contains<T>(T[] arr, T target, Comparison<T> comp)
		{
			return IndexOf<T>(arr, target, comp) != -1;
		}

		public static bool Contains<T>(T[] arr, Predicate<T> match)
		{
			return IndexOf<T>(arr, match) != -1;
		}

		// memo: Enumerable の実装 --https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs
		// メモリ使用量テスト --> CSharp/wb/t20200804

		public static List<T> ToList<T>(IEnumerable<T> src)
		{
#if true
			int count = 0;

			using (IEnumerator<T> reader = src.GetEnumerator())
				while (reader.MoveNext())
					count++;

			List<T> dest = new List<T>(count);

			using (IEnumerator<T> reader = src.GetEnumerator())
			{
				for (int index = 0; index < count; index++)
				{
					if (reader.MoveNext() == false)
						throw new Exception(string.Format("2回目の列挙で要素が減りました。(count, index: {0}, {1})", count, index));

					dest.Add(reader.Current);
				}
				if (reader.MoveNext())
					throw new Exception(string.Format("2回目の列挙で要素が増えました。(count: {0})", count));
			}
			return dest;
#elif true // old
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
#else // almost-cretain same code
			return src.ToList();
#endif
		}

		public static T[] ToArray<T>(IEnumerable<T> src)
		{
#if true
			int count = 0;

			using (IEnumerator<T> reader = src.GetEnumerator())
				while (reader.MoveNext())
					count++;

			T[] dest = new T[count];

			using (IEnumerator<T> reader = src.GetEnumerator())
			{
				for (int index = 0; index < count; index++)
				{
					if (reader.MoveNext() == false)
						throw new Exception(string.Format("2回目の列挙で要素が減りました。(count, index: {0}, {1})", count, index));

					dest[index] = reader.Current;
				}
				if (reader.MoveNext())
					throw new Exception(string.Format("2回目の列挙で要素が増えました。(count: {0})", count));
			}
			return dest;
#elif true // old
			List<T> list = ToList(src);
			T[] dest = new T[list.Count];

			for (int index = 0; index < list.Count; index++)
				dest[index] = list[index];

			return dest;
#else // almost-cretain same code
			return src.ToArray();
#endif
		}

		public static T[] Repeat<T>(T element, int count)
		{
			T[] dest = new T[count];

			for (int index = 0; index < count; index++)
				dest[index] = element;

			return dest;
		}

		/// <summary>
		/// マージ
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="arr1">入力1</param>
		/// <param name="arr2">入力2</param>
		/// <param name="destOnly1">入力1だけに存在する要素をここへ追加する。null == 何もしない。</param>
		/// <param name="destBoth1">両方に存在する入力1の要素をここへ追加する。null == 何もしない。</param>
		/// <param name="destBoth2">両方に存在する入力2の要素をここへ追加する。null == 何もしない。</param>
		/// <param name="destOnly2">入力2だけに存在する要素をここへ追加する。null == 何もしない。</param>
		/// <param name="comp">要素の比較</param>
		public static void Merge<T>(T[] arr1, T[] arr2, List<T> destOnly1, List<T> destBoth1, List<T> destBoth2, List<T> destOnly2, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			Merge_NoSort(arr1, arr2, destOnly1, destBoth1, destBoth2, destOnly2, comp);
		}

		public static void Merge_NoSort<T>(T[] arr1, T[] arr2, List<T> destOnly1, List<T> destBoth1, List<T> destBoth2, List<T> destOnly2, Comparison<T> comp)
		{
			int index1 = 0;
			int index2 = 0;

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					if (destOnly1 != null)
						destOnly1.Add(arr1[index1]);

					index1++;
				}
				else if (0 < ret)
				{
					if (destOnly2 != null)
						destOnly2.Add(arr2[index2]);

					index2++;
				}
				else
				{
					if (destBoth1 != null)
						destBoth1.Add(arr1[index1]);

					if (destBoth2 != null)
						destBoth2.Add(arr2[index2]);

					index1++;
					index2++;
				}
			}
		}

		public static T[][] GetMergedPairs<T>(T[] arr1, T[] arr2, T defval, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			return GetMergedPairs_NoSort(arr1, arr2, defval, comp);
		}

		public static T[][] GetMergedPairs_NoSort<T>(T[] arr1, T[] arr2, T defval, Comparison<T> comp)
		{
			int index1 = 0;
			int index2 = 0;

			List<T[]> dest = new List<T[]>();

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					dest.Add(new T[] { arr1[index1++], defval });
				}
				else if (0 < ret)
				{
					dest.Add(new T[] { defval, arr2[index2++] });
				}
				else
				{
					dest.Add(new T[] { arr1[index1++], arr2[index2++] });
				}
			}
			return dest.ToArray();
		}

		public static IEnumerable<T> Distinct<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			using (IEnumerator<T> reader = src.GetEnumerator())
			{
				if (reader.MoveNext())
				{
					T lastElement = reader.Current;

					yield return lastElement;

					while (reader.MoveNext())
					{
						T element = reader.Current;

						if (comp(element, lastElement) != 0)
						{
							yield return element;

							lastElement = element;
						}
					}
				}
			}
		}

		public static T Lightest<T>(IEnumerable<T> src, Func<T, double> toWeight)
		{
			using (IEnumerator<T> reader = src.GetEnumerator())
			{
				if (reader.MoveNext() == false)
					throw new ArgumentException("空のリストです。");

				T ret = reader.Current;
				double ret_weight = toWeight(ret);

				while (reader.MoveNext())
				{
					T value = reader.Current;
					double weight = toWeight(value);

					if (weight < ret_weight)
					{
						ret = value;
						ret_weight = weight;
					}
				}
				return ret;
			}
		}

		public static T Heaviest<T>(IEnumerable<T> src, Func<T, double> toWeight)
		{
			return Lightest(src, value => toWeight(value) * -1);
		}

		public static T Smallest<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			using (IEnumerator<T> reader = src.GetEnumerator())
			{
				if (reader.MoveNext() == false)
					throw new ArgumentException("空のリストです。");

				T ret = reader.Current;

				while (reader.MoveNext())
				{
					T value = reader.Current;

					if (comp(value, ret) < 0)
					{
						ret = value;
					}
				}
				return ret;
			}
		}

		public static T Largest<T>(IEnumerable<T> src, Comparison<T> comp)
		{
#if true
			return Smallest(src, (a, b) => comp(b, a));
#else // old same
			return Smallest(src, (a, b) => comp(a, b) * -1);
#endif
		}
	}
}
