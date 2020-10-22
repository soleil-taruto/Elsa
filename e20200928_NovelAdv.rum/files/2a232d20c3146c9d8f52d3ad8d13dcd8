using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AnonyDisposable : IDisposable
	{
		private Action Routine;

		public AnonyDisposable(Action routine)
		{
			this.Routine = routine;
		}

		public void Dispose()
		{
			if (this.Routine != null)
			{
				this.Routine();
				this.Routine = null;
			}
		}
	}
}
