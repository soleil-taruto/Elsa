using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SortedList<T>
	{
		private List<T> InnerList;
		private Comparison<T> Comp;
		private bool SortedFlag;

		public SortedList(List<T> bindingList, Comparison<T> comp, bool sortedFlag = false)
		{
			this.InnerList = bindingList;
			this.Comp = comp;
			this.SortedFlag = sortedFlag;
		}

		public SortedList(Comparison<T> comp)
		{
			this.InnerList = new List<T>();
			this.Comp = comp;
			this.SortedFlag = true;
		}

		public void Clear()
		{
			this.InnerList.Clear();
			this.SortedFlag = true;
		}

		public void Add(T element)
		{
			this.InnerList.Add(element);
			this.SortedFlag = false;
		}

		public void AddRange(T[] elements)
		{
			this.InnerList.AddRange(elements);
			this.SortedFlag = false;
		}

		public int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		private void BeforeAccessElement()
		{
			if (SortedFlag == false)
			{
				this.InnerList.Sort(this.Comp);
				this.SortedFlag = true;
			}
		}

		public T Get(int index)
		{
			this.BeforeAccessElement();
			return this.InnerList[index];
		}

		public List<T> GetRange(int index = 0)
		{
			return this.GetRange(index, this.Count - index);
		}

		public List<T> GetRange(int index, int count)
		{
			this.BeforeAccessElement();
			return this.InnerList.GetRange(index, count);
		}

		public bool Contains(Func<T, int> ferret)
		{
			return this.IndexOf(ferret) != -1;
		}

		public int IndexOf(Func<T, int> ferret)
		{
			this.BeforeAccessElement();

			int l = -1;
			int r = this.InnerList.Count;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (ret < 0)
				{
					l = m;
				}
				else if (0 < ret)
				{
					r = m;
				}
				else
				{
					return m;
				}
			}
			return -1; // not found
		}

		public int LeftIndexOf(Func<T, int> ferret) // ret: ターゲット以上になる最初の位置、無ければ要素数
		{
			this.BeforeAccessElement();

			int l = 0;
			int r = this.InnerList.Count;

			while (l < r)
			{
				int m = (l + r) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (ret < 0)
				{
					l = m + 1;
				}
				else
				{
					r = m;
				}
			}
			return l;
		}

		public int RightIndexOf(Func<T, int> ferret, int l = -1) // ret: ターゲット以下になる最後の位置、無ければ(-1)
		{
			this.BeforeAccessElement();

			//int l = -1;
			int r = this.InnerList.Count - 1;

			while (l < r)
			{
				int m = (l + r + 1) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (0 < ret)
				{
					r = m - 1;
				}
				else
				{
					l = m;
				}
			}
			return r;
		}

		public List<T> GetMatch(Func<T, int> ferret)
		{
#if true
			int l = this.LeftIndexOf(ferret);
			int r = this.RightIndexOf(ferret, l - 1) + 1;

			return this.GetRange(l, r - l);
#else // smpl_same
			List<T> dest = new List<T>();

			for (int index = this.LeftIndexOf(ferret); index <= this.RightIndexOf(ferret); index++)
			{
				dest.Add(this.InnerList[index]);
			}
			return dest;
#endif
		}

		public List<T> GetMatchWithEdge(Func<T, int> ferret)
		{
#if true
			int l = this.LeftIndexOf(ferret);
			int r = this.RightIndexOf(ferret, l - 1) + 1;

			bool ld = false;
			bool rd = false;

			if (0 < l)
				l--;
			else
				ld = true;

			if (r < this.Count)
				r++;
			else
				rd = true;

			List<T> dest = new List<T>();

			if (ld)
				dest.Add(default(T));

			dest.AddRange(this.GetRange(l, r - l));

			if (rd)
				dest.Add(default(T));

			return dest;
#else // smpl_same
			List<T> dest = new List<T>();

			for (int index = this.LeftIndexOf(ferret) - 1; index <= this.RightIndexOf(ferret) + 1; index++)
			{
				if (index < 0 || this.Count <= index)
					dest.Add(default(T));
				else
					dest.Add(this.InnerList[index]);
			}
			return dest;
#endif
		}

		public Func<T, int> GetFerret(T target)
		{
			return (T value) => this.Comp(value, target);
		}

		public Func<T, int> GetFerret(T mintrg, T maxtrg)
		{
			return (T value) =>
			{
				if (this.Comp(value, mintrg) < 0)
					return -1;

				if (this.Comp(value, maxtrg) > 0)
					return 1;

				return 0;
			};
		}
	}
}
