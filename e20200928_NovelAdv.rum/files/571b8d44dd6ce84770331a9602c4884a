using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class EnumerableTools
	{
		public static IEnumerable<T> Iterate<T>(Func<IEnumerator<T>> src)
		{
			using (IEnumerator<T> reader = src())
				while (reader.MoveNext())
					yield return reader.Current;
		}

		/// <summary>
		/// <para>列挙の引数配列(2次元列挙)を列挙(1次元列挙)に変換する。</para>
		/// <para>例：{{ A, B, C }, { D, E, D }, { G, H, I }} -> { A, B, C, D, E, F, G, H, I }</para>
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="src">列挙の引数配列(2次元列挙)</param>
		/// <returns>列挙(1次元列挙)</returns>
		public static IEnumerable<T> Join<T>(params IEnumerable<T>[] src)
		{
			return Linearize(src);
		}

		/// <summary>
		/// <para>列挙の列挙(2次元列挙)を列挙(1次元列挙)に変換する。</para>
		/// <para>例：{{ A, B, C }, { D, E, D }, { G, H, I }} -> { A, B, C, D, E, F, G, H, I }</para>
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="src">列挙の列挙(2次元列挙)</param>
		/// <returns>列挙(1次元列挙)</returns>
		public static IEnumerable<T> Linearize<T>(IEnumerable<IEnumerable<T>> src)
		{
			foreach (IEnumerable<T> part in src)
				foreach (T element in part)
					yield return element;
		}

		public class Cartridge<T> : IDisposable
		{
			private IEnumerator<T> Inner;
			private bool MoveNextRet = false;

			public Cartridge(IEnumerable<T> inner)
			{
				this.Inner = inner.GetEnumerator();
			}

			[Obsolete]
			public Cartridge(IEnumerator<T> inner_binding)
			{
				this.Inner = inner_binding;
			}

			public bool MoveNext()
			{
				return this.MoveNextRet = this.Inner.MoveNext();
			}

			public bool HasCurrent()
			{
				return this.MoveNextRet;
			}

			public T Current
			{
				get
				{
					return this.Inner.Current;
				}
			}

			public void Dispose()
			{
				if (this.Inner != null)
				{
					this.Inner.Dispose();
					this.Inner = null;
				}
			}
		}

		public static Cartridge<T> GetCartridge<T>(IEnumerable<T> inner)
		{
			return new Cartridge<T>(inner);
		}

		[Obsolete]
		public static Cartridge<T> GetCartridge<T>(IEnumerator<T> inner_binding)
		{
			return new Cartridge<T>(inner_binding);
		}

		/// <summary>
		/// マージ
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="enu1">入力1</param>
		/// <param name="enu2">入力2</param>
		/// <param name="destOnly1">入力1だけに存在する要素について呼び出す。null == 何もしない。</param>
		/// <param name="destBoth1">両方に存在する入力1の要素について呼び出す。null == 何もしない。</param>
		/// <param name="destBoth2">両方に存在する入力2の要素について呼び出す。null == 何もしない。</param>
		/// <param name="destOnly2">入力2だけに存在する要素について呼び出す。null == 何もしない。</param>
		/// <param name="comp">要素の比較</param>
		public static void Merge<T>(IEnumerable<T> enu1, IEnumerable<T> enu2, Action<T> destOnly1, Action<T> destBoth1, Action<T> destBoth2, Action<T> destOnly2, Comparison<T> comp)
		{
			using (Cartridge<T> reader1 = GetCartridge(enu1))
			using (Cartridge<T> reader2 = GetCartridge(enu2))
			{
				if (destOnly1 == null)
					destOnly1 = v => { };

				if (destBoth1 == null)
					destBoth1 = v => { };

				if (destBoth2 == null)
					destBoth2 = v => { };

				if (destOnly2 == null)
					destOnly2 = v => { };

				reader1.MoveNext();
				reader2.MoveNext();

#if true
				while (reader1.HasCurrent() && reader2.HasCurrent())
				{
					int ret = comp(reader1.Current, reader2.Current);

					if (ret < 0)
					{
						destOnly1(reader1.Current);
						reader1.MoveNext();
					}
					else if (0 < ret)
					{
						destOnly2(reader2.Current);
						reader2.MoveNext();
					}
					else
					{
						destBoth1(reader1.Current);
						destBoth2(reader2.Current);
						reader1.MoveNext();
						reader2.MoveNext();
					}
				}
				while (reader1.HasCurrent())
				{
					destOnly1(reader1.Current);
					reader1.MoveNext();
				}
				while (reader2.HasCurrent())
				{
					destOnly2(reader2.Current);
					reader2.MoveNext();
				}
#else // same_code
				for (; ; )
				{
					int ret;

					if (reader1.HasCurrent() == false)
					{
						if (reader2.HasCurrent() == false)
							break;

						ret = 1;
					}
					else if (reader2.HasCurrent() == false)
					{
						ret = -1;
					}
					else
					{
						ret = comp(reader1.Current, reader2.Current);
					}

					if (ret < 0)
					{
						destOnly1(reader1.Current);
						reader1.MoveNext();
					}
					else if (0 < ret)
					{
						destOnly2(reader2.Current);
						reader2.MoveNext();
					}
					else
					{
						destBoth1(reader1.Current);
						destBoth2(reader2.Current);
						reader1.MoveNext();
						reader2.MoveNext();
					}
				}
#endif
			}
		}

		public static void CollectMergedPairs<T>(IEnumerable<T> enu1, IEnumerable<T> enu2, Action<T[]> dest, T defval, Comparison<T> comp)
		{
			using (Cartridge<T> reader1 = GetCartridge(enu1))
			using (Cartridge<T> reader2 = GetCartridge(enu2))
			{
				reader1.MoveNext();
				reader2.MoveNext();

				for (; ; )
				{
					int ret;

					if (reader1.HasCurrent() == false)
					{
						if (reader2.HasCurrent() == false)
							break;

						ret = 1;
					}
					else if (reader2.HasCurrent() == false)
					{
						ret = -1;
					}
					else
					{
						ret = comp(reader1.Current, reader2.Current);
					}

					if (ret < 0)
					{
						dest(new T[] { reader1.Current, defval });
						reader1.MoveNext();
					}
					else if (0 < ret)
					{
						dest(new T[] { defval, reader2.Current });
						reader2.MoveNext();
					}
					else
					{
						dest(new T[] { reader1.Current, reader2.Current });
						reader1.MoveNext();
						reader2.MoveNext();
					}
				}
			}
		}

		public static IEnumerable<T> Endless<T>(T element)
		{
			return Endless<T>(() => element);
		}

		public static IEnumerable<T> Endless<T>(Func<T> getter)
		{
			for (; ; )
			{
				yield return getter();
			}
		}

		public static IEnumerable<T> Repeat<T>(T element, int count)
		{
			while (1 <= count)
			{
				yield return element;
				count--;
			}
		}

		public static IEnumerable<T> Reverse<T>(T[] src)
		{
			for (int index = src.Length - 1; 0 <= index; index--)
			{
				yield return src[index];
			}
		}

		public static IEnumerable<T> Sort<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			T[] arr = src.ToArray();
			Array.Sort(arr, comp);
			return arr;
		}

		/// <summary>
		/// 列挙をゲッターメソッドでラップします。
		/// 例：{ A, B, C } -> 呼び出し毎に右の順で戻り値を返す { A, B, C, default(T), default(T), default(T), ... }
		/// </summary>
		/// <typeparam name="T">要素の型</typeparam>
		/// <param name="src">列挙</param>
		/// <returns>ゲッターメソッド</returns>
		public static Func<T> Supplier<T>(IEnumerable<T> src)
		{
			IEnumerator<T> reader = src.GetEnumerator();

			return () =>
			{
				if (reader != null)
				{
					if (reader.MoveNext())
						return reader.Current;

					reader.Dispose();
					reader = null;
				}
				return default(T);
			};
		}
	}
}
