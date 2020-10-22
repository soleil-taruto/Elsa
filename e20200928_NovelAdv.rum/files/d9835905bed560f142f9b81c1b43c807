using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class BluffList<T>
	{
		public int Count = 0;
		public Func<int, T> GetElement = null;
		public Action<int, T> SetElement = null;

		// <---- prm

		public BluffList()
		{ }

		public BluffList(T[] entity)
		{
			this.Count = entity.Length;
			this.GetElement = index => entity[index];
			this.SetElement = (index, value) => entity[index] = value;
		}

		public BluffList(T element = default(T), int count = 1)
		{
			this.Count = count;
			this.GetElement = index => element;
			this.SetElement = null;
		}

		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}

			set
			{
				this.SetElement(index, value);
			}
		}

		public IEnumerable<T> Iterate()
		{
			for (int index = 0; index < this.Count; index++)
			{
				yield return this.GetElement(index);
			}
		}

		public BluffList<T> FreeRange(T defval = default(T))
		{
			return new BluffList<T>()
			{
				Count = this.Count,
				GetElement = index =>
				{
					if (index < 0 || this.Count <= index) // out of range
					{
						return defval;
					}
					return this.GetElement(index);
				},
				SetElement = this.SetElement,
			};
		}

		public BluffList<T> Reverse()
		{
			return new BluffList<T>()
			{
				Count = this.Count,
				GetElement = index => this.GetElement(this.Count - 1 - index),
				SetElement = (index, value) => this.SetElement(this.Count - 1 - index, value),
			};
		}

		public BluffList<T> SubRange(int start, int count)
		{
			return new BluffList<T>()
			{
				Count = count,
				GetElement = index => this.GetElement(start + index),
				SetElement = (index, value) => this.SetElement(start + index, value),
			};
		}

		public BluffList<T> AddRange(BluffList<T> trailer)
		{
			return new BluffList<T>()
			{
				Count = this.Count + trailer.Count,
				GetElement = index => index < this.Count ? this.GetElement(index) : trailer.GetElement(index - this.Count),
				SetElement = (index, value) =>
				{
					if (index < this.Count)
						this.SetElement(index, value);
					else
						trailer.SetElement(index - this.Count, value);
				},
			};
		}
	}
}
