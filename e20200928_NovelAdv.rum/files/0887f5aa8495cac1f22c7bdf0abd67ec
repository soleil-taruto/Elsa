using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	public class NamedEventUnit : IDisposable
	{
		private EventWaitHandle Handle;
		private bool Binding;

		public NamedEventUnit(string name)
			: this(NamedEventTools.Create(name), true)
		{ }

		public NamedEventUnit(EventWaitHandle handle, bool binding = false)
		{
			this.Handle = handle;
			this.Binding = binding;
		}

		public void Set()
		{
			this.Handle.Set();
		}

		public bool WaitForMillis(int millis)
		{
			return this.Handle.WaitOne(millis);
		}

		public void WaitForever()
		{
			this.Handle.WaitOne();
		}

		public void Dispose()
		{
			if (this.Handle != null)
			{
				if (this.Binding)
					this.Handle.Dispose();

				this.Handle = null;
			}
		}
	}
}
