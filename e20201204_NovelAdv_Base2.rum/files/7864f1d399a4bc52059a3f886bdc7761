using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Surfaces
{
	public class Surface_効果音 : Surface
	{
		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "再生")
			{
				this.Act.Add(() =>
				{
					DDCCResource.GetSE(arguments[c++]).Play();
					return false;
				});
			}
			else
			{
				throw new DDError();
			}
		}
	}
}
