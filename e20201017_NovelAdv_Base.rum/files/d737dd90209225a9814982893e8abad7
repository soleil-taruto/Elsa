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

		// <---- prm

		public static Game I;

		//public DDTaskList SurfaceEL; // 廃止

		public Game()
		{
			I = this;

			//this.SurfaceEL = new DDTaskList(); // 廃止
		}

		public void Dispose()
		{
			//this.SurfaceEL = null; // 廃止

			I = null;
		}

		/// <summary>
		/// 短めの入力抑止フレーム数
		/// 例：前のホイール操作が次のホイール入力受付に反応してしまわないように
		/// </summary>
		public const int WHEEL_INPUT_SLEEP = 5;

		/// <summary>
		/// 長めの入力抑止フレーム数
		/// 例：メニューから戻ってきたとき
		/// </summary>
		public const int LONG_INPUT_SLEEP = 30;

		/// <summary>
		/// 現在のページのテキストを表示しきってから次ページへ遷移させないための入力抑止フレーム数
		/// </summary>
		private const int NEXT_PAGE_INPUT_INTERVAL = 10;

		/// <summary>
		/// 自動モードで次ページへ遷移するまでのフレーム数
		/// </summary>
		private const int AUTO_NEXT_PAGE_INTERVAL = 180;

		private ScenarioPage CurrPage;
		public int SelectedSystemButtonIndex = -1; // -1 == システムボタン未選択
		public bool SkipMode;
		public bool AutoMode;

		public void Perform(bool continueByTitleMenuFlag = false)
		{
			this.SkipMode = false;
			this.AutoMode = false;

			Surface_MessageWindow.Hide = false; // 2bs
			Surface_SystemButtons.Hide = false; // 2bs

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain();

		restartCurrPage:
			this.CurrPage = this.Status.Scenario.Pages[this.Status.CurrPageIndex];

			if (!continueByTitleMenuFlag)
				foreach (ScenarioCommand command in this.CurrPage.Commands)
					command.Invoke();

			continueByTitleMenuFlag = false;

			int dispSubtitleCharCount = 0;
			int dispCharCount = 0;
			int dispPageEndedCount = 0;
			bool dispFastMode = false;

			DDEngine.FreezeInput();

			for (; ; )
			{
				bool nextPageFlag = false;

				// ★★★ キー押下は 1 マウス押下は -1 で判定する。

				// 入力：シナリオを進める。(マウスホイール)
				if (DDMouse.Rot < 0)
				{
					this.CancelSkipAutoMode();

					if (dispPageEndedCount < NEXT_PAGE_INPUT_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						dispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (!this.Status.HasSelect())
							nextPageFlag = true;
					}
					DDEngine.FreezeInput(WHEEL_INPUT_SLEEP);
				}

				// 入力：シナリオを進める。(マウスホイール_以外)
				if (
					DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex == -1 || // システムボタン以外を左クリック
					DDInput.A.GetInput() == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1
					)
				{
					if (dispPageEndedCount < NEXT_PAGE_INPUT_INTERVAL) // ? ページ表示_未完了 -> ページ表示_高速化
					{
						dispFastMode = true;
					}
					else // ? ページ表示_完了 -> 次ページ
					{
						if (!this.Status.HasSelect())
						{
							nextPageFlag = true;
						}
						else // 選択肢表示中
						{
							int index = this.Status.GetSelect().GetMouseFocusedIndex();

							if (index != -1) // 選択中の選択肢へ進む
							{
								string scenarioName = this.Status.GetSelect().Options[index].ScenarioName;

								this.Status.Scenario = new Scenario(scenarioName);
								this.Status.CurrPageIndex = 0;
								this.Status.RemoveSelect(); // 選択肢_終了
								//this.SkipMode = false; // moved

								goto restartCurrPage;
							}
						}
					}
				}

				if (this.SkipMode)
					if (!this.Status.HasSelect())
						if (1 <= dispPageEndedCount)
							nextPageFlag = true;

				if (this.AutoMode)
					if (!this.Status.HasSelect())
						if (AUTO_NEXT_PAGE_INTERVAL <= dispPageEndedCount)
							nextPageFlag = true;

				if (nextPageFlag) // 次ページ
				{
					if (this.SkipMode)
						foreach (Surface surface in this.Status.Surfaces)
							surface.Act.Clear();

					this.Status.CurrPageIndex++;

					if (this.Status.Scenario.Pages.Count <= this.Status.CurrPageIndex)
						break;

					goto restartCurrPage;
				}

				// 入力：スキップモード -> サーフェス化

				// 入力：過去ログ
				if (
					DDKey.GetInput(DX.KEY_INPUT_LEFT) == 1 ||
					DDInput.DIR_4.GetInput() == 1 ||
					0 < DDMouse.Rot
					)
				{
					this.BackLog();
				}

				// 入力：鑑賞モード
				if (
					DDMouse.R.GetInput() == -1 ||
					DDKey.GetInput(DX.KEY_INPUT_BACK) == 1 ||
					DDInput.B.GetInput() == 1
					)
				{
					this.Appreciate();
				}


				// 入力：システムボタン
				if (DDMouse.L.GetInput() == -1 && this.SelectedSystemButtonIndex != -1) // システムボタンを左クリック
				{
					switch (this.SelectedSystemButtonIndex)
					{
						case 0: this.SaveMenu(); break;
						case 1: this.LoadMenu(); break;
						case 2: this.SkipMode = !this.SkipMode; break;
						case 3: this.AutoMode = !this.AutoMode; break;
						case 4: this.BackLog(); break;
						case 5: this.SystemMenu(); break;

						default:
							throw null; // never
					}
					if (this.SystemMenu_ReturnToTitleMenu)
						break;
				}

				// 入力：選択肢_移動 -> サーフェス化

				if (
					this.CurrPage.Subtitle.Length < dispSubtitleCharCount &&
					this.CurrPage.Text.Length < dispCharCount
					)
					dispPageEndedCount++;

				if (this.SkipMode)
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

				// メッセージ枠 -> サーフェス化

				// システムボタン -> サーフェス化

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

				// 選択肢 -> サーフェス化

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
		/// スキップモード・オートモードを解除する。
		/// 両モード中、何か入力があれば解除されるのが自然だと思う。
		/// どこで解除しているか分かるようにメソッド化した。
		/// </summary>
		public void CancelSkipAutoMode()
		{
			this.SkipMode = false;
			this.AutoMode = false;
		}

		/// <summary>
		/// <para>主たる画面描画</para>
		/// <para>色々な場所(モード)から呼び出されるだろう。</para>
		/// </summary>
		public void DrawSurfaces()
		{
			DDCurtain.DrawCurtain(); // 画面クリア

			Game.I.Status.Surfaces.Sort((a, b) => a.Z - b.Z); // Z-オーダー順

			foreach (Surface surface in Game.I.Status.Surfaces) // キャラクタ・オブジェクト・壁紙
				if (!surface.Act.Draw())
					surface.Draw();

			//Game.I.SurfaceEL.ExecuteAllTask(); // 廃止, 理由：Z-オーダーを無視している。
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

			DDEngine.FreezeInput(WHEEL_INPUT_SLEEP);

			int backIndex = 0;

			for (; ; )
			{
				if (
					DDMouse.L.GetInput() == -1 ||
					DDInput.A.GetInput() == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1
					)
					break;

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
			DDEngine.FreezeInput(WHEEL_INPUT_SLEEP);
		}

		/// <summary>
		/// 鑑賞モード
		/// </summary>
		private void Appreciate()
		{
			Surface_MessageWindow.Hide = true;
			Surface_SystemButtons.Hide = true;

			DDEngine.FreezeInput(WHEEL_INPUT_SLEEP);

			for (; ; )
			{
				if (
					DDMouse.L.GetInput() == -1 ||
					DDMouse.R.GetInput() == -1 ||
					DDInput.A.GetInput() == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_SPACE) == 1 ||
					DDKey.GetInput(DX.KEY_INPUT_RETURN) == 1
					)
					break;

				this.DrawSurfaces();
				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput(WHEEL_INPUT_SLEEP);

			Surface_MessageWindow.Hide = false; // restore
			Surface_SystemButtons.Hide = false; // restore
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
				Color = new I3Color(255, 255, 255),
				BorderColor = saveMode ? new I3Color(192, 0, 0) : new I3Color(0, 0, 192),
				WallPicture = Ground.I.Picture.星屑物語02,
				WallCurtain = -0.5,
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
			DDEngine.FreezeInput(LONG_INPUT_SLEEP);
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
				Color = new I3Color(255, 255, 255),
				BorderColor = new I3Color(0, 64, 0),
				WallPicture = Ground.I.Picture.星屑物語04,
				WallCurtain = -0.5,
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
			DDEngine.FreezeInput(LONG_INPUT_SLEEP);
		}
	}
}
