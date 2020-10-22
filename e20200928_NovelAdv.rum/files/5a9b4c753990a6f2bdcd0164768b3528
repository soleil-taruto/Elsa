using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class VisitorCounter
	{
		public int Count;

		public VisitorCounter(int count = 0)
		{
			this.Count = count;
		}

		public bool HasVisitor(int count = 1)
		{
			return count <= this.Count;
		}

		public void Enter()
		{
			this.Count++;
		}

		public void Leave()
		{
			this.Count--;
		}

		public IDisposable Section()
		{
			this.Enter();
			return new AnonyDisposable(() => this.Leave());
		}
	}
}
