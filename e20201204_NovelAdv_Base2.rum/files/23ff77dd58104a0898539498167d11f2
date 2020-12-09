using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;

namespace Charlotte.Games
{
	public class TitleMenu : IDisposable
	{
		public static TitleMenu I;

		public TitleMenu()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private DDSimpleMenu SimpleMenu = new DDSimpleMenu()
		{
			Color = new I3Color(255, 255, 128),
			BorderColor = new I3Color(0, 0, 100),
			WallPicture = Ground.I.Picture.星屑物語05,
			WallCurtain = -0.5,
		};

		private double YukariYureHaba = 0.0;
		private bool MenuItemShown;

		public void Perform()
		{
			DDUtils.SetMouseDispMode(true);

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			Ground.I.Music.Piano_Milkeyway.Play(); // Logo.cs で再生している。念の為ここでも再生しておく。

			this.DrawWall_INIT();

		restart:
			this.MenuItemShown = false;

			S_TipString tipClickScreen = new S_TipString()
			{
				Title = "CLICK ON THE SCREEN",
				Color = new I3Color(220, 220, 255),
				BorderColor = new I3Color(0, 0, 128),
			};

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			foreach (DDScene scene in DDSceneUtils.Create(900))
			{
				if (DDMouse.L.GetInput() == -1)
					break;

				this.DrawWall();
				tipClickScreen.Draw(DDConsts.Screen_W - 240, DDConsts.Screen_H - 60, Math.Min(scene.Numer - 300, (scene.Denom - scene.Numer) - 30));
				DDEngine.EachFrame();
			}
			this.MenuItemShown = true;

			S_MenuItemInfo menuItemStart = new S_MenuItemInfo()
			{
				X = 800.0,
				Y = 860.0,
				Slide_X = 0.0,
				Slide_Y = -90.0,
				YureCyc_X = 101.0,
				YureCyc_Y = 103.0,
				Picture = Ground.I.Picture.ゆかりステッカー純,
				TipString = new S_TipString()
				{
					Title = "GAME START",
					Color = new I3Color(255, 220, 255),
					BorderColor = new I3Color(128, 0, 128),
				},
			};

			S_MenuItemInfo menuItemContinue = new S_MenuItemInfo()
			{
				X = 1250.0,
				Y = 860.0,
				Slide_X = 0.0,
				Slide_Y = 0.0,
				YureCyc_X = 107.0,
				YureCyc_Y = 109.0,
				Picture = Ground.I.Picture.ゆかりステッカー穏,
				TipString = new S_TipString()
				{
					Title = "CONTINUE",
					Color = new I3Color(255, 220, 220),
					BorderColor = new I3Color(128, 0, 0),
				},
			};

			S_MenuItemInfo menuItemGoodbye = new S_MenuItemInfo()
			{
				X = 1700.0,
				Y = 860.0,
				Slide_X = 0.0,
				Slide_Y = -10.0,
				YureCyc_X = 113.0,
				YureCyc_Y = 127.0,
				Picture = Ground.I.Picture.ゆかりステッカー凛,
				TipString = new S_TipString()
				{
					Title = "GOOD-BYE",
					Color = new I3Color(220, 220, 255),
					BorderColor = new I3Color(0, 0, 128),
				},
			};

			S_MenuItemInfo menuItemSetting = new S_MenuItemInfo()
			{
				X = 1700.0,
				Y = 360.0,
				SpecZ = 0.5,
				Slide_X = 0.0,
				Slide_Y = 0.0,
				YureCyc_X = 131.0,
				YureCyc_Y = 137.0,
				Picture = Ground.I.Picture.ゆかりステッカーうさ01,
				TipString = new S_TipString()
				{
					Title = "SETTING",
					Color = new I3Color(255, 255, 220),
					BorderColor = new I3Color(128, 128, 0),
				},
			};

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			for (; ; )
			{
				if (DDMouse.L.GetInput() == -1)
				{
					if (menuItemStart.MouseCrashedFrame != 0)
					{
						this.LeaveTitleMenu();

						using (new Game())
						{
							Game.I.Status.Scenario = new Scenario(GameConsts.FIRST_SCENARIO_NAME);
							Game.I.Perform();
						}
						this.ReturnTitleMenu();
						goto restart;
					}
					else if (menuItemContinue.MouseCrashedFrame != 0)
					{
						string savedData = this.LoadGame();

						if (savedData != null)
						{
							this.LeaveTitleMenu();

							using (new Game())
							{
								Game.I.Status = GameStatus.Deserialize(savedData);
								Game.I.Perform(true);
							}
							this.ReturnTitleMenu();
							goto restart;
						}
					}
					else if (menuItemGoodbye.MouseCrashedFrame != 0)
					{
						goto endMenu;
					}
					else if (menuItemSetting.MouseCrashedFrame != 0)
					{
						this.Setting();
					}
					else if (DDMouse.X < 600.0)
					{
						this.YukariYureHaba = 200.0;
						Ground.I.SE.Poka02.Play();
					}
					else
					{
						goto restart;
					}
				}

				this.DrawWall();

				menuItemStart.Draw();
				menuItemContinue.Draw();
				menuItemGoodbye.Draw();
				menuItemSetting.Draw();

				DDEngine.EachFrame();
			}
		endMenu:
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		private class S_MenuItemInfo
		{
			public double X;
			public double Y;
			public double SpecZ = 1.0;
			public double Slide_X;
			public double Slide_Y;
			public double YureCyc_X;
			public double YureCyc_Y;
			public DDPicture Picture;
			public S_TipString TipString;

			// <---- prm

			public int MouseCrashedFrame = 0;

			private double Z = 0.1;
			private double R = 0.0;

			public void Draw()
			{
				if (DDUtils.IsCrashed_Rect_Point(new D4Rect(this.X - 220.0, this.Y - 220.0, 440.0, 440.0), new D2Point(DDMouse.X, DDMouse.Y)))
					this.MouseCrashedFrame++;
				else
					this.MouseCrashedFrame = 0;

				if (this.MouseCrashedFrame != 0)
					DDUtils.Approach(ref this.Z, 2.2, 0.8);
				else
					DDUtils.Approach(ref this.Z, 1.8, 0.9);

				if (this.MouseCrashedFrame == 1)
					this.R = 15.0;

				DDUtils.Approach(ref this.R, 0.0, 0.7);

				DDDraw.DrawBegin(
					this.Picture,
					this.X + Math.Sin(DDEngine.ProcFrame / this.YureCyc_X) * 10.0,
					this.Y + Math.Sin(DDEngine.ProcFrame / this.YureCyc_Y) * 10.0
					);
				DDDraw.DrawSlide(this.Slide_X, this.Slide_Y);
				DDDraw.DrawZoom(this.Z * this.SpecZ);
				DDDraw.DrawRotate(this.R);
				DDDraw.DrawEnd();

				if (this.MouseCrashedFrame != 0)
				{
					this.TipString.Draw(
						this.X,
						this.Y - 180.0,
						this.MouseCrashedFrame
						);
				}
			}
		}

		private class S_TipString
		{
			public string Title;
			public I3Color Color;
			public I3Color BorderColor;

			// <---- prm

			public void Draw(double x, double y, int frame)
			{
				if (frame < 0)
					return;

				int printLen = frame / 3;
				string printTitle;

				if (printLen < this.Title.Length)
					printTitle = this.Title.Substring(0, printLen) + "_";
				else
					printTitle = this.Title + ((frame / 10) % 2 == 0 ? "_" : " ");

				DDPrint.SetColor(this.Color);
				DDPrint.SetBorder(this.BorderColor);
				DDPrint.SetPrint((int)x, (int)y);
				DDPrint.Print(printTitle, 0.5);
				DDPrint.Reset();
			}
		}

		private string LoadGame()
		{
			string savedData = null;

			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = Ground.I.SaveDataSlots.Select(v => v == null ? "[no-data]" : SCommon.Hex.ToString(SCommon.GetSHA512(Encoding.UTF8.GetBytes(v))).Substring(0, 20)).Concat(new string[] { "戻る" }).ToArray();

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("コンテニュー(仮)", items, selectIndex);

				if (selectIndex < GameConsts.SAVE_DATA_SLOT_NUM)
				{
					if (Ground.I.SaveDataSlots[selectIndex] != null)
					{
						savedData = Ground.I.SaveDataSlots[selectIndex];
						break;
					}
				}
				else // [戻る]
				{
					break;
				}
			}
			DDEngine.FreezeInput();

			return savedData;
		}

