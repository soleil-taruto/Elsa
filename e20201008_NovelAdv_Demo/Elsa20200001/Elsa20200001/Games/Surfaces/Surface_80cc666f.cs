﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_背景 : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.背景_森の中, // 800 x 450
			Ground.I.Picture.背景_屋上, // 776 x 900
			Ground.I.Picture.背景_夜, // 800 x 1028 // 余白除く_LTRB == 0, 60, 800, 976

			Ground.I.Picture.背景_BG02a_80,
			Ground.I.Picture.背景_BG14a_80,
			Ground.I.Picture.背景_BG15a_80,
			Ground.I.Picture.背景_BG16a_80,
			Ground.I.Picture.背景_BG26a_80,

			Ground.I.Picture.背景_BG40o1,
		};

		private int ImageIndex = -1; // -1 == 画像無し, 0 ～ (Images.Length - 1)
		private D2Size ImageDrawSize;
		private D2Point ImageLTStart = new D2Point(0, 0); // 固定値
		private D2Point ImageLTEnd;
		private double CurrDrawPosRate_X = 0.5;
		private double CurrDrawPosRate_Y = 0.5;
		private double DestDrawPosRate_X = 0.5;
		private double DestDrawPosRate_Y = 0.5;

		#region CurrDrawPosRate, DestDrawPosRate

		private double CurrDrawPosRate
		{
			get
			{
				return this.CurrDrawPosRate_X;
				//return this.CurrDrawPosRate_Y;
			}

			set
			{
				this.CurrDrawPosRate_X = value;
				this.CurrDrawPosRate_Y = value;
			}
		}

		private double DestDrawPosRate
		{
			get
			{
				return this.DestDrawPosRate_X;
				//return this.DestDrawPosRate_Y;
			}

			set
			{
				this.DestDrawPosRate_X = value;
				this.DestDrawPosRate_Y = value;
			}
		}

		#endregion

		public override void Draw()
		{
			this.Draw(1.0);
		}

		private void Draw(double a)
		{
			if (this.ImageIndex == -1) // ? 画像無し
				return;

			DDUtils.Approach(ref this.CurrDrawPosRate_X, this.DestDrawPosRate_X, 0.9999);
			DDUtils.Approach(ref this.CurrDrawPosRate_Y, this.DestDrawPosRate_Y, 0.9999);

			DDDraw.SetAlpha(a);
			DDDraw.DrawRect(
				this.Images[this.ImageIndex],
				DDUtils.AToBRate(this.ImageLTStart.X, this.ImageLTEnd.X, this.CurrDrawPosRate_X),
				DDUtils.AToBRate(this.ImageLTStart.Y, this.ImageLTEnd.Y, this.CurrDrawPosRate_Y),
				this.ImageDrawSize.W,
				this.ImageDrawSize.H
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
				this.ImageDrawSize = DDUtils.AdjustRectExterior(this.Images[index].GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H)).Size;
				//this.ImageLTStart = new D2Point(0, 0); // 固定値なので更新しない。
				this.ImageLTEnd = new D2Point(DDConsts.Screen_W - this.ImageDrawSize.W, DDConsts.Screen_H - this.ImageDrawSize.H);

				this.CurrDrawPosRate = 0.5; // reset
				this.DestDrawPosRate = 0.5; // reset
			}
			else if (command == "拡大")
			{
				if (this.ImageIndex == -1) // ? 画像無し
					throw new DDError("画像無し");

				double rate = double.Parse(arguments[c++]);

				this.ImageDrawSize *= rate;
				//this.ImageLTStart = new D2Point(0, 0); // 固定値なので更新しない。
				this.ImageLTEnd = new D2Point(DDConsts.Screen_W - this.ImageDrawSize.W, DDConsts.Screen_H - this.ImageDrawSize.H);

				// しきい値に根拠無し
				if (
					this.ImageDrawSize.W < 10.0 ||
					this.ImageDrawSize.H < 10.0
					)
					throw new DDError();
			}
			else if (command == "Slide")
			{
				double rate1 = double.Parse(arguments[c++]);
				double rate2 = double.Parse(arguments[c++]);

				rate1 = SCommon.ToRange(rate1, 0.0, 1.0);
				rate2 = SCommon.ToRange(rate2, 0.0, 1.0);

				this.CurrDrawPosRate = rate1;
				this.DestDrawPosRate = rate2;
			}
			else if (command == "スライド") // "からい" の "スライド" に合わせる。
			{
				double rate1_X = this.CurrDrawPosRate_X;
				double rate1_Y = this.CurrDrawPosRate_Y;
				double rate2 = double.Parse(arguments[c++]);
				int waitFrame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド(rate1_X, rate1_Y, rate2, waitFrame)));

				// アクションクリア対策
				this.CurrDrawPosRate = rate2;
				this.DestDrawPosRate = rate2;
			}
			else if (command == "スライド_X") // "からい" の "スライド" に合わせる。
			{
				double rate1 = this.CurrDrawPosRate_X;
				double rate2 = double.Parse(arguments[c++]);
				int waitFrame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド_X(rate1, rate2, waitFrame)));

				// アクションクリア対策
				this.CurrDrawPosRate_X = rate2;
				this.DestDrawPosRate_X = rate2;
			}
			else if (command == "スライド_Y") // "からい" の "スライド" に合わせる。
			{
				double rate1 = this.CurrDrawPosRate_Y;
				double rate2 = double.Parse(arguments[c++]);
				int waitFrame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド_Y(rate1, rate2, waitFrame)));

				// アクションクリア対策
				this.CurrDrawPosRate_Y = rate2;
				this.DestDrawPosRate_Y = rate2;
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン()));
			}
			else
			{
				throw new DDError();
			}
		}

		/// <summary>
		/// "からい" の "スライド" に合わせる。
		/// </summary>
		/// <param name="rate1"></param>
		/// <param name="rate2"></param>
		/// <param name="waitFrame"></param>
		/// <returns></returns>
		private IEnumerable<bool> スライド(double rate1_X, double rate1_Y, double rate2, int waitFrame)
		{
			for (int c = 0; c < waitFrame; c++)
			{
				this.Draw(1.0);
				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				double tmpRate_X = DDUtils.AToBRate(rate1_X, rate2, DDUtils.SCurve(scene.Rate));
				double tmpRate_Y = DDUtils.AToBRate(rate1_Y, rate2, DDUtils.SCurve(scene.Rate));

				this.CurrDrawPosRate_X = tmpRate_X;
				this.CurrDrawPosRate_Y = tmpRate_Y;
				this.Draw(1.0);
				this.CurrDrawPosRate = this.DestDrawPosRate; // restore

				yield return true;
			}
		}

		/// <summary>
		/// "からい" の "スライド" に合わせる。
		/// </summary>
		/// <param name="rate1"></param>
		/// <param name="rate2"></param>
		/// <param name="waitFrame"></param>
		/// <returns></returns>
		private IEnumerable<bool> スライド_X(double rate1, double rate2, int waitFrame)
		{
			for (int c = 0; c < waitFrame; c++)
			{
				this.Draw(1.0);
				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				double tmpRate = DDUtils.AToBRate(rate1, rate2, DDUtils.SCurve(scene.Rate));

				this.CurrDrawPosRate_X = tmpRate;
				this.Draw(1.0);
				this.CurrDrawPosRate_X = this.DestDrawPosRate_X; // restore

				yield return true;
			}
		}

		/// <summary>
		/// "からい" の "スライド" に合わせる。
		/// </summary>
		/// <param name="rate1"></param>
		/// <param name="rate2"></param>
		/// <param name="waitFrame"></param>
		/// <returns></returns>
		private IEnumerable<bool> スライド_Y(double rate1, double rate2, int waitFrame)
		{
			for (int c = 0; c < waitFrame; c++)
			{
				this.Draw(1.0);
				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				double tmpRate = DDUtils.AToBRate(rate1, rate2, DDUtils.SCurve(scene.Rate));

				this.CurrDrawPosRate_Y = tmpRate;
				this.Draw(1.0);
				this.CurrDrawPosRate_Y = this.DestDrawPosRate_Y; // restore

				yield return true;
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				this.Draw(scene.Rate);

				yield return true;
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
				this.ImageLTEnd.X.ToString("F9"),
				this.ImageLTEnd.Y.ToString("F9"),
				this.CurrDrawPosRate_X.ToString("F9"),
				this.CurrDrawPosRate_Y.ToString("F9"),
				this.DestDrawPosRate_X.ToString("F9"),
				this.DestDrawPosRate_Y.ToString("F9"),
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
			this.ImageLTEnd.X = double.Parse(lines[c++]);
			this.ImageLTEnd.Y = double.Parse(lines[c++]);
			this.CurrDrawPosRate_X = double.Parse(lines[c++]);
			this.CurrDrawPosRate_Y = double.Parse(lines[c++]);
			this.DestDrawPosRate_X = double.Parse(lines[c++]);
			this.DestDrawPosRate_Y = double.Parse(lines[c++]);
		}
	}
}
