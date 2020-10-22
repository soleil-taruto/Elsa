using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	/// <summary>
	/// <para>からい氏</para>
	/// <para>東北ずん子</para>
	/// <para>正対</para>
	/// </summary>
	public class Surface_東北ずん子 : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.東北ずん子01, // 0 - 制服_普(喜)
			Ground.I.Picture.東北ずん子02, // 1 - 制服_怒
			Ground.I.Picture.東北ずん子03, // 2 - 制服_泣
			Ground.I.Picture.東北ずん子04, // 3 - 制服_食
			Ground.I.Picture.東北ずん子05, // 4 - 普
			Ground.I.Picture.東北ずん子06, // 5 - 赤面？
			Ground.I.Picture.東北ずん子07, // 6 - なまはげ
			Ground.I.Picture.東北ずん子08, // 7 - 思考
			Ground.I.Picture.東北ずん子09, // 8 - はてな
			Ground.I.Picture.東北ずん子10, // 9 - 刃
			Ground.I.Picture.東北ずん子11, // 10 - メイド_驚
		};

		private int Mode = MODE_DEFAULT; // 0 - (Images.Length - 1)

		public const int MODE_DEFAULT = 4;

		public override void Draw()
		{
			const double R = 2.0;

			DDDraw.DrawBegin(this.Images[this.Mode], this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + R) * 2.0);
			DDDraw.DrawZoom(0.5);
			DDDraw.DrawEnd();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "Mode")
			{
				int mode = int.Parse(arguments[c++]);

				if (mode < 0 || this.Images.Length <= mode)
					throw new DDError("Bad mode: " + mode);

				this.Mode = mode;
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
				this.Mode.ToString(),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Mode = int.Parse(lines[c++]);
		}
	}
}
