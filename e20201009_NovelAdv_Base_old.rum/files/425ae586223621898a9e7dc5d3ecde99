using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_イベント絵 : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.東北ずん子サムネ用,
		};

		private int ImageIndex = -1; // -1 == 画像無し, 0 ～ (Images.Length - 1)
		private double A = 1.0;

		public override void Draw()
		{
			this.Draw(this.A);
		}

		private void Draw(double a)
		{
			if (this.ImageIndex == -1) // ? 画像無し
				return;

			DDDraw.SetAlpha(a);
			DDDraw.DrawRect(
				this.Images[this.ImageIndex],
				DDUtils.AdjustRectExterior(this.Images[this.ImageIndex].GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H))
				);
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "画像")
			{
				int index = int.Parse(arguments[c++]);

				if (index < 0 || this.Images.Length <= index)
					throw new DDError("Bad index: " + index);

				this.ImageIndex = index;
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン()));
				this.A = 1.0;
			}
			else if (command == "フェードアウト")
			{
				this.Act.Add(SCommon.Supplier(this.フェードアウト()));
				this.A = 0.0;
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				this.A = scene.Rate;
				this.Draw();
				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				this.Draw(1.0 - scene.Rate);

				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.ImageIndex.ToString(),
				this.A.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.ImageIndex = int.Parse(lines[c++]);
			this.A = double.Parse(lines[c++]);
		}
	}
}

