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
		private double Zoom = 1.0;
		private double DestZoom = 1.0;

		public Surface_スクリーン(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 10000;
		}

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				this.P_Draw();

				yield return true;
			}
		}

		private void P_Draw()
		{
			if (this.ImageFile == null) // ? 画像無し
				return;

			DDUtils.Approach(ref this.SlideRate, this.DestSlideRate, 0.9999);
			DDUtils.Approach(ref this.Zoom, this.DestZoom, 0.9999);

			DDPicture picture = DDCCResource.GetPicture(this.ImageFile);
			D2Size size = DDUtils.AdjustRectExterior(picture.GetSize().ToD2Size(), new D4Rect(0, 0, DDConsts.Screen_W, DDConsts.Screen_H)).Size;

			size *= this.Zoom;

			DDDraw.SetAlpha(this.A);
			DDDraw.DrawRect(
				picture,
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
				this.Act.AddOnce(() => this.ImageFile = arguments[c++]);
			}
			else if (command == "スライド")
			{
				this.Act.AddOnce(() =>
				{
					this.SlideRate = double.Parse(arguments[c++]);
					this.DestSlideRate = double.Parse(arguments[c++]);
				});
			}
			else if (command == "ズーム")
			{
				this.Act.AddOnce(() =>
				{
					this.Zoom = double.Parse(arguments[c++]);
					this.DestZoom = double.Parse(arguments[c++]);
				});
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン()));
			}
			else if (command == "フェードアウト")
			{
				this.Act.Add(SCommon.Supplier(this.フェードアウト()));
			}
			else if (command == "遅延フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.遅延フェードイン(int.Parse(arguments[c++]))));
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
				if (Act.IsFlush)
				{
					this.A = 1.0;
					yield break;
				}
				this.A = scene.Rate;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト()
		{
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				if (Act.IsFlush)
				{
					this.A = 0.0;
					yield break;
				}
				this.A = 1.0 - scene.Rate;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> 遅延フェードイン(int delayFrame)
		{
			Action onFlush = () =>
			{
				this.A = 1.0;
			};

			foreach (DDScene scene in DDSceneUtils.Create(delayFrame))
			{
				if (Act.IsFlush)
				{
					onFlush();
					yield break;
				}
				this.P_Draw();

				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				if (Act.IsFlush)
				{
					onFlush();
					yield break;
				}
				this.A = 1.0 - scene.Rate;
				this.P_Draw();

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
