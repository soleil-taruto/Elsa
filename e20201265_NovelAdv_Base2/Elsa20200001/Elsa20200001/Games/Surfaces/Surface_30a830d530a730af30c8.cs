using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Games.Surfaces
{
	public class Surface_エフェクト : Surface
	{
		public Surface_エフェクト(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 50000;
		}

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				// none

				yield return true;
			}
		}
	}
}
