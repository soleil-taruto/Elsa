using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games.Surfaces
{
	public class Surface_スクリーン : Surface
	{
		private string ImageFile = null; // null == 画像無し
		private double A = 1.0;
		private double SlideRate = 0.5;
		private double DestSlideRate = 0.5;

		public Surface_スクリーン(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 10000;
		}

		private struct StatusInfo
		{
			public double A;
		}

		private StatusInfo GetStatus()
		{
			return new StatusInfo()
			{
				A = this.A,
			};
		}

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				this.Draw(this.GetStatus());

				yield return true;
			}
		}

		private void Draw(StatusInfo status)
		{
			if (this.ImageFile == null) // ? 画像無し
				return;

			DDUtils.Approach(ref this.SlideRate, this.DestSlideRate, 0.9999);

			DDPicture image = DDCCResource.GetPicture(this.ImageFile);
			D2Size size = DDUtils.AdjustRectExterior(image.GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H)).Size;

			DDDraw.SetAlpha(status.A);
			DDDraw.DrawRect(
				image,
				(DDConsts.Screen_W - size.W) * this.SlideRate,
				(DDConsts.Screen_H - size.H) * this.SlideRate,
				size.W,
				size.H
				);
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "画像")
			{
				this.ImageFile = arguments[c++];
			}
			else if (command == "スライド")
			{
				this.SlideRate = double.Parse(arguments[c++]);
				this.DestSlideRate = double.Parse(arguments[c++]);
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン(this.GetStatus())));
				this.A = 1.0;
			}
			else if (command == "フェードアウト")
			{
				this.Act.Add(SCommon.Supplier(this.フェードアウト(this.GetStatus())));
				this.A = 0.0;
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> フェードイン(StatusInfo status)
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				status.A = scene.Rate;

				this.Draw(status);
				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト(StatusInfo status)
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				status.A = 1.0 - scene.Rate;

				this.Draw(status);
				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				Common.WrapNullOrString(this.ImageFile),
				this.A.ToString("F9"),
				this.SlideRate.ToString("F9"),
				this.DestSlideRate.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.ImageFile = Common.UnwrapNullOrString(lines[c++]);
			this.A = double.Parse(lines[c++]);
			this.SlideRate = double.Parse(lines[c++]);
			this.DestSlideRate = double.Parse(lines[c++]);
		}
	}
}
