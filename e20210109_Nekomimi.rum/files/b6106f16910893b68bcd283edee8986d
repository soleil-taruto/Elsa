using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using DxLibDLL;

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

		private DDSimpleMenu SimpleMenu;

		#region DrawWall

		private DrawWallTask DrawWall = new DrawWallTask();

		private class DrawWallTask : DDTask
		{
			public bool TopMenuLeaved = false;

			public override IEnumerable<bool> E_Task()
			{
				double leaveRate = 0.0;
				double z = 1.0;

				for (; ; )
				{
					DDUtils.Approach(ref leaveRate, this.TopMenuLeaved ? 1.0 : 0.0, 0.95);
					DDUtils.Approach(ref z, this.TopMenuLeaved ? 1.02 : 1.0, 0.9);

					DDDraw.DrawBegin(Ground.I.Picture.Title, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDDraw.DrawZoom(z);
					DDDraw.DrawEnd();

					if (0.01 < leaveRate)
					{
						ぼかし効果.Perform(leaveRate);
						DDCurtain.DrawCurtain(-0.3 * leaveRate);
					}
					yield return true;
				}
			}
		}

		#endregion

		#region TopMenu

		private TopMenuTask TopMenu = new TopMenuTask();

		private class TopMenuTask : DDTask
		{
			public const int ITEM_NUM = 4;
			public int SelectIndex = 0;

			public List<ItemTask> Items = new List<ItemTask>();
			private DDTaskList EL_Items = new DDTaskList();

			private const double ITEM_X = 200.0;
			private const double ITEM_Y = 700.0;
			private const double ITEM_Y_STEP = 100.0;
			private const double ITEM_A = 0.5;
			private const double ITEM_Z = 2.0;
			private const double ITEM_W = 400.0;
			private const double ITEM_H = 98.0;

			private const double ITEM_SEL_X = 250.0;
			private const double ITEM_SEL_A = 1.0;

			public TopMenuTask()
			{
				for (int index = 0; index < ITEM_NUM; index++)
				{
					ItemTask item = new ItemTask(index);

					this.Items.Add(item);
					this.EL_Items.Add(item.Task);
				}
			}

			public class ItemTask : DDTask
			{
				public int SelfIndex;

				public ItemTask(int selfIndex)
				{
					this.SelfIndex = selfIndex;
				}

				public bool Selected = false;

				public override IEnumerable<bool> E_Task()
				{
					DDPicture picture = Ground.I.Picture2.TitleMenuItem[0, this.SelfIndex];

					double targX = ITEM_X;
					double targA = ITEM_A;

					double x = targX;
					double y = ITEM_Y + this.SelfIndex * ITEM_Y_STEP;
					double a = targA;

					for (; ; )
					{
						targX = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_X : ITEM_X;
						targA = TitleMenu.I.TopMenu.SelectIndex == this.SelfIndex ? ITEM_SEL_A : ITEM_A;

						DDUtils.Approach(ref x, targX, 0.93);
						DDUtils.Approach(ref a, targA, 0.93);

						DDDraw.SetAlpha(a);
						DDDraw.DrawBegin(picture, x, y);
						DDDraw.DrawZoom(ITEM_Z);
						DDDraw.DrawEnd();
						DDDraw.Reset();

						this.Selected = !DDUtils.IsOut(new D2Point(DDMouse.X, DDMouse.Y), new D4Rect(x - ITEM_W / 2, y - ITEM_H / 2, ITEM_W, ITEM_H));

						if (this.Selected)
						{
							TitleMenu.I.TopMenu.SelectIndex = this.SelfIndex;
						}

						yield return true;
					}
				}

				public void マウスカーソルをここへ移動()
				{
					const double MOUSE_XY_MGN = 3.0;

					DDMouse.X = (int)(ITEM_X + ITEM_W / 2 - MOUSE_XY_MGN);
					DDMouse.Y = (int)(ITEM_Y + this.SelfIndex * ITEM_Y_STEP + ITEM_H / 2 - MOUSE_XY_MGN);

					DDMouse.PosChanged();
				}
			}

			public override IEnumerable<bool> E_Task()
			{
				for (; ; )
				{
					this.EL_Items.ExecuteAllTask();

					yield return true;
				}
			}
		}

		#endregion

		public void Perform()
		{
			DDUtils.SetMouseDispMode(true); // 2bs -- 既にマウス有効であるはず

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			//Ground.I.Music.Piano_Milkeyway.Play();

			this.SimpleMenu = new DDSimpleMenu();

			this.SimpleMenu.Color = new I3Color(255, 255, 128);
			this.SimpleMenu.BorderColor = new I3Color(0, 0, 100);
			//this.SimpleMenu.WallPicture = Ground.I.Picture.仮の背景;
			//this.SimpleMenu.WallCurtain = -0.5;
			this.SimpleMenu.WallDrawer = this.DrawWall.Execute;

			for (; ; )
			{
				bool selIdxChg = false;

				if (DDInput.DIR_8.IsPound())
				{
					selIdxChg = true;
					this.TopMenu.SelectIndex--;
				}
				if (DDInput.DIR_2.IsPound())
				{
					selIdxChg = true;
					this.TopMenu.SelectIndex++;
				}
				this.TopMenu.SelectIndex += TopMenuTask.ITEM_NUM;
				this.TopMenu.SelectIndex %= TopMenuTask.ITEM_NUM;

				if (selIdxChg)
				{
					this.TopMenu.Items[this.TopMenu.SelectIndex].マウスカーソルをここへ移動();
				}

				// ? 決定ボタン押下
				if (
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1 ||
					DDMouse.L.GetInput() == -1 && this.TopMenu.Items[this.TopMenu.SelectIndex].Selected || // アイテム以外を左クリックしても効かない様に、マウスで選択中かチェックする。
					DDInput.A.GetInput() == 1
					)
				{
					switch (this.TopMenu.SelectIndex)
					{
						case 0:
							{
								this.LeaveTitleMenu();

								using (new Game())
								{
									Game.I.Status.Scenario = new Scenario(GameConsts.FIRST_SCENARIO_NAME);
									Game.I.Perform();
								}
								this.ReturnTitleMenu();
							}
							break;

						case 1:
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
								}
							}
							break;

						case 2:
							this.DrawWall.TopMenuLeaved = true;
							this.Setting();
							this.DrawWall.TopMenuLeaved = false;
							break;

						case 3:
							goto endMenu;

						default:
							throw new DDError();
					}
				}
				// ? キャンセルボタン押下 (右クリック_以外)
				if (
					DDKey.GetInput(DX.KEY_INPUT_BACK) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_DELETE) == 1 ||
					DDInput.B.GetInput() == 1
					)
				{
					if (this.TopMenu.SelectIndex == TopMenuTask.ITEM_NUM - 1)
						break;

					this.TopMenu.SelectIndex = TopMenuTask.ITEM_NUM - 1;
				}
				// ? キャンセルボタン押下 (右クリック)
				if (DDMouse.R.GetInput() == -1)
				{
					break;
				}

				this.DrawWall.Execute();
				this.TopMenu.Execute();

				DDEngine.EachFrame();
			}
		endMenu:
			DDMusicUtils.Fade();
			DDCurtain.SetCurtain(30, -1.0);

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawWall.Execute();
				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
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

				if (selectIndex < Consts.GAME_SAVE_DATA_SLOT_NUM)
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
				"ゲームパッドのボタン設定",
				//"キーボードのキー設定",
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

					//case 1:
					//    this.SimpleMenu.PadConfig(true);
					//    break;

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
				this.DrawWall.Execute();
				DDEngine.EachFrame();
			}

			GC.Collect();
		}

		private void ReturnTitleMenu()
		{
			DDCurtain.SetCurtain();
			//Ground.I.Music.Piano_Milkeyway.Play();

			DDEngine.FreezeInput(GameConsts.LONG_INPUT_SLEEP);

			GC.Collect();
		}
	}
}