		private void Setting()
		{
			DDCurtain.SetCurtain();
			DDEngine.FreezeInput();

			string[] items = new string[]
			{
				"パッドのボタン設定",
				"ウィンドウサイズ変更",
				"ＢＧＭ音量",
				"ＳＥ音量",
				"戻る",
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = this.SimpleMenu.Perform("設定メニュー(仮)", items, selectIndex);

				switch (selectIndex)
				{
					case 0:
						this.SimpleMenu.PadConfig();
						break;

					case 1:
						this.SimpleMenu.WindowSizeConfig();
						break;

					case 2:
						this.SimpleMenu.VolumeConfig("ＢＧＭ音量", DDGround.MusicVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.MusicVolume = volume;
							DDMusicUtils.UpdateVolume();
						},
						() => { }
						);
						break;

					case 3:
						this.SimpleMenu.VolumeConfig("ＳＥ音量", DDGround.SEVolume, 0, 100, 1, 10, volume =>
						{
							DDGround.SEVolume = volume;
							DDSEUtils.UpdateVolume();
						},
						() =>
						{
							Ground.I.SE.Poka01.Play();
						}
						);
						break;

					case 4:
						goto endMenu;

					default:
						throw new DDError();
				}
			}
		endMenu:
			DDEngine.FreezeInput();
		}

		private void LeaveTitleMenu()
		{
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall();
				DDEngine.EachFrame();
			}

