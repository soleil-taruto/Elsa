using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_音楽 : Surface
	{
		private class MusicInfo
		{
			public string Name;
			public DDMusic Music;

			public MusicInfo(string name, DDMusic music)
			{
				this.Name = name;
				this.Music = music;
			}
		}

		#region MusicList

		private MusicInfo[] MusicList = new MusicInfo[]
		{
			new MusicInfo("Healing01", Ground.I.Music.Healing01),
			new MusicInfo("Healing02", Ground.I.Music.Healing02),
			new MusicInfo("Healing03", Ground.I.Music.Healing03),
			new MusicInfo("Healing04", Ground.I.Music.Healing04),
			new MusicInfo("Healing05", Ground.I.Music.Healing05),
			new MusicInfo("Healing06", Ground.I.Music.Healing06),
			new MusicInfo("Healing07", Ground.I.Music.Healing07),
			new MusicInfo("Healing08", Ground.I.Music.Healing08),
			new MusicInfo("Healing09", Ground.I.Music.Healing09),
			new MusicInfo("Healing10", Ground.I.Music.Healing10),
			new MusicInfo("Healing11", Ground.I.Music.Healing11),
			new MusicInfo("Healing11B", Ground.I.Music.Healing11B),
			new MusicInfo("Healing12", Ground.I.Music.Healing12),
			new MusicInfo("Healing12B", Ground.I.Music.Healing12B),
			new MusicInfo("Healing13", Ground.I.Music.Healing13),
			new MusicInfo("Healing14", Ground.I.Music.Healing14),
			new MusicInfo("Healing14B", Ground.I.Music.Healing14B),
			new MusicInfo("Healing15", Ground.I.Music.Healing15),
			new MusicInfo("Healing16", Ground.I.Music.Healing16),
			new MusicInfo("Healing17", Ground.I.Music.Healing17),
			new MusicInfo("Orchestra01", Ground.I.Music.Orchestra01),
			new MusicInfo("Orchestra01B", Ground.I.Music.Orchestra01B),
			new MusicInfo("Orchestra02", Ground.I.Music.Orchestra02),
			new MusicInfo("Orchestra03", Ground.I.Music.Orchestra03),
			new MusicInfo("Orchestra04", Ground.I.Music.Orchestra04),
			new MusicInfo("Orchestra05", Ground.I.Music.Orchestra05),
			new MusicInfo("Orchestra06", Ground.I.Music.Orchestra06),
			new MusicInfo("Orchestra07", Ground.I.Music.Orchestra07),
			new MusicInfo("Orchestra08", Ground.I.Music.Orchestra08),
			new MusicInfo("Orchestra09", Ground.I.Music.Orchestra09),
			new MusicInfo("Orchestra10", Ground.I.Music.Orchestra10),
			new MusicInfo("Orchestra11", Ground.I.Music.Orchestra11),
			new MusicInfo("Orchestra12", Ground.I.Music.Orchestra12),
			new MusicInfo("Orchestra13", Ground.I.Music.Orchestra13),
			new MusicInfo("Orchestra14", Ground.I.Music.Orchestra14),
			new MusicInfo("Orchestra15", Ground.I.Music.Orchestra15),
			new MusicInfo("Orchestra16", Ground.I.Music.Orchestra16),
			new MusicInfo("Orchestra17", Ground.I.Music.Orchestra17),
			new MusicInfo("Orchestra18", Ground.I.Music.Orchestra18),
			new MusicInfo("Orchestra19", Ground.I.Music.Orchestra19),
			new MusicInfo("Orchestra20", Ground.I.Music.Orchestra20),
			new MusicInfo("Orchestra21", Ground.I.Music.Orchestra21),
			new MusicInfo("Orchestra22", Ground.I.Music.Orchestra22),
			new MusicInfo("Orchestra23", Ground.I.Music.Orchestra23),
			new MusicInfo("Orchestra24", Ground.I.Music.Orchestra24),
			new MusicInfo("Orchestra25", Ground.I.Music.Orchestra25),
			new MusicInfo("Orchestra26", Ground.I.Music.Orchestra26),
			new MusicInfo("Sinfonia01", Ground.I.Music.Sinfonia01),
			new MusicInfo("Sinfonia01_Piano", Ground.I.Music.Sinfonia01_Piano),
			new MusicInfo("Sinfonia02_Piano", Ground.I.Music.Sinfonia02_Piano),
			new MusicInfo("Piano_Ahurera", Ground.I.Music.Piano_Ahurera),
			new MusicInfo("Piano_Calendula", Ground.I.Music.Piano_Calendula),
			new MusicInfo("Piano_FeelsHappiness", Ground.I.Music.Piano_FeelsHappiness),
			new MusicInfo("Piano_Milkeyway", Ground.I.Music.Piano_Milkeyway),
			new MusicInfo("Piano_Noapusa", Ground.I.Music.Piano_Noapusa),
			new MusicInfo("Piano_Siroganenokobune", Ground.I.Music.Piano_Siroganenokobune),
			new MusicInfo("Piano01", Ground.I.Music.Piano01),
			new MusicInfo("Piano02", Ground.I.Music.Piano02),
			new MusicInfo("Piano03", Ground.I.Music.Piano03),
			new MusicInfo("Piano04", Ground.I.Music.Piano04),
			new MusicInfo("Piano05", Ground.I.Music.Piano05),
			new MusicInfo("Piano06", Ground.I.Music.Piano06),
			new MusicInfo("Piano07", Ground.I.Music.Piano07),
			new MusicInfo("Piano08", Ground.I.Music.Piano08),
			new MusicInfo("Piano09", Ground.I.Music.Piano09),
			new MusicInfo("Piano10", Ground.I.Music.Piano10),
			new MusicInfo("Piano11", Ground.I.Music.Piano11),
			new MusicInfo("Piano12", Ground.I.Music.Piano12),
			new MusicInfo("Piano13", Ground.I.Music.Piano13),
			new MusicInfo("Piano14", Ground.I.Music.Piano14),
			new MusicInfo("Piano15", Ground.I.Music.Piano15),
			new MusicInfo("Piano16", Ground.I.Music.Piano16),
			new MusicInfo("Piano17", Ground.I.Music.Piano17),
			new MusicInfo("Piano18", Ground.I.Music.Piano18),
			new MusicInfo("Piano19", Ground.I.Music.Piano19),
			new MusicInfo("Piano20", Ground.I.Music.Piano20),
			new MusicInfo("Piano21", Ground.I.Music.Piano21),
			new MusicInfo("Piano22", Ground.I.Music.Piano22),
			new MusicInfo("Piano23", Ground.I.Music.Piano23),
			new MusicInfo("Piano24", Ground.I.Music.Piano24),
			new MusicInfo("Piano25", Ground.I.Music.Piano25),
			new MusicInfo("Piano26", Ground.I.Music.Piano26),
			new MusicInfo("Piano27", Ground.I.Music.Piano27),
			new MusicInfo("Piano28", Ground.I.Music.Piano28),
			new MusicInfo("Piano29", Ground.I.Music.Piano29),
			new MusicInfo("Piano30", Ground.I.Music.Piano30),
			new MusicInfo("Piano31", Ground.I.Music.Piano31),
			new MusicInfo("Piano32", Ground.I.Music.Piano32),
			new MusicInfo("Piano33", Ground.I.Music.Piano33),
			new MusicInfo("Piano34", Ground.I.Music.Piano34),
			new MusicInfo("Piano35", Ground.I.Music.Piano35),
			new MusicInfo("Piano36", Ground.I.Music.Piano36),
			new MusicInfo("Piano37", Ground.I.Music.Piano37),
			new MusicInfo("Piano38", Ground.I.Music.Piano38),
			new MusicInfo("Piano39", Ground.I.Music.Piano39),
			new MusicInfo("Piano40", Ground.I.Music.Piano40),
			new MusicInfo("Piano41", Ground.I.Music.Piano41),
		};

		#endregion

		private MusicInfo CurrMusic = null; // null == 停止中

		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "再生")
			{
				string name = arguments[c++];
				int index = SCommon.IndexOf(this.MusicList, v => v.Name == name);

				if (index == -1)
					throw new DDError("Bad (music) name: " + name);

				this.CurrMusic = this.MusicList[index];
				this.CurrMusicChanged();
			}
			else if (command == "停止")
			{
				this.CurrMusic = null;
				this.CurrMusicChanged();
			}
			else
			{
				throw new DDError();
			}
		}

		private void CurrMusicChanged()
		{
			if (this.CurrMusic == null)
				DDMusicUtils.Fade();
			else
				this.CurrMusic.Music.Play();
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.CurrMusic == null ? Consts.SERIALIZED_NULL : Consts.SERIALIZED_NOT_NULL_PREFIX + this.CurrMusic.Name,
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			{
				string line = lines[c++];

				if (line == Consts.SERIALIZED_NULL)
				{
					this.CurrMusic = null;
					this.CurrMusicChanged();
				}
				else
				{
					string name = line.Substring(Consts.SERIALIZED_NOT_NULL_PREFIX.Length);
					int index = SCommon.IndexOf(this.MusicList, v => v.Name == name);

					if (index == -1)
						throw new DDError("Bad (music) name: " + name);

					this.CurrMusic = this.MusicList[index];
					this.CurrMusicChanged();
				}
			}
		}
	}
}
