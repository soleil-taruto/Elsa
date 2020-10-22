using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_ゆかりHead : Surface
	{
		private DDPicture[] Images = new DDPicture[]
		{
			Ground.I.Picture.未確認飛行ゆかりん_あたま0000,
			Ground.I.Picture.未確認飛行ゆかりん_あたま0001,
			Ground.I.Picture.未確認飛行ゆかりん_あたま0002,
			Ground.I.Picture.未確認飛行ゆかりん_あたま0003,
			Ground.I.Picture.未確認飛行ゆかりん_あたま0004,
		};

		public int ImageIndex = 0; // 0 ～ (Images.Length - 1)
		public double RotMag = 0.0;
		public double Zoom_X = 1.0;

		// HACK: StatusInfo, GetStatus() 冗長問題
		// -- アクション中はセーブできないという方針でどうか？
		// ---- 駄目、他にも問題がある。
		// ------ アクション中に次のページに遷移するとステータスが乱れる。
		// ------ EffOff コマンドを実行されるとステータスが乱れる。

		// ★ コマンドの組み合わせをやる場合、この Status 方式は必須となるだろう。

		// ◆◆◆ このサーフェスは Status 方式なので１つのページで複数のコマンドを組み合わせられる。

		private struct StatusInfo
		{
			public double X;
			public double Y;
			public int ImageIndex;
			public double RotMag;
			public double Zoom_X;
		}

		private StatusInfo GetStatus()
		{
			return new StatusInfo()
			{
				X = (double)this.X,
				Y = (double)this.Y,
				ImageIndex = this.ImageIndex,
				RotMag = this.RotMag,
				Zoom_X = this.Zoom_X,
			};
		}

		public override void Draw()
		{
			this.Draw(this.GetStatus());
		}

		private void Draw(StatusInfo status)
		{
			DDDraw.DrawBegin(this.Images[status.ImageIndex], status.X, status.Y);
			DDDraw.DrawSlide(0.0, 250.0);
			DDDraw.DrawZoom(0.25);
			DDDraw.DrawRotate(DDEngine.ProcFrame * status.RotMag);
			DDDraw.DrawZoom_X(status.Zoom_X);
			DDDraw.DrawEnd();
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
			else if (command == "RotMag")
			{
				this.RotMag = double.Parse(arguments[c++]);
			}
			else if (command == "Zoom_X")
			{
				this.Zoom_X = double.Parse(arguments[c++]);
			}
			else if (command == "スマッシュ")
			{
				StatusInfo status = this.GetStatus();
				double destX = double.Parse(arguments[c++]);
				double destY = double.Parse(arguments[c++]);
				int frame = int.Parse(arguments[c++]);

				this.X = SCommon.ToInt(destX);
				this.Y = SCommon.ToInt(destY);

				this.Act.Add(SCommon.Supplier(this.スマッシュ(status, destX, destY, frame)));
			}
			else if (command == "跳ねる")
			{
				StatusInfo status = this.GetStatus();
				double destX = double.Parse(arguments[c++]);
				double destY = double.Parse(arguments[c++]);
				double hi = double.Parse(arguments[c++]);
				int frame = int.Parse(arguments[c++]);

				this.X = SCommon.ToInt(destX);
				this.Y = SCommon.ToInt(destY);

				this.Act.Add(SCommon.Supplier(this.跳ねる(status, destX, destY, hi, frame)));
			}
			else if (command == "待ち")
			{
				StatusInfo status = this.GetStatus();
				int frame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.待ち(status, frame)));
			}
			else
			{
				throw new DDError();
			}
		}

		private IEnumerable<bool> スマッシュ(StatusInfo status, double destX, double destY, int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				StatusInfo tmp = status;

				tmp.X = DDUtils.AToBRate(status.X, destX, scene.Rate);
				tmp.Y = DDUtils.AToBRate(status.Y, destY, scene.Rate);

				this.Draw(tmp);
				yield return true;
			}
		}

		private IEnumerable<bool> 跳ねる(StatusInfo status, double destX, double destY, double hi, int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				StatusInfo tmp = status;

				tmp.X = DDUtils.AToBRate(status.X, destX, scene.Rate);
				tmp.Y = DDUtils.AToBRate(status.Y, destY, scene.Rate) - DDUtils.Parabola(scene.Rate) * hi;

				this.Draw(tmp);
				yield return true;
			}
		}

		private IEnumerable<bool> 待ち(StatusInfo status, int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				this.Draw(status);
				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.ImageIndex.ToString(),
				this.RotMag.ToString("F9"),
				this.Zoom_X.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.ImageIndex = int.Parse(lines[c++]);
			this.RotMag = double.Parse(lines[c++]);
			this.Zoom_X = double.Parse(lines[c++]);
		}
	}
}