			GC.Collect();
		}

		private void ReturnTitleMenu()
		{
			DDCurtain.SetCurtain();
			Ground.I.Music.Piano_Milkeyway.Play();

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			GC.Collect();
		}

		private DDTaskList DrawWallTL = new DDTaskList();

		private void DrawWall()
		{
			this.DrawWallTL.ExecuteAllTask();
		}

		private void DrawWall_INIT()
		{
			this.DrawWallTL.Add(SCommon.Supplier(this.T_DrawWall()));
			this.DrawWallTL.Add(SCommon.Supplier(this.T_DrawWall2()));
			this.DrawWallTL.Add(SCommon.Supplier(this.T_DrawWall3()));
			this.DrawWallTL.Add(SCommon.Supplier(this.T_DrawYukari()));
			this.DrawWallTL.Add(SCommon.Supplier(this.T_DrawLogo()));
		}

		private IEnumerable<bool> T_DrawWall()
		{
			double x = 0.0;
			double y = 0.0;
			double z = 6.0;
			double r = 100.0;

			for (int frame = 0; ; frame++)
			{
				DDUtils.Approach(ref x, DDConsts.Screen_W / 2.0, 0.97);
				DDUtils.Approach(ref y, DDConsts.Screen_H / 2.0, 0.98);

				if (!this.MenuItemShown || frame < 900)
					DDUtils.Approach(ref z, 2.0, 0.99);
				else
					DDUtils.Approach(ref z, 4.0, 0.9999);

				DDUtils.Approach(ref r, 0.0, 0.9);

				DDDraw.DrawBegin(Ground.I.Picture.星屑物語11, x, y);
				DDDraw.DrawZoom(z);
				DDDraw.DrawRotate(r);
				DDDraw.DrawEnd();

				yield return true;
			}
		}

		private IEnumerable<bool> T_DrawWall2()
		{
			for (int c = 0; c < 70; c++)
				yield return true;

			double h = 10.0;
			double a = 0.0;

			for (; ; )
			{
				if (!this.MenuItemShown)
				{
					DDUtils.Approach(ref h, 100.0, 0.93);
					DDUtils.Approach(ref a, 0.7, 0.97);
				}
				else
				{
					DDUtils.Approach(ref h, 10.0, 0.95);
					DDUtils.Approach(ref a, 0.0, 0.95);
				}

				DDDraw.SetBright(0, 0, 0);
				DDDraw.SetAlpha(a);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0.0, 0.0, DDConsts.Screen_W, h);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0.0, DDConsts.Screen_H - h, DDConsts.Screen_W, h);
				DDDraw.Reset();

				yield return true;
			}
		}

		private IEnumerable<bool> T_DrawWall3()
		{
			double a = 0.0;

			for (; ; )
			{
				double target_a;

				if (!this.MenuItemShown)
					target_a = 0.0;
				else
					target_a = -0.7;

				DDUtils.Approach(ref a, target_a, 0.95);

				DDCurtain.DrawCurtain(a);
				yield return true;
			}
		}

		private IEnumerable<bool> T_DrawYukari()
		{
			for (int c = 0; c < 30; c++)
				yield return true;

			double x = 800.0;
			double y = 900.0;
			double z = 2.4;
			double a = 0.0;

			for (; ; )
			{
				if (!this.MenuItemShown)
					DDUtils.Approach(ref x, 500.0, 0.993);
				else
					DDUtils.Approach(ref x, 400.0, 0.9);

				DDUtils.Approach(ref y, 700.0, 0.994);
				DDUtils.Approach(ref z, 1.4, 0.98);
				DDUtils.Approach(ref a, 1.0, 0.93);
				DDUtils.Approach(ref this.YukariYureHaba, 0.0, 0.9);

				DDDraw.SetAlpha(a);
				DDDraw.DrawBegin(Ground.I.Picture.結月ゆかり01, x, y);
				DDDraw.DrawZoom(z);
				DDDraw.DrawSlide(
					(DDUtils.Random.Real() - 0.5) * this.YukariYureHaba,
					(DDUtils.Random.Real() - 0.5) * this.YukariYureHaba
					);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}

		private IEnumerable<bool> T_DrawLogo()
		{
			for (int c = 0; c < 50; c++)
				yield return true;

			double x = 1600.0;
			double y = 1000.0;
			double z = 1.6;
			double a = 0.0;
			double r = 100.0;

			for (; ; )
			{
				if (!this.MenuItemShown)
				{
					DDUtils.Approach(ref x, 1400.0, 0.93);
					DDUtils.Approach(ref y, 540.0, 0.97);
					DDUtils.Approach(ref z, 2.4, 0.99);
					DDUtils.Approach(ref a, 1.0, 0.96);
				}
				else
				{
					DDUtils.Approach(ref x, 1060.0, 0.9);
					DDUtils.Approach(ref y, 400.0, 0.9);
					DDUtils.Approach(ref z, 2.0, 0.9);
					DDUtils.Approach(ref a, 0.7, 0.9);
				}
				DDUtils.Approach(ref r, 0.0, 0.95);

				DDDraw.SetAlpha(a);
				DDDraw.DrawBegin(Ground.I.Picture.ゆかりステッカーLogo, x, y);
				DDDraw.DrawZoom(z);
				DDDraw.DrawRotate(r);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				yield return true;
			}
		}
	}
}
