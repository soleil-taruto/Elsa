using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_ゆかりUFO : Surface
	{
		private double Real_X;
		private double Real_Y;

		public override void Draw()
		{
			this.DrawYukari(
				Math.Sin(DDEngine.ProcFrame / 71.0) * 60.0,
				Math.Sin(DDEngine.ProcFrame / 61.0) * 90.0,
				0.0,
				0.0
				);
		}

		private bool Inited = false;

		private void DrawYukari(double targSlide_x, double targSlide_y, double slide_x, double slide_y)
		{
			if (!this.Inited)
			{
				this.Inited = true;

				this.Real_X = this.X;
				this.Real_Y = this.Y;
			}

			DDUtils.Approach(ref this.Real_X, this.X + targSlide_x, 0.99);
			DDUtils.Approach(ref this.Real_Y, this.Y + targSlide_y, 0.99);

			double x = this.Real_X + slide_x;
			double y = this.Real_Y + slide_y;
			double z = 0.5;

			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_からだ, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 1.3) * 0.03);
			DDDraw.DrawZoom(z);
			DDDraw.DrawEnd();

			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_あたま0000, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 0.4) * 0.03);
			DDDraw.DrawZoom(z);
			DDDraw.DrawEnd();

			DDDraw.DrawBegin(Ground.I.Picture.未確認飛行ゆかりん_UFO, x, y);
			DDDraw.DrawRotate(Math.Sin(DDEngine.ProcFrame / 17.0 - 0.0) * 0.03);
			DDDraw.DrawZoom(z);
			DDDraw.DrawEnd();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			//int c = 0;

			if (command == "跳ねて登場")
			{
				this.Act.Add(SCommon.Supplier(this.跳ねて登場()));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> 跳ねて登場()
		{
			foreach (var info in new[]
			{
				new { f = 60, hi = 100.0 },
				new { f = 40, hi = 75.0 },
				new { f = 30, hi = 50.0 },
			})
			{
				foreach (DDScene scene in DDSceneUtils.Create(info.f))
				{
					this.DrawYukari(
						0.0,
						0.0,
						0.0,
						-DDUtils.Parabola(scene.Rate) * info.hi
						);

					yield return true;
				}
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Real_X.ToString("F9"),
				this.Real_Y.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Real_X = double.Parse(lines[c++]);
			this.Real_Y = double.Parse(lines[c++]);
		}
	}
}
