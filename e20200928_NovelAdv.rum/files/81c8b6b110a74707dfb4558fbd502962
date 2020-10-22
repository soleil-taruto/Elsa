using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// からい氏
	/// </summary>
	public class Surface_からい : Surface
	{
		private double Draw_Rnd = DDUtils.Random.Real2() * Math.PI * 2.0;

		public enum Chara_t
		{
			結月ゆかり, // 右向き
			東北ずん子, // 正対
			弦巻マキ, // 左向き
		}

		private Chara_t Chara = Chara_t.結月ゆかり;

		public enum Mode_t
		{
			普,
			泣,
			喜,
			怒,
		}

		private Mode_t Mode = Mode_t.普;

		private DDPicture[,] ImageTable = new DDPicture[,]
		{
			// 結月ゆかり (右向き)
			{
				Ground.I.Picture.結月ゆかり16, // 普
				Ground.I.Picture.結月ゆかり02, // 泣
				Ground.I.Picture.結月ゆかり03, // 喜
				Ground.I.Picture.結月ゆかり01, // 怒
			},
			// 東北ずん子 (正対)
			{
				Ground.I.Picture.東北ずん子04, // 普
				Ground.I.Picture.東北ずん子03, // 泣
				Ground.I.Picture.東北ずん子01, // 喜
				Ground.I.Picture.東北ずん子01, // 怒
			},
			// 弦巻マキ (左向き)
			{
				Ground.I.Picture.弦巻マキ02, // 普
				Ground.I.Picture.弦巻マキ03, // 泣
				Ground.I.Picture.弦巻マキ06, // 喜 (服が違う)
				Ground.I.Picture.弦巻マキ01, // 怒
			},
		};

		public override void Draw()
		{
			double R = this.Draw_Rnd;

			DDDraw.DrawBegin(this.ImageTable[(int)this.Chara, (int)this.Mode], this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + R) * 2.0);
			DDDraw.DrawZoom(0.5);
			DDDraw.DrawEnd();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == ScenarioWords.COMMAND_Chara)
			{
				int chara = int.Parse(arguments[c++]);

				if (chara < 0 || Enum.GetValues(typeof(Chara_t)).Cast<int>().Max() < chara)
					throw new DDError("Bad chara: " + chara);

				this.Chara = (Chara_t)chara;
			}
			else if (command == ScenarioWords.COMMAND_Mode)
			{
				int mode = int.Parse(arguments[c++]);

				if (mode < 0 || Enum.GetValues(typeof(Mode_t)).Cast<int>().Max() < mode)
					throw new DDError("Bad mode: " + mode);

				this.Mode = (Mode_t)mode;
			}
			else
			{
				throw new DDError();
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				"" + (int)this.Chara,
				"" + (int)this.Mode,
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Chara = (Chara_t)int.Parse(lines[c++]);
			this.Mode = (Mode_t)int.Parse(lines[c++]);
		}
	}
}
