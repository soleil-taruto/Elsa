using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Surfaces
{
	public class Surface_音楽 : Surface
	{
		private string MusicFile = null; // null == 停止中

		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "再生")
			{
				this.MusicFile = arguments[c++];
				this.MusicFileChanged();
			}
			else if (command == "停止")
			{
				this.MusicFile = null;
				this.MusicFileChanged();
			}
			else
			{
				throw new DDError();
			}
		}

		private void MusicFileChanged()
		{
			if (this.MusicFile == null)
				DDMusicUtils.Fade();
			else
				DDCCResource.GetMusic(this.MusicFile).Play();
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				Common.WrapNullOrString(this.MusicFile),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.MusicFile = Common.UnwrapNullOrString(lines[c++]);
			this.MusicFileChanged();
		}
	}
}
