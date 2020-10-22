using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_キャラクタ : Surface
	{
		public double Draw_Rnd = DDUtils.Random.Real2() * Math.PI * 2.0;

		public enum Chara_t
		{
			結月ゆかり,
			東北ずん子,
			弦巻マキ,
		}

		private class ImageInfo
		{
			public string Name;
			public DDPicture Image;

			public ImageInfo(string name, DDPicture image)
			{
				this.Name = name;
				this.Image = image;
			}
		}

		private ImageInfo[][] ImageTable = new ImageInfo[][]
		{
			new ImageInfo[] // 結月ゆかり
			{
				new ImageInfo("制服_珈琲", Ground.I.Picture.結月ゆかり01),
				new ImageInfo("制服_困惑", Ground.I.Picture.結月ゆかり02),
				new ImageInfo("制服_喜", Ground.I.Picture.結月ゆかり03),
				new ImageInfo("制服_驚", Ground.I.Picture.結月ゆかり04),
				new ImageInfo("制服_鶏肉", Ground.I.Picture.結月ゆかり05),
				new ImageInfo("怒", Ground.I.Picture.結月ゆかり11),
				new ImageInfo("寝", Ground.I.Picture.結月ゆかり12),
				new ImageInfo("困惑", Ground.I.Picture.結月ゆかり13),
				new ImageInfo("喜", Ground.I.Picture.結月ゆかり14),
				new ImageInfo("衝撃", Ground.I.Picture.結月ゆかり15),
				new ImageInfo("普", Ground.I.Picture.結月ゆかり16),
			},
			new ImageInfo[] // 東北ずん子
			{
				new ImageInfo("喜", Ground.I.Picture.東北ずん子01),
				new ImageInfo("怒", Ground.I.Picture.東北ずん子02),
				new ImageInfo("困惑", Ground.I.Picture.東北ずん子03),
				new ImageInfo("餅", Ground.I.Picture.東北ずん子04),
				new ImageInfo("着物_普", Ground.I.Picture.東北ずん子05),
				new ImageInfo("着物_怒", Ground.I.Picture.東北ずん子06),
				new ImageInfo("着物_なまはげ", Ground.I.Picture.東北ずん子07),
				new ImageInfo("着物_困惑", Ground.I.Picture.東北ずん子08),
				new ImageInfo("着物_疑問", Ground.I.Picture.東北ずん子09),
				new ImageInfo("激怒", Ground.I.Picture.東北ずん子10),
				new ImageInfo("メイド", Ground.I.Picture.東北ずん子11),
			},
			new ImageInfo[] // 弦巻マキ
			{
				new ImageInfo("制服_怒", Ground.I.Picture.弦巻マキ01),
				new ImageInfo("制服_激怒", Ground.I.Picture.弦巻マキ02),
				new ImageInfo("制服_泣", Ground.I.Picture.弦巻マキ03),
				new ImageInfo("普", Ground.I.Picture.弦巻マキ04),
				new ImageInfo("泣", Ground.I.Picture.弦巻マキ05),
				new ImageInfo("喜", Ground.I.Picture.弦巻マキ06),
			},
		};

		public Chara_t Chara = Chara_t.結月ゆかり;
		public int Mode = 0;
		public double A = 1.0;
		public double Zoom = 1.0;

		private struct StatusInfo
		{
			public double X;
			public double Y;
			//public int Z; // Z-オーダーは制御出来ない。
			public Chara_t Chara;
			public int Mode;
			public double A;
			public double Zoom;
		}

		private StatusInfo GetStatus()
		{
			return new StatusInfo()
			{
				X = this.X,
				Y = this.Y,
				Chara = this.Chara,
				Mode = this.Mode,
				A = this.A,
				Zoom = this.Zoom,
			};
		}

		public Surface_キャラクタ()
		{
			this.Z = 20000;
		}

		public override void Draw()
		{
			this.Draw(this.GetStatus());
		}

		private void Draw(StatusInfo status)
		{
			const double BASIC_ZOOM = 1.0;

			DDDraw.SetAlpha(status.A);
			DDDraw.DrawBegin(this.ImageTable[(int)status.Chara][status.Mode].Image, status.X, status.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + this.Draw_Rnd) * 2.0);
			DDDraw.DrawZoom(BASIC_ZOOM * status.Zoom);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "Chara")
			{
				string charaName = arguments[c++];
				int chara = SCommon.IndexOf(Enum.GetNames(typeof(Chara_t)), charaName);

				if (chara == -1)
					throw new DDError("Bad chara: " + charaName);

				this.Chara = (Chara_t)chara;
			}
			else if (command == "Mode")
			{
				string modeName = arguments[c++];
				int mode = SCommon.IndexOf(this.ImageTable[(int)this.Chara], v => v.Name == modeName);

				if (mode == -1)
					throw new DDError("Bad mode: " + mode);

				this.Mode = mode;
			}
			else if (command == "A")
			{
				this.A = double.Parse(arguments[c++]);
			}
			else if (command == "Zoom")
			{
				this.Zoom = double.Parse(arguments[c++]);
			}
			else if (command == "待ち")
			{
				int frame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.待ち(this.GetStatus(), frame)));
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
			else if (command == "モード変更")
			{
				string modeName = arguments[c++];
				int mode = SCommon.IndexOf(this.ImageTable[(int)this.Chara], v => v.Name == modeName);

				if (mode == -1)
					throw new DDError("Bad mode: " + mode);

				this.Act.Add(SCommon.Supplier(this.モード変更(this.GetStatus(), mode)));
				this.Mode = mode;
			}
			else if (command == "スライド")
			{
				double x = double.Parse(arguments[c++]);
				double y = double.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド(this.GetStatus(), x, y)));
				this.X = SCommon.ToInt(x);
				this.Y = SCommon.ToInt(y);
			}
			else
			{
				ProcMain.WriteLog(command);
				throw new DDError();
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

		private IEnumerable<bool> フェードイン(StatusInfo status)
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				status.A = scene.Rate;

				this.Draw(status);
				yield return true;
			}
		}

		private IEnumerable<bool> フェードアウト(StatusInfo status)
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
			{
				status.A = 1.0 - scene.Rate;

				this.Draw(status);
				yield return true;
			}
		}

		private IEnumerable<bool> モード変更(StatusInfo status, int destMode)
		{
			StatusInfo currStatus = status;
			StatusInfo destStatus = status;

			destStatus.Mode = destMode;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				currStatus.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.5);
				destStatus.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.0);

				this.Draw(currStatus);
				this.Draw(destStatus);
				yield return true;
			}
		}

		private IEnumerable<bool> スライド(StatusInfo status, double x, double y)
		{
			StatusInfo currStatus = status;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				currStatus.X = DDUtils.AToBRate(status.X, x, DDUtils.SCurve(scene.Rate));
				currStatus.Y = DDUtils.AToBRate(status.Y, y, DDUtils.SCurve(scene.Rate));

				this.Draw(currStatus);
				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				"" + (int)this.Chara,
				this.Mode.ToString(),
				this.A.ToString("F9"),
				this.Zoom.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Chara = (Chara_t)int.Parse(lines[c++]);
			this.Mode = int.Parse(lines[c++]);
			this.A = double.Parse(lines[c++]);
			this.Zoom = double.Parse(lines[c++]);
		}
	}
}
