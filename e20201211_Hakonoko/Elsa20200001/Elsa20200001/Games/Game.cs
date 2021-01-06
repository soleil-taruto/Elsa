﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;
using Charlotte.GameCommons;
using Charlotte.GameCommons.Options;
using Charlotte.Games.Designs;
using Charlotte.Games.Enemies;
using Charlotte.LevelEditors;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public Map Map;

		// <---- prm

		public bool ReturnToTitleMenu = false;

		// <---- ret

		public static Game I;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		public Player Player = new Player();

		private bool CamSlideMode; // ? モード中
		private int CamSlideCount;
		private int CamSlideX; // -1 ～ 1
		private int CamSlideY; // -1 ～ 1

		public int Frame;

		public int SnapshotCount = GameConsts.SNAPSHOT_COUNT_MAX;

		public Snapshot Snapshot = null; // null == リスポーン地点_未設定

		public void Perform()
		{
			this.ReloadEnemies();

			// プレイヤー・ステータス反映(マップ入場時)
			{
				// none
			}

			DDCurtain.SetCurtain(0, -1.0);
			DDCurtain.SetCurtain(10);

			DDEngine.FreezeInput();

			// TODO: 音楽

			Func<bool> f_reorderEnemies = SCommon.Supplier(this.E_ReorderEnemies());
			Func<bool> f_ゴミ回収 = SCommon.Supplier(this.E_ゴミ回収());

			bool goalFlag = false; // ? ゴールした。

			this.Respawn();

			for (this.Frame = 0; ; this.Frame++)
			{
				f_reorderEnemies();

				if (DDInput.PAUSE.GetInput() == 1)
				{
					this.Pause();

					if (this.Pause_ReturnToTitleMenu)
					{
						this.ReturnToTitleMenu = true;
						break;
					}
					if (this.Pause_Respawn)
					{
						this.Snapshot = null;
						this.Respawn();
					}
				}

				if (this.Player.DeadFrame == 0)
					this.カメラ位置調整(false);

				if (DDConfig.LOG_ENABLED && DDKey.GetInput(DX.KEY_INPUT_E) == 1) // エディットモード(デバッグ用)
				{
					this.Edit();
					this.ReloadEnemies();
					this.Frame = 0;
				}
				if (DDConfig.LOG_ENABLED && DDKey.GetInput(DX.KEY_INPUT_Q) == 1) // 強制クリア(デバッグ用)
				{
					goalFlag = true;
					break;
				}

				if (DDInput.R.GetInput() == 1) // リスポーン
				{
					this.Respawn();
				}
				if (DDInput.C.GetInput() == 1) // リスポーン地点_設置
				{
					if (1 <= this.SnapshotCount)
					{
						this.SnapshotCount--;

						this.Snapshot = new Snapshot();
						this.Snapshot.PlayerPosition = new D2Point(this.Player.X, this.Player.Y);
						this.Snapshot.Enemies = this.Enemies.Iterate().Select(enemy => enemy.GetClone()).ToList();

						波紋効果.Add(this.Player.X, this.Player.Y);
					}
				}

				int plMove = 0; // プレイヤー移動 { -1, 0, 1 } == { 左, 移動ナシ, 右 }

				// プレイヤー入力
				{
					bool deadOrReborn = 1 <= this.Player.DeadFrame || 1 <= this.Player.RebornFrame;
					bool slow = false;
					bool camSlide = false;
					int jump = 0;

					// 入力抑止中であるか否かに関わらず左右の入力は受け付ける様にする。
					int freezeInputFrameBackup = DDEngine.FreezeInputFrame;
					DDEngine.FreezeInputFrame = 0;

					if (!deadOrReborn && 1 <= DDInput.DIR_4.GetInput())
					{
						plMove = -1;
					}
					if (!deadOrReborn && 1 <= DDInput.DIR_6.GetInput())
					{
						plMove = 1;
					}

					DDEngine.FreezeInputFrame = freezeInputFrameBackup; // restore

					if (1 <= DDInput.L.GetInput())
					{
						plMove = 0;
						camSlide = true;
					}
					if (!deadOrReborn && 1 <= DDInput.B.GetInput())
					{
						slow = true;
					}
					if (!deadOrReborn && 1 <= DDInput.A.GetInput())
					{
						jump = DDInput.A.GetInput();
					}

					if (plMove != 0)
						this.Player.MoveFrame++;
					else
						this.Player.MoveFrame = 0;

					this.Player.MoveSlow = plMove != 0 && slow;

					if (1 <= this.Player.JumpFrame)
					{
						const int JUMP_FRAME_MAX = 22;

						if (1 <= jump && this.Player.JumpFrame < JUMP_FRAME_MAX)
							this.Player.JumpFrame++;
						else
							this.Player.JumpFrame = 0;
					}
					else
					{
						// 事前入力 == 着地前の数フレーム間にジャンプボタンを押し始めてもジャンプできるようにする。
						// 入力猶予 == 落下(地面から離れた)直後の数フレーム間にジャンプボタンを押し始めてもジャンプできるようにする。

						const int 事前入力時間 = 10;
						const int 入力猶予時間 = 5;

						if (1 <= jump && jump < 事前入力時間 && this.Player.AirborneFrame < 入力猶予時間)
							this.Player.JumpFrame = 1;
					}

					if (camSlide)
					{
						if (DDInput.DIR_4.IsPound())
						{
							this.CamSlideCount++;
							this.CamSlideX--;
						}
						if (DDInput.DIR_6.IsPound())
						{
							this.CamSlideCount++;
							this.CamSlideX++;
						}
						if (DDInput.DIR_8.IsPound())
						{
							this.CamSlideCount++;
							this.CamSlideY--;
						}
						if (DDInput.DIR_2.IsPound())
						{
							this.CamSlideCount++;
							this.CamSlideY++;
						}
						DDUtils.ToRange(ref this.CamSlideX, -1, 1);
						DDUtils.ToRange(ref this.CamSlideY, -1, 1);
					}
					else
					{
						if (this.CamSlideMode && this.CamSlideCount == 0)
						{
							this.CamSlideX = 0;
							this.CamSlideY = 0;
						}
						this.CamSlideCount = 0;
					}
					this.CamSlideMode = camSlide;
				}

				//startDead:
				if (1 <= this.Player.DeadFrame) // プレイヤー死亡中の処理
				{
					int frame = this.Player.DeadFrame - 1;

					if (GameConsts.PLAYER_DEAD_FRAME_MAX < frame)
					{
						this.Respawn();
						goto endDead;
					}
					this.Player.DeadFrame++;

					// この時点でとりうる this.Player.DeadFrame の最大値は Consts.PLAYER_DEAD_FRAME_MAX + 2

					// ----

					if (frame == 0) // init
					{
						if (this.Dead()) // ? リスポーン_ボタンが押された。
						{
							this.Respawn();
							goto endDead;
						}
					}
				}
			endDead:

				//startReborn:
				if (1 <= this.Player.RebornFrame) // プレイヤー再登場中の処理
				{
					int frame = this.Player.RebornFrame - 1;

					if (GameConsts.PLAYER_REBORN_FRAME_MAX < frame)
					{
						this.Player.RebornFrame = 0;
						goto endReborn;
					}
					this.Player.RebornFrame++;

					// この時点でとりうる this.Player.RebornFrame の最大値は Consts.PLAYER_REBORN_FRAME_MAX + 2

					// ----

					// TODO
				}
			endReborn:

				// プレイヤー移動
				if (
					this.Player.DeadFrame == 0 &&
					this.Player.RebornFrame == 0
					)
				{
					if (1 <= this.Player.MoveFrame)
					{
						double speed = 0.0;

						if (this.Player.MoveSlow)
						{
							speed = this.Player.MoveFrame * 0.2;
							DDUtils.Minim(ref speed, 3.0);
						}
						else
							speed = 7.0;

						speed *= plMove;

						DDUtils.Approach(ref this.Player.XSpeed, speed, 0.333);
					}
					else
						this.Player.XSpeed /= 2.0;

					this.Player.X += this.Player.XSpeed;

					const double 重力加速度 = 0.5;
					//const double 重力加速度 = 0.45; // del @ 2021.1.4
					const double 落下最高速度 = 7.0;
					const double ジャンプによる上昇_YSpeed = -7.0;

					if (1 <= this.Player.JumpFrame)
						this.Player.YSpeed = ジャンプによる上昇_YSpeed;
					else
						this.Player.YSpeed += 重力加速度;

					DDUtils.Minim(ref this.Player.YSpeed, 落下最高速度);

					this.Player.Y += this.Player.YSpeed; // 自由落下
				}

				// プレイヤー位置矯正
				if (
					this.Player.DeadFrame == 0 &&
					this.Player.RebornFrame == 0
					)
				{
					自機位置調整.Perform(ref this.Player.X, ref this.Player.Y, v => v.IsWall());

					if (自機位置調整.Touch == 自機位置調整.Touch_e.GROUND)
					{
						this.Player.AirborneFrame = 0;
						this.Player.YSpeed = Math.Min(0.0, this.Player.YSpeed);
					}
					else
					{
						this.Player.AirborneFrame++;

						if (自機位置調整.Touch == 自機位置調整.Touch_e.ROOF)
						{
							this.Player.JumpFrame = 0;
							//this.Player.YSpeed = Math.Max(0.0, this.Player.YSpeed);
							this.Player.YSpeed = Math.Abs(this.Player.YSpeed);
						}
					}
				}
				//endPlayer:

				// マップ外に出た。-> 適当に内側に押し戻しておく。
				{
					const double SPAN = 100.0;

					if (this.Player.X < 0.0) // ? マップの左側に出た。
					{
						this.Player.X += SPAN;
					}
					if (this.Map.W * GameConsts.TILE_W < this.Player.X) // ? マップの右側に出た。
					{
						this.Player.X -= SPAN;
					}
					if (this.Player.Y < 0.0) // ? マップの上側に出た。
					{
						this.Player.Y += SPAN;
					}
					if (this.Map.H * GameConsts.TILE_H < this.Player.Y) // ? マップの下側に出た。
					{
						this.Player.Y -= SPAN;
					}
				}

				// 画面遷移時の微妙なカメラ位置ズレ解消
				// -- スタート地点(入場地点)が地面と接していると、最初のフレームでプレイヤーは上に押し出されて(ゲームによっては)カメラの初期位置とズレてしまう。
				// ---- なのでこの場所で処理する。
				if (this.Frame == 0)
				{
					this.カメラ位置調整(true);
				}

				const double PL_CRASH_W = GameConsts.TILE_W - 1;
				const double PL_CRASH_H = GameConsts.TILE_H - 1;

				//DDCrash plCrash = DDCrashUtils.Point(new D2Point(this.Player.X, this.Player.Y)); // 点
				//DDCrash plCrash = DDCrashUtils.Rect_CenterSize(new D2Point(this.Player.X, this.Player.Y), new D2Size(Consts.TILE_W, Consts.TILE_H));
				DDCrash plCrash = DDCrashUtils.Rect_CenterSize(new D2Point(this.Player.X, this.Player.Y), new D2Size(PL_CRASH_W, PL_CRASH_H));

				// ====
				// 描画ここから
				// ====

				this.Map.Design.DrawWall(
					DDGround.Camera.X,
					DDGround.Camera.Y,
					DDGround.Camera.X / (this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W),
					DDGround.Camera.Y / (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H)
					);
				this.DrawTiles();
				this.Player.Draw();

				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					enemy.Crash = DDCrashUtils.None(); // reset
					enemy.Draw();
				}

				// memo: DeadFlag をチェックするのは「当たり判定」から

				if (this.当たり判定表示)
				{
					// 最後に描画されるように DDGround.EL.Add() する。

					DDGround.EL.Add(() =>
					{
						DDCurtain.DrawCurtain(-0.7);

						const double A = 0.7;

						DDCrashView.Draw(new DDCrash[] { plCrash }, new I3Color(255, 0, 0), 1.0);
						DDCrashView.Draw(this.Enemies.Iterate().Select(v => v.Crash), new I3Color(255, 255, 255), A);

						return false;
					});
				}

				// ====
				// 描画ここまで
				// ====

				// ====
				// 当たり判定ここから
				// ====

				// ゴール
				{
					const double R = 15.9;

					if (
						this.Map.GetCell(GameCommon.ToTablePoint(new D2Point(this.Player.X - R, this.Player.Y - R))).IsGoal() ||
						this.Map.GetCell(GameCommon.ToTablePoint(new D2Point(this.Player.X + R, this.Player.Y - R))).IsGoal() ||
						this.Map.GetCell(GameCommon.ToTablePoint(new D2Point(this.Player.X - R, this.Player.Y + R))).IsGoal() ||
						this.Map.GetCell(GameCommon.ToTablePoint(new D2Point(this.Player.X + R, this.Player.Y + R))).IsGoal()
						)
					{
						goalFlag = true;
						DDEngine.EachFrame(); // ゴールした瞬間を描画する。
						break; // 当たり判定まで進んでしまうと死亡判定と競合しそうなので、ここで break してしまう。
					}
				}

				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					// 衝突判定：敵 x 自機
					if (
						this.Player.DeadFrame == 0 && // ? プレイヤー死亡中ではない。
						this.Player.RebornFrame == 0 && // ? プレイヤー登場中ではない。
						!enemy.DeadFlag && // ? 敵：生存
						DDCrashUtils.IsCrashed(enemy.Crash, plCrash) // ? 衝突
						)
					{
						// ★ 自機_被弾ここから

						this.Player.DeadFrame = 1;

						// ★ 自機_被弾ここまで
					}
				}

				// ====
				// 当たり判定ここまで
				// ====

				f_ゴミ回収();

				this.Enemies.RemoveAll(v => v.DeadFlag);

				DDEngine.EachFrame();

				// ★★★ ゲームループの終点 ★★★
			}
			DDEngine.FreezeInput();

			// プレイヤー・ステータス反映(マップ退場時)
			{
				// noop
			}

			DDMain.KeepMainScreen();

			if (goalFlag) // ? ゴールした。
			{
				波紋効果.Add(this.Player.X, this.Player.Y);

				foreach (DDScene scene in DDSceneUtils.Create(30))
				{
					DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);
					DDEngine.EachFrame();
				}
				DDCurtain.SetCurtain(50, -1.0);

				foreach (DDScene scene in DDSceneUtils.Create(60))
				{
					DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);
					DDEngine.EachFrame();
				}
			}
			else // ? タイトルへ戻るを選択した。など
			{
				DDCurtain.SetCurtain(30, -1.0);

				foreach (DDScene scene in DDSceneUtils.Create(40))
				{
					DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);
					DDEngine.EachFrame();
				}
			}

			波紋効果.Clear();

			// ★★★ end of Perform() ★★★
		}

		private IEnumerable<bool> E_ReorderEnemies()
		{
			for (; ; )
			{
				for (int index = 1; index < this.Enemies.Count; index++)
				{
					if (!(this.Enemies[index - 1] is Enemy_Death) && this.Enemies[index] is Enemy_Death)
						this.Enemies.Swap(index - 1, index);

					if (index % 10 == 0) // 間隔は適当
						yield return true;
				}
				yield return true; // ループ内で1度も実行されない場合を想定
			}
		}

		/// <summary>
		/// あまりにもマップから離れすぎている敵・自弾の死亡フラグを立てる。
		/// </summary>
		/// <returns></returns>
		private IEnumerable<bool> E_ゴミ回収()
		{
			for (; ; )
			{
				foreach (Enemy enemy in this.Enemies.Iterate())
				{
					if (this.IsProbablyEvacuated(enemy.X, enemy.Y))
						enemy.DeadFlag = true;

					yield return true;
				}
				yield return true; // ループ内で1度も実行されない場合を想定
			}
		}

		private bool Dead() // ret: ? リスポーン_ボタンが押された。
		{
			DDMain.KeepMainScreen();

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);

				DDDraw.SetAlpha(0.2);
				DDDraw.SetBright(1.0, 0.0, 0.0);
				DDDraw.DrawRect(DDGround.GeneralResource.WhiteBox, 0, 0, DDConsts.Screen_W, DDConsts.Screen_H);
				DDDraw.Reset();

				if (DDInput.R.GetInput() == 1) // ? リスポーン_ボタンが押された。
				{
					波紋効果.Add(this.Player.X, this.Player.Y); // リスポーン押下時も出す。

					return true;
				}
				DDEngine.EachFrame();
			}

			波紋効果.Add(this.Player.X, this.Player.Y);

			return false;
		}

		private void Respawn()
		{
			if (this.Snapshot == null)
				this.RespawnRestart();
			else
				this.RespawnFromSnapshot(this.Snapshot);
		}

		/// <summary>
		/// スタート地点からのリスポーン
		/// </summary>
		private void RespawnRestart()
		{
			// デフォルトの「プレイヤーのスタート地点」
			// -- マップの中央
			this.Player.X = this.Map.W * GameConsts.TILE_W / 2.0;
			this.Player.Y = this.Map.H * GameConsts.TILE_H / 2.0;

			{
				int x;
				int y;

				if (this.Map.FindCell(out x, out y, v => v.Kind == MapCell.Kind_e.START))
				{
					this.Player.X = x * GameConsts.TILE_W + GameConsts.TILE_W / 2;
					this.Player.Y = y * GameConsts.TILE_H + GameConsts.TILE_H / 2;
				}
			}

			this.Player.XSpeed = 0.0;
			this.Player.YSpeed = 0.0;

			this.Player.DeadFrame = 0; // 死亡時の処理から呼び出された場合用
			this.Player.RebornFrame = 1;
			this.Player.AirborneFrame = SCommon.IMAX / 2; // リスポーン＆ジャンプ_抑止のため

			this.CamSlideX = 0;
			this.CamSlideY = 0;

			this.ReloadEnemies();

			波紋効果.Add(this.Player.X, this.Player.Y);
		}

		/// <summary>
		/// リスポーン地点からのリスポーン
		/// </summary>
		/// <param name="ss"></param>
		private void RespawnFromSnapshot(Snapshot ss)
		{
			this.Player.X = ss.PlayerPosition.X;
			this.Player.Y = ss.PlayerPosition.Y;

			this.Player.XSpeed = 0.0;
			this.Player.YSpeed = 0.0;

			this.Player.DeadFrame = 0; // 死亡時の処理から呼び出された場合用
			this.Player.RebornFrame = 0;
			this.Player.AirborneFrame = SCommon.IMAX / 2; // リスポーン＆ジャンプ_抑止のため

			this.CamSlideX = 0;
			this.CamSlideY = 0;

			this.Enemies = new DDList<Enemy>(ss.Enemies.Select(enemy => enemy.GetClone()).ToList());

			波紋効果.Add(this.Player.X, this.Player.Y);
		}

		private void カメラ位置調整(bool 一瞬で)
		{
			double targCamX = this.Player.X - DDConsts.Screen_W / 2 + (this.CamSlideX * DDConsts.Screen_W / 3);
			double targCamY = this.Player.Y - DDConsts.Screen_H / 2 + (this.CamSlideY * DDConsts.Screen_H / 3);

			DDUtils.ToRange(ref targCamX, 0.0, this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W);
			DDUtils.ToRange(ref targCamY, 0.0, this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H);

			if (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H < GameConsts.TILE_H) // ? カメラの縦の可動域が1タイルより狭い場合
				targCamY = (this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H) / 2; // 中心に合わせる。

			DDUtils.Approach(ref DDGround.Camera.X, targCamX, 一瞬で ? 0.0 : 0.8);
			DDUtils.Approach(ref DDGround.Camera.Y, targCamY, 一瞬で ? 0.0 : 0.8);

			//DDUtils.ToRange(ref DDGround.Camera.X, 0.0, this.Map.W * Consts.TILE_W - DDConsts.Screen_W);
			//DDUtils.ToRange(ref DDGround.Camera.Y, 0.0, this.Map.H * Consts.TILE_H - DDConsts.Screen_H);

			DDGround.ICamera.X = SCommon.ToInt(DDGround.Camera.X);
			DDGround.ICamera.Y = SCommon.ToInt(DDGround.Camera.Y);
		}

		#region Edit

		private void Edit()
		{
			this.Map.Load(); // ゲーム中にマップを書き換える場合があるので、再ロードする。

			DDEngine.FreezeInput();
			DDUtils.SetMouseDispMode(true);
			LevelEditor.ShowDialog();

			int lastMouseX = DDMouse.X;
			int lastMouseY = DDMouse.Y;

			for (; ; )
			{
				if (LevelEditor.Dlg.XPressed)
					break;

				// 廃止
				//if (DDKey.GetInput(DX.KEY_INPUT_E) == 1)
				//    break;

				I2Point cellPos = GameCommon.ToTablePoint(
					DDGround.Camera.X + DDMouse.X,
					DDGround.Camera.Y + DDMouse.Y
					);

				MapCell cell = Game.I.Map.GetCell(cellPos);

				if (cell.IsDefault)
				{
					// noop
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LSHIFT) && 1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL)) // 左シフト・コントロール押下 -> 塗り潰し / none
				{
					if (DDMouse.L.GetInput() == -1) // クリックを検出
					{
						MapCell.Kind_e kind = LevelEditor.Dlg.GetKind();

						if (kind != cell.Kind)
						{
							MapCell.Kind_e targetKind = cell.Kind; // cell.Kind は this.EditFill で変更される。

							this.EditFill(
								cellPos,
								v => v.Kind == targetKind,
								v => v.Kind = kind
								);
						}
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LSHIFT)) // 左シフト押下 -> 移動 / none
				{
					if (1 <= DDMouse.L.GetInput())
					{
						DDGround.Camera.X -= DDMouse.X - lastMouseX;
						DDGround.Camera.Y -= DDMouse.Y - lastMouseY;

						DDUtils.ToRange(ref DDGround.Camera.X, 0.0, this.Map.W * GameConsts.TILE_W - DDConsts.Screen_W);
						DDUtils.ToRange(ref DDGround.Camera.Y, 0.0, this.Map.H * GameConsts.TILE_H - DDConsts.Screen_H);

						DDGround.ICamera.X = SCommon.ToInt(DDGround.Camera.X);
						DDGround.ICamera.Y = SCommon.ToInt(DDGround.Camera.Y);
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else if (1 <= DDKey.GetInput(DX.KEY_INPUT_LCONTROL)) // 左コントロール押下 -> スポイト / none
				{
					if (1 <= DDMouse.L.GetInput())
					{
						LevelEditor.Dlg.SetKind(cell.Kind);
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						// none
					}
				}
				else // 編集モード
				{
					if (1 <= DDMouse.L.GetInput())
					{
						cell.Kind = LevelEditor.Dlg.GetKind();
					}
					else if (1 <= DDMouse.R.GetInput())
					{
						cell.Kind = MapCell.Kind_e.EMPTY;
					}
				}

				DDCurtain.DrawCurtain();

				LevelEditor.DrawTiles();

				lastMouseX = DDMouse.X;
				lastMouseY = DDMouse.Y;

				DDEngine.EachFrame();
			}
			DDEngine.FreezeInput();
			DDUtils.SetMouseDispMode(false);
			LevelEditor.CloseDialog();

			this.Map.Save(); // ★★★ マップをセーブする ★★★
		}

		private void EditFill(I2Point targetPos, Predicate<MapCell> isFillable, Action<MapCell> fill)
		{
			Queue<I2Point> q = new Queue<I2Point>();

			q.Enqueue(targetPos);

			while (1 <= q.Count)
			{
				I2Point cellPos = q.Dequeue();
				MapCell cell = this.Map.GetCell(cellPos);

				if (!cell.IsDefault && isFillable(cell))
				{
					fill(cell);

					q.Enqueue(new I2Point(cellPos.X - 1, cellPos.Y));
					q.Enqueue(new I2Point(cellPos.X + 1, cellPos.Y));
					q.Enqueue(new I2Point(cellPos.X, cellPos.Y - 1));
					q.Enqueue(new I2Point(cellPos.X, cellPos.Y + 1));
				}
			}
		}

		#endregion

		private void DrawTiles()
		{
			int w = this.Map.W;
			int h = this.Map.H;

			int cam_l = DDGround.ICamera.X;
			int cam_t = DDGround.ICamera.Y;
			int cam_r = cam_l + DDConsts.Screen_W;
			int cam_b = cam_t + DDConsts.Screen_H;

			I2Point lt = GameCommon.ToTablePoint(cam_l, cam_t);
			I2Point rb = GameCommon.ToTablePoint(cam_r, cam_b);

			// マージン付与
			// -- マップセルの範囲をはみ出て描画されるタイルのためにマージンを付加する。
			{
				const int MARGIN = 2;

				lt.X -= MARGIN;
				lt.Y -= MARGIN;
				rb.X += MARGIN;
				rb.Y += MARGIN;
			}

			lt.X = SCommon.ToRange(lt.X, 0, w - 1);
			lt.Y = SCommon.ToRange(lt.Y, 0, h - 1);
			rb.X = SCommon.ToRange(rb.X, 0, w - 1);
			rb.Y = SCommon.ToRange(rb.Y, 0, h - 1);

			for (int x = lt.X; x <= rb.X; x++)
			{
				for (int y = lt.Y; y <= rb.Y; y++)
				{
					MapCell cell = this.Map.Table[x, y];

					int tileX = x * GameConsts.TILE_W + GameConsts.TILE_W / 2;
					int tileY = y * GameConsts.TILE_H + GameConsts.TILE_H / 2;

					this.Map.Design.DrawTile(cell, x, y, tileX - cam_l, tileY - cam_t);
				}
			}
		}

		public DDList<Enemy> Enemies = new DDList<Enemy>();

		private void ReloadEnemies()
		{
			this.Enemies.Clear();

			for (int x = 0; x < this.Map.W; x++)
			{
				for (int y = 0; y < this.Map.H; y++)
				{
					MapCell.Kind_e kind = this.Map.Table[x, y].Kind;

					D2Point pos = new D2Point(
						x * GameConsts.TILE_W + GameConsts.TILE_W / 2,
						y * GameConsts.TILE_H + GameConsts.TILE_H / 2
						);

					switch (kind)
					{
						case MapCell.Kind_e.DEATH: this.Enemies.Add(new Enemy_Death(pos)); break;

						case MapCell.Kind_e.ARKANOID_1: this.Enemies.Add(new Enemy_Arkanoid(pos, 1)); break;
						case MapCell.Kind_e.ARKANOID_2: this.Enemies.Add(new Enemy_Arkanoid(pos, 2)); break;
						case MapCell.Kind_e.ARKANOID_3: this.Enemies.Add(new Enemy_Arkanoid(pos, 3)); break;
						case MapCell.Kind_e.ARKANOID_4: this.Enemies.Add(new Enemy_Arkanoid(pos, 4)); break;
						case MapCell.Kind_e.ARKANOID_6: this.Enemies.Add(new Enemy_Arkanoid(pos, 6)); break;
						case MapCell.Kind_e.ARKANOID_7: this.Enemies.Add(new Enemy_Arkanoid(pos, 7)); break;
						case MapCell.Kind_e.ARKANOID_8: this.Enemies.Add(new Enemy_Arkanoid(pos, 8)); break;
						case MapCell.Kind_e.ARKANOID_9: this.Enemies.Add(new Enemy_Arkanoid(pos, 9)); break;

						case MapCell.Kind_e.COOKIE_時計回り_1: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 3)); break;
						case MapCell.Kind_e.COOKIE_時計回り_2: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 2)); break;
						case MapCell.Kind_e.COOKIE_時計回り_3: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 1)); break;
						case MapCell.Kind_e.COOKIE_時計回り_4: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 4)); break;
						case MapCell.Kind_e.COOKIE_時計回り_6: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 0)); break;
						case MapCell.Kind_e.COOKIE_時計回り_7: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 5)); break;
						case MapCell.Kind_e.COOKIE_時計回り_8: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 6)); break;
						case MapCell.Kind_e.COOKIE_時計回り_9: this.Enemies.Add(new Enemy_Cookie(pos, Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 7)); break;

						case MapCell.Kind_e.COOKIE_反時計回り_1: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 3)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_2: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 2)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_3: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 1)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_4: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 4)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_6: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 0)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_7: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 5)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_8: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 6)); break;
						case MapCell.Kind_e.COOKIE_反時計回り_9: this.Enemies.Add(new Enemy_Cookie(pos, -Enemy_Cookie.IROT_SPEED, Enemy_Cookie.IROT_360 / 8 * 7)); break;

						case MapCell.Kind_e.PATA_L: this.Enemies.Add(new Enemy_Pata(pos, -Enemy_Pata.SPEED_NORMAL)); break;
						case MapCell.Kind_e.PATA_L_SLOW: this.Enemies.Add(new Enemy_Pata(pos, -Enemy_Pata.SPEED_SLOW)); break;
						case MapCell.Kind_e.PATA_L_FAST: this.Enemies.Add(new Enemy_Pata(pos, -Enemy_Pata.SPEED_FAST)); break;
						case MapCell.Kind_e.PATA_R: this.Enemies.Add(new Enemy_Pata(pos, Enemy_Pata.SPEED_NORMAL)); break;
						case MapCell.Kind_e.PATA_R_SLOW: this.Enemies.Add(new Enemy_Pata(pos, Enemy_Pata.SPEED_SLOW)); break;
						case MapCell.Kind_e.PATA_R_FAST: this.Enemies.Add(new Enemy_Pata(pos, Enemy_Pata.SPEED_FAST)); break;

						// TODO

						default:
							break;
					}
				}
			}
		}

		/// <summary>
		/// マップから離れすぎているか
		/// 退場と見なして良いか
		/// </summary>
		/// <param name="x">X_座標</param>
		/// <param name="y">Y_座標</param>
		/// <returns></returns>
		private bool IsProbablyEvacuated(double x, double y)
		{
			const int MGN_SCREEN_NUM = 3;

			return
				x < -DDConsts.Screen_W * MGN_SCREEN_NUM || this.Map.W * GameConsts.TILE_W + DDConsts.Screen_W * MGN_SCREEN_NUM < x ||
				y < -DDConsts.Screen_H * MGN_SCREEN_NUM || this.Map.H * GameConsts.TILE_H + DDConsts.Screen_H * MGN_SCREEN_NUM < y;
		}

		private bool Pause_ReturnToTitleMenu = false;
		private bool Pause_Respawn = false;

		private bool 当たり判定表示 = false;

		private void Pause()
		{
			DDMain.KeepMainScreen();

			DDSimpleMenu simpleMenu = new DDSimpleMenu()
			{
				Color = new I3Color(255, 255, 255),
				BorderColor = new I3Color(0, 64, 128),
				WallPicture = DDGround.KeptMainScreen.ToPicture(),
				WallCurtain = -0.5,
			};

			DDEngine.FreezeInput();

			// reset
			this.Pause_ReturnToTitleMenu = false;
			this.Pause_Respawn = false;

			int selectIndex = 0;

			for (; ; )
			{
				selectIndex = simpleMenu.Perform(
					"ポーズメニュー(仮)",
					new string[]
					{
						"----",
						"(デバッグ用)強制遅延 [ 現在の設定：" + DDEngine.SlowdownLevel + " ]",
						"(デバッグ用)当たり判定表示 [ 現在の設定：" + this.当たり判定表示 +" ]",
						"このステージの最初からやり直す",
						"タイトルに戻る",
						"ゲームに戻る",
					},
					selectIndex,
					true
					);

				switch (selectIndex)
				{
					case 0:
						break;

					case 1:
						if (DDEngine.SlowdownLevel == 0)
							DDEngine.SlowdownLevel = 1;
						else
							DDEngine.SlowdownLevel *= 2;

						DDEngine.SlowdownLevel %= 16;
						break;

					case 2:
						this.当たり判定表示 = !this.当たり判定表示;
						break;

					case 3:
						this.Pause_Respawn = true;
						goto endLoop;

					case 4:
						this.Pause_ReturnToTitleMenu = true;
						goto endLoop;

					case 5:
						goto endLoop;

					default:
						throw null; // never
				}
				//DDEngine.EachFrame(); // 不要
			}
		endLoop:
			DDEngine.FreezeInput();
		}
	}
}
