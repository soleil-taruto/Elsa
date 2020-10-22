using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.Games.Surfaces;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public GameStatus Status = new GameStatus(); // 軽量な仮設オブジェクト
		public bool LoadedFlag = false;

		// <---- prm

		public static Game I;

		public DDTaskList SurfaceEL;

		public Game()
		{
			I = this;

			this.SurfaceEL = new DDTaskList();
		}

		public void Dispose()
		{
			this.SurfaceEL = null;

			I = null;
		}

		/// <summary>
		/// 現在のページのテキストを表示しきってから次ページへ遷移させないための入力抑止フレーム数
		/// </summary>
		private const int NEXT_PAGE_KEY_INTERVAL = 10;

		/// <summary>
		/// 過去ログに入った時・出た時にホイールの操作をさせないための入力抑止フレーム数
		/// </summary>
		private const int BACKLOG_SHITA_KORO_SLEEP = 5;

		/// <summary>
		/// メニューから戻ってきたときの入力抑止フレーム数
		/// </summary>
		private const int MENU_RETURN_SLEEP = 30;

		/// <summary>
		/// 自動モードで次ページへ遷移するまでのフレーム数
		/// </summary>
		private const int AUTO_NEXT_PAGE_INTERVAL = 180;

		private ScenarioPage CurrPage;
		private int SelectedSystemButtonIndex = -1; // -1 == システムボタン未選択

		public void Perform()
		{
			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

			bool skipMode = false;
			bool autoMode = false;

		restartCurrPage:
			this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];

			if (!this.LoadedFlag)
				foreach (ScenarioCommand command in this.CurrPage.Commands)
					command.Invoke();

			int dispSubtitleCharCount = 0;
			int dispCharCount = 0;
			int dispPageEndedCount = 0;
			bool dispFastMode = false;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				bool nextPageFlag = false;

				// ★★★ キー押下は 1 マウス押下は -1 で判定する。

				// 入力：シナリオを進める。(マウスホイール)
				if (DDMouse.Rot < 0)
				{
					if (dispPageEndedCount < NEXT_PAGE_KEY_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						dispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (this.Status.CurrSelect == null)
							nextPageFlag = true;
					}
					DDEngine.FreezeInput(BACKLOG_SHITA_KORO_SLEEP);
				}

				// 入力：シナリオを進める。(マウスホイール_以外)
				if (
					DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex == -1 || // システムボタン以外を左クリック
					DDInput.A.GetInput() == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1
					)
				{
					if (dispPageEndedCount < NEXT_PAGE_KEY_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						dispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (this.Status.CurrSelect == null)
						{
							nextPageFlag = true;
						}
						else // 選択肢表示中
						{
							int index = this.Status.CurrSelect.GetMouseFocusedIndex();

							if (index != -1) // 選択中の選択肢へ進む
							{
								string scenarioName = this.Status.CurrSelect.Options[index].ScenarioName;

								this.Status.Scenario = new Scenario(scenarioName);
								this.Status.CurrPageIndex = 0;
								this.Status.CurrSelect = null; // 選択肢_終了
								skipMode = false;

								goto restartCurrPage;
							}
						}
					}
				}

				if (skipMode)
					if (this.Status.CurrSelect == null)
						if (1 <= dispPageEndedCount)
							nextPageFlag = true;

				if (autoMode)
					if (this.Status.CurrSelect == null)
						if (AUTO_NEXT_PAGE_INTERVAL <= dispPageEndedCount)
							nextPageFlag = true;

				if (nextPageFlag) // 次ページ
				{
					this.Status.CurrPageIndex++;

					if (this.Status.Scenario.Pages.Count <= this.Status.CurrPageIndex)
						break;

					goto restartCurrPage;
				}

				// 入力：スキップモード
				if (skipMode)
				{
					if (DDKey.GetInput(DX.KEY_INPUT_LCONTROL) == -1)
						skipMode = false;
				}
				else
				{
					if (DDKey.GetInput(DX.KEY_INPUT_LCONTROL) == 1)
						skipMode = true;
				}

				// 入力：過去ログ
				if (
					DDKey.GetInput(DX.KEY_INPUT_LEFT) == 1 ||
					DDInput.DIR_4.GetInput() == 1 ||
					0 < DDMouse.Rot
					)
				{
					this.BackLog();
				}

				// 入力：システムボタン
				if (DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex != -1) // システムボタンを左クリック
				{
					switch (this.SelectedSystemButtonIndex)
					{
						case 0: this.SaveMenu(); break;
						case 1: this.LoadMenu(); break;
						case 2: skipMode = true; break;
						case 3: autoMode = true; break;
						case 4: this.BackLog(); break;
						case 5: this.SystemMenu(); break;

						default:
							throw null; // never
					}
					if (this.SystemMenu_ReturnToTitleMenu)
						break;
				}

				// 入力：選択肢_移動
				if (this.Status.CurrSelect != null)
				{
					int moving = 0;

					if (
						DDKey.IsPound(DX.KEY_INPUT_UP) ||
						DDInput.DIR_8.IsPound()
						)
						moving = -1;

					if (
						DDKey.IsPound(DX.KEY_INPUT_DOWN) ||
						DDInput.DIR_2.IsPound()
						)
						moving = 1;

					if (moving != 0)
					{
						int optIndex = this.Status.CurrSelect.GetMouseFocusedIndex();

						if (optIndex == -1)
						{
							optIndex = 0;
						}
						else
						{
							optIndex += this.Status.CurrSelect.Options.Count + moving;
							optIndex %= this.Status.CurrSelect.Options.Count;
						}

						DDMouse.X =
							GameConsts.SELECT_FRAME_L +
							Ground.I.Picture.MessageFrame_Button2.Get_W() -
							10;
						DDMouse.Y =
							GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * optIndex +
							Ground.I.Picture.MessageFrame_Button2.Get_H() -
							10;

						DDMouse.ApplyPos();
					}
				}

				if (
					this.CurrPage.Subtitle.Length < dispSubtitleCharCount &&
					this.CurrPage.Text.Length < dispCharCount
					)
					dispPageEndedCount++;

				if (skipMode)
				{
					dispSubtitleCharCount += 8;
					dispCharCount += 8;
				}
				else if (dispFastMode)
				{
					dispSubtitleCharCount += 2;
					dispCharCount += 2;
				}
				else
				{
					if (DDEngine.ProcFrame % 2 == 0)
						dispSubtitleCharCount++;

					if (DDEngine.ProcFrame % 3 == 0)
						dispCharCount++;
				}
				DDUtils.ToRange(ref dispSubtitleCharCount, 0, SCommon.IMAX);
				DDUtils.ToRange(ref dispCharCount, 0, SCommon.IMAX);

				// ====
				// 描画ここから
				// ====

				this.DrawSurfaces();

				// メッセージ枠
				{
					const int h = 136;

					DDDraw.SetAlpha(0.9);
					DDDraw.DrawRect(Ground.I.Picture.MessageFrame_Message, 0, DDConsts.Screen_H - h, DDConsts.Screen_W, h);
					DDDraw.Reset();
				}

				// システムボタン
				{
					var buttons = new[]
					{
						// p: 画像, fp: フォーカス時の画像
						new { p = Ground.I.Picture.MessageFrame_Save, fp = Ground.I.Picture.MessageFrame_Save2 },
						new { p = Ground.I.Picture.MessageFrame_Load, fp = Ground.I.Picture.MessageFrame_Load2 },
						new { p = Ground.I.Picture.MessageFrame_Skip, fp = Ground.I.Picture.MessageFrame_Skip2 },
						new { p = Ground.I.Picture.MessageFrame_Auto, fp = Ground.I.Picture.MessageFrame_Auto2 },
						new { p = Ground.I.Picture.MessageFrame_Log, fp = Ground.I.Picture.MessageFrame_Log2 },
						new { p = Ground.I.Picture.MessageFrame_Menu, fp = Ground.I.Picture.MessageFrame_Menu2 },
						//new { p = Ground.I.Picture.MessageFrame_Close, fp = Ground.I.Picture.MessageFrame_Close2 },
						//new { p = Ground.I.Picture.MessageFrame_Config, fp = Ground.I.Picture.MessageFrame_Config2 },
						//new { p = Ground.I.Picture.MessageFrame_QLoad, fp = Ground.I.Picture.MessageFrame_QLoad2 },
						//new { p = Ground.I.Picture.MessageFrame_QSave, fp = Ground.I.Picture.MessageFrame_QSave2 },
						//new { p = Ground.I.Picture.MessageFrame_Screen, fp = Ground.I.Picture.MessageFrame_Screen2 },
						//new { p = Ground.I.Picture.MessageFrame_Title, fp = Ground.I.Picture.MessageFrame_Title2 },
					};

					int selSysBtnIdx = -1;

					for (int index = 0; index < buttons.Length; index++)
					{
						DDDraw.DrawCenter(
							index == this.SelectedSystemButtonIndex ? buttons[index].fp : buttons[index].p,
							GameConsts.SYSTEM_BUTTON_X + index * GameConsts.SYSTEM_BUTTON_X_STEP,
							GameConsts.SYSTEM_BUTTON_Y
							);

						// フォーカスしているシステムボタンを再設定
						{
							if (!DDUtils.IsOut(
								new D2Point(
									DDMouse.X,
									DDMouse.Y
									),
								new D4Rect(
									GameConsts.SYSTEM_BUTTON_X - buttons[index].p.Get_W() / 2 + index * GameConsts.SYSTEM_BUTTON_X_STEP,
									GameConsts.SYSTEM_BUTTON_Y - buttons[index].p.Get_H() / 2,
									buttons[index].p.Get_W(),
									buttons[index].p.Get_H()
									)
								))
								selSysBtnIdx = index;
						}
					}
					this.SelectedSystemButtonIndex = selSysBtnIdx;
				}

				// サブタイトル文字列
				{
					int dispSubtitleLength = Math.Min(dispCharCount, this.CurrPage.Subtitle.Length);
					string dispSubtitle = this.CurrPage.Subtitle.Substring(0, dispSubtitleLength);

					DDFontUtils.DrawString(10, 413, dispSubtitle, DDFontUtils.GetFont("Kゴシック", 16));
				}

				// シナリオのテキスト文字列
				{
					int dispTextLength = Math.Min(dispCharCount, this.CurrPage.Text.Length);
					string dispText = this.CurrPage.Text.Substring(0, dispTextLength);
					string[] dispLines = dispText.Split('\n');

					for (int index = 0; index < dispLines.Length; index++)
					{
						DDFontUtils.DrawString(10, 450 + index * 30, dispLines[index], DDFontUtils.GetFont("Kゴシック", 16), false, new I3Color(110, 100, 90));
					}
				}

				if (this.Status.CurrSelect != null) // 選択肢
				{
					for (int index = 0; index < GameConsts.SELECT_FRAME_NUM; index++)
					{
						DDPicture picture = Ground.I.Picture.MessageFrame_Button;

						if (index < this.Status.CurrSelect.Options.Count)
						{
							picture = Ground.I.Picture.MessageFrame_Button2;

							if (this.Status.CurrSelect.Options[index].MouseFocused)
								picture = Ground.I.Picture.MessageFrame_Button3;
						}

						DDDraw.DrawSimple(
							picture,
							GameConsts.SELECT_FRAME_L,
							GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index
							);
					}
					for (int index = 0; index < this.Status.CurrSelect.Options.Count; index++)
					{
						const int title_x = 80;
						const int title_y = 28;

						DDFontUtils.DrawString(
							 GameConsts.SELECT_FRAME_L + title_x,
							 GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index + title_y,
							 this.Status.CurrSelect.Options[index].Title,
							 DDFontUtils.GetFont("Kゴシック", 16),
							 false,
							 new I3Color(110, 100, 90)
							 );

						// フォーカスしている選択項目を再設定
						{
							bool mouseOut = DDUtils.IsOut(
								new D2Point(
									DDMouse.X,
									DDMouse.Y
									),
								new D4Rect(
									GameConsts.SELECT_FRAME_L,
									GameConsts.SELECT_FRAME_T + GameConsts.SELECT_FRAME_T_STEP * index,
									Ground.I.Picture.MessageFrame_Button2.Get_W(),
									Ground.I.Picture.MessageFrame_Button2.Get_H()
									)
								);

							this.Status.CurrSelect.Options[index].MouseFocused = !mouseOut;
						}
					}
				}

				DDEngine.EachFrame();
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				this.DrawSurfaces();

				DDEngine.EachFrame();
			}

			DDEngine.FreezeInput();
		}

		/// <summary>
		/// <para>主たる画面描画</para>
		/// <para>色々な場所(モード)から呼び出されるだろう。</para>
		/// </summary>
		private void DrawSurfaces()
		{
			DDCurtain.DrawCurtain(); // 画面クリア

			Game.I.Status.Surfaces.Sort((a, b) => a.Z - b.Z); // Z-オーダー順

			foreach (Surface surface in Game.I.Status.Surfaces) // キャラクタ・オブジェクト・壁紙
				if (!surface.Act.Draw())
					surface.Draw();

			Game.I.SurfaceEL.ExecuteAllTask(); // キャラクタ・オブジェクト・壁紙 の エフェクト
		}

		/// <summary>
		/// 過去ログ
		/// </summary>
		private void BackLog()
		{
			List<string> logLines = new List<string>();

			for (int index = 0; index < this.Status.CurrPageIndex; index++)
				foreach (string line in this.Status.Scenario.Pages[index].Lines)
					logLines.Add(line);

			DDEngine.FreezeInput(BACKLOG_SHITA_KORO_SLEEP);

			int backIndex = 0;

			for (; ; )
			{
				if (
					DDKey.IsPound(DX.KEY_INPUT_UP) ||
					DDInput.DIR_8.IsPound() ||
					0 < DDMouse.Rot
					)
					backIndex++;

				if (
					DDKey.IsPound(DX.KEY_INPUT_DOWN) ||
					DDInput.DIR_2.IsPound() ||
					DDMouse.Rot < 0
					)
					backIndex--;

				if (
					DDKey.IsPound(DX.KEY_INPUT_RIGHT) ||
					DDInput.DIR_6.GetInput() == 1
					)
					backIndex = -1;

				DDUtils.ToRange(ref backIndex, -1, logLines.Count - 1);

				if (backIndex < 0)
					break;

				this.DrawSurfaces();
				DDCurtain.DrawCurtain(-0.5);

				for (int c = 1; c <= 16; c++)
				{
					int i = logLines.Count - backIndex - c;

					if (0 <= i)
					{
						DDFontUtils.DrawString(10, DDConsts.Screen_H - c * 30 - 15, logLines[i], DDFontUtils.GetFont("Kゴシック", 16));
					}
				}
				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput(BACKLOG_SHITA_KORO_SLEEP);
		}

		/// <summary>
		/// ゲーム中のセーブ画面
		/// </summary>
		private void SaveMenu()
		{
			this.SaveLoadMenu(true);
		}

		/// <summary>
		/// ゲーム中のロード画面
		/// </summary>
		private void LoadMenu()
		{
			this.SaveLoadMenu(false);
		}

		/// <summary>
		/// ゲーム中のセーブ・ロード画面
		/// </summary>
		/// <param name="saveMode">セーブモードであるか</param>
		private void SaveLoadMenu(bool saveMode)
		{
			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				WallColor = saveMode ? new I3Color(192, 60, 60) : new I3Color(60, 60, 192),
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					saveMode ? "セーブメニュー(仮)" : "ロードメニュー(仮)",
					Ground.I.SaveDataSlots.Select(v => v == null ? "[no-data]" : SCommon.Hex.ToString(SCommon.GetSHA512(Encoding.UTF8.GetBytes(v))).Substring(0, 20)).Concat(new string[] { "戻る" }).ToArray(),
					selectIndex
					);

				if (selectIndex < GameConsts.SAVE_DATA_SLOT_NUM)
				{
					if (saveMode) // ? セーブモード
					{
						Ground.I.SaveDataSlots[selectIndex] = this.Status.Serialize();
					}
					else // ? ロードモード
					{
						if (Ground.I.SaveDataSlots[selectIndex] != null) // ロードする。
						{
							this.Status = GameStatus.Deserialize(Ground.I.SaveDataSlots[selectIndex]);
							this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];
							break;
						}
					}
				}
				else // [戻る]
				{
					break;
				}
				//DDEngine.EachFrame(); // 不要
			}
			DDEngine.FreezeInput(MENU_RETURN_SLEEP);
		}

		private bool SystemMenu_ReturnToTitleMenu = false;

		/// <summary>
		/// システムメニュー画面
		/// </summary>
		private void SystemMenu()
		{
			this.SystemMenu_ReturnToTitleMenu = false; // reset

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				WallColor = new I3Color(100, 90, 100),
			};

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					"システムメニュー(仮)",
					new string[]
					{
						"タイトルに戻る",
						"ゲームに戻る",
					},
					selectIndex
					);

				switch (selectIndex)
				{
					case 0:
						this.SystemMenu_ReturnToTitleMenu = true;
						goto endLoop;

					case 1:
						goto endLoop;

					default:
						throw null; // never
				}
				//DDEngine.EachFrame(); // 不要
			}
		endLoop:
			DDEngine.FreezeInput(MENU_RETURN_SLEEP);
		}
	}
}
