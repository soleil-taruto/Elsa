using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Surfaces
{
	public class Surface_背景 : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.背景_森の中, // 800 x 450
			Ground.I.Picture.背景_屋上, // 776 x 900
			Ground.I.Picture.背景_夜, // 800 x 1028 // 余白除く_LTRB == 0, 60, 800, 976
		};

		private int ImageIndex = -1; // -1 == 無効, 0 ～ (Images.Length - 1)
		private D2Size ImageDrawSize;
		private D2Point ImageLTStart;
		private D2Point ImageLTEnd = new D2Point(0, 0); // 固定値
		private double CurrDrawPosRate = 0.0;
		private double DestDrawPosRate = 0.0;

		public override void Draw()
		{
			if (this.ImageIndex == -1)
				throw new DDError("画像を指定して下さい。");

			DDUtils.Approach(ref this.CurrDrawPosRate, this.DestDrawPosRate, 0.99995);

			D2Point lt = DDUtils.AToBRate(this.ImageLTStart, this.ImageLTEnd, this.CurrDrawPosRate);

			DDDraw.DrawRect(
				this.Images[this.ImageIndex],
				lt.X,
				lt.Y,
				this.ImageDrawSize.W,
				this.ImageDrawSize.H
				);
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == ScenarioWords.COMMAND_画像)
			{
				int index = int.Parse(arguments[c++]);

				if (index < 0 || this.Images.Length <= index)
					throw new DDError("Bad index: " + index);

				this.ImageIndex = index;
				this.ImageDrawSize = DDUtils.AdjustRectExterior(this.Images[index].GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H)).Size;
				this.ImageLTStart = new D2Point(DDConsts.Screen_W - this.ImageDrawSize.W, DDConsts.Screen_H - this.ImageDrawSize.H);
				//this.ImageLTEnd = new D2Point(0, 0); // 固定値なので更新しない。
			}
			else if (command == ScenarioWords.COMMAND_Slide)
			{
				double rate1 = double.Parse(arguments[c++]);
				double rate2 = double.Parse(arguments[c++]);

				rate1 = DoubleTools.ToRange(rate1, 0.0, 1.0);
				rate2 = DoubleTools.ToRange(rate2, 0.0, 1.0);

				this.CurrDrawPosRate = rate1;
				this.DestDrawPosRate = rate2;
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
				this.ImageIndex.ToString(),
				this.ImageDrawSize.W.ToString("F9"),
				this.ImageDrawSize.H.ToString("F9"),
				this.ImageLTStart.X.ToString("F9"),
				this.ImageLTStart.Y.ToString("F9"),
				this.CurrDrawPosRate.ToString("F9"),
				this.DestDrawPosRate.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.ImageIndex = int.Parse(lines[c++]);
			this.ImageDrawSize.W = double.Parse(lines[c++]);
			this.ImageDrawSize.H = double.Parse(lines[c++]);
			this.ImageLTStart.X = double.Parse(lines[c++]);
			this.ImageLTStart.Y = double.Parse(lines[c++]);
			this.CurrDrawPosRate = double.Parse(lines[c++]);
			this.DestDrawPosRate = double.Parse(lines[c++]);
		}
	}
}
