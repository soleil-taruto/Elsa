﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_セーブ地点 : Enemy
	{
		public Enemy_セーブ地点(double x, double y)
			: base(x, y, 0, 0, false)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			for (; ; )
			{
				if (DDUtils.GetDistance(new D2Point(Game.I.Player.X, Game.I.Player.Y), new D2Point(this.X, this.Y)) < 30.0)
				{
					// セーブ
					// -- GameCommon あたりに移動した方が良いか...
					{
						GameStatus gameStatus = Game.I.Status.GetClone();

						// ★★★★★
						// プレイヤー・ステータス反映(セーブ時)
						// その他の反映箇所：
						// -- マップ入場時
						// -- マップ退場時
						{
							gameStatus.StartHP = Game.I.Player.HP;
						}

						Ground.I.GameSaveData = new Ground.GameSaveDataInfo()
						{
							MapName = GameCommon.GetMapName(Game.I.Map.MapFile, "t0001"),
							GameStatus = gameStatus,
						};
					}

					break;
				}

				DDDraw.DrawBegin(DDGround.GeneralResource.Dummy, this.X - DDGround.ICamera.X, this.Y - DDGround.ICamera.Y);
				DDDraw.DrawRotate(DDEngine.ProcFrame / 30.0);
				DDDraw.DrawEnd();

				DDPrint.SetPrint((int)this.X - DDGround.ICamera.X, (int)this.Y - DDGround.ICamera.Y);
				DDPrint.SetBorder(new I3Color(0, 0, 0));
				DDPrint.PrintLine("セーブ地点");
				DDPrint.Reset();

				// 当たり判定無し

				yield return true;
			}
		}
	}
}

