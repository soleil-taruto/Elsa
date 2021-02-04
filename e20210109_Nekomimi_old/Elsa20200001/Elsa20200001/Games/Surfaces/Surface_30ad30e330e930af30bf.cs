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
		public double Draw_Rnd = DDUtils.Random.Real() * Math.PI * 2.0;

		public static string[] CHARA_NAMES = new string[]
		{
			"こもも",
			"リル",
		};

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
			new ImageInfo[] // こもも
			{
				new ImageInfo("普", Ground.I.Picture.こもも),
			},
			new ImageInfo[] // リル
			{
				new ImageInfo("普", Ground.I.Picture.リル),
			},
		};

		public int Chara = 0; // こもも
		public int Mode = 0; // 普
		public double A = 1.0;
		public double Zoom = 1.0;
		public double XZoom = 1.0;

		public Surface_キャラクタ(string typeName, string instanceName)
			: base(typeName, instanceName)
		{
			this.Z = 20000;
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
			const double BASIC_ZOOM = 1.0;

			DDDraw.SetAlpha(this.A);
			DDDraw.DrawBegin(this.ImageTable[(int)this.Chara][this.Mode].Image, this.X, this.Y + Math.Sin(DDEngine.ProcFrame / 67.0 + this.Draw_Rnd) * 2.0);
			DDDraw.DrawZoom(BASIC_ZOOM * this.Zoom);
			DDDraw.DrawZoom_X(this.XZoom);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "Chara")
			{
				this.Act.AddOnce(() =>
				{
					string charaName = arguments[c++];
					int chara = SCommon.IndexOf(CHARA_NAMES, charaName);

					if (chara == -1)
						throw new DDError("Bad chara: " + charaName);

					this.Chara = chara;
				});
			}
			else if (command == "Mode")
			{
				this.Act.AddOnce(() =>
				{
					string modeName = arguments[c++];
					int mode = SCommon.IndexOf(this.ImageTable[this.Chara], v => v.Name == modeName);

					if (mode == -1)
						throw new DDError("Bad mode: " + mode);

					this.Mode = mode;
				});
			}
			else if (command == "A")
			{
				this.Act.AddOnce(() => this.A = double.Parse(arguments[c++]));
			}
			else if (command == "Zoom")
			{
				this.Act.AddOnce(() => this.Zoom = double.Parse(arguments[c++]));
			}
			else if (command == "XZoom")
			{
				this.Act.AddOnce(() => this.XZoom = double.Parse(arguments[c++]));
			}
			else if (command == "待ち")
			{
				this.Act.Add(SCommon.Supplier(this.待ち(int.Parse(arguments[c++]))));
			}
			else if (command == "フェードイン")
			{
				this.Act.Add(SCommon.Supplier(this.フェードイン()));
			}
			else if (command == "フェードアウト")
			{
				this.Act.Add(SCommon.Supplier(this.フェードアウト()));
			}
			else if (command == "モード変更")
			{
				this.Act.Add(SCommon.Supplier(this.モード変更(arguments[c++])));
			}
			else if (command == "スライド")
			{
				double x = double.Parse(arguments[c++]);
				double y = double.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.スライド(x, y)));
			}
			else if (command == "はてな")
			{
				this.Act.AddOnce(() => DDGround.EL.Add(SCommon.Supplier(this.E_はてな())));
			}
			else if (command == "ジャンプ")
			{
				double x = double.Parse(arguments[c++]);
				double y = double.Parse(arguments[c++]);
				double height = double.Parse(arguments[c++]);
				int frame = int.Parse(arguments[c++]);

				this.Act.Add(SCommon.Supplier(this.ジャンプ(x, y, height, frame)));
			}
			else
			{
				ProcMain.WriteLog(command);
				throw new DDError();
			}
		}

		private IEnumerable<bool> 待ち(int frame)
		{
			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				if (Act.IsFlush)
					yield break;

				this.P_Draw();
				yield return true;
			}
		}

		private IEnumerable<bool> フェードイン()
		{
			foreach (DDScene scene in DDSceneUtils.Create(10))
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
			foreach (DDScene scene in DDSceneUtils.Create(10))
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

		private IEnumerable<bool> モード変更(string modeName)
		{
			int mode = SCommon.IndexOf(this.ImageTable[this.Chara], v => v.Name == modeName);

			if (mode == -1)
				throw new DDError("Bad mode: " + mode);

			int currMode = this.Mode;
			int destMode = mode;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					this.A = 1.0;
					this.Mode = destMode;

					yield break;
				}
				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.5);
				this.Mode = currMode;
				this.P_Draw();

				this.A = DDUtils.Parabola(scene.Rate * 0.5 + 0.0);
				this.Mode = destMode;
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> スライド(double x, double y)
		{
			double currX = this.X;
			double destX = x;
			double currY = this.Y;
			double destY = y;

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					this.X = destX;
					this.Y = destY;

					yield break;
				}
				this.X = DDUtils.AToBRate(currX, destX, DDUtils.SCurve(scene.Rate));
				this.Y = DDUtils.AToBRate(currY, destY, DDUtils.SCurve(scene.Rate));
				this.P_Draw();

				yield return true;
			}
		}

		private IEnumerable<bool> E_はてな()
		{
			double x = this.X + 200.0;
			double y = this.Y - 650.0;

			Action onFlush = () => { };

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					onFlush();
					yield break;
				}
				DDDraw.DrawCenter(
					Ground.I.Picture.はてな,
					x,
					y - 60.0 * DDUtils.Parabola(scene.Rate)
					);

				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				if (Act.IsFlush)
				{
					onFlush();
					yield break;
				}
				DDDraw.DrawCenter(
					Ground.I.Picture.はてな,
					x,
					y - 30.0 * DDUtils.Parabola(scene.Rate)
					);

				yield return true;
			}
			foreach (DDScene scene in DDSceneUtils.Create(20))
			{
				if (Act.IsFlush)
				{
					onFlush();
					yield break;
				}
				DDDraw.SetAlpha(1.0 - scene.Rate);
				DDDraw.DrawCenter(
					Ground.I.Picture.はてな,
					x,
					y
					);
				DDDraw.Reset();

				yield return true;
			}
		}

		private IEnumerable<bool> ジャンプ(double x, double y, double height, int frame)
		{
			double currX = this.X;
			double destX = x;
			double currY = this.Y;
			double destY = y;

			foreach (DDScene scene in DDSceneUtils.Create(frame))
			{
				if (Act.IsFlush)
				{
					this.X = destX;
					this.Y = destY;

					yield break;
				}
				this.X = DDUtils.AToBRate(currX, destX, scene.Rate);
				this.Y = DDUtils.AToBRate(currY, destY, scene.Rate) - height * DDUtils.Parabola(scene.Rate);
				this.P_Draw();

				yield return true;
			}
		}

		protected override string[] Serialize_02()
		{
			return new string[]
			{
				this.Chara.ToString(),
				this.Mode.ToString(),
				this.A.ToString("F9"),
				this.Zoom.ToString("F9"),
			};
		}

		protected override void Deserialize_02(string[] lines)
		{
			int c = 0;

			this.Chara = int.Parse(lines[c++]);
			this.Mode = int.Parse(lines[c++]);
			this.A = double.Parse(lines[c++]);
			this.Zoom = double.Parse(lines[c++]);
		}
	}
}
