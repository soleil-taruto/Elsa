using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Games.Enemies
{
	public class Enemy_MeteorLoader : Enemy
	{
		private DDRandom Random = new DDRandom(1u);

		public Enemy_MeteorLoader(D2Point pos)
			: base(pos)
		{ }

		public int 発生領域 = 5; // { 2, 4, 5, 6, 8 } == 下, 左, 全域, 右, 上

		public override void Draw()
		{
			if (this.Random.GetInt(10) == 0)
			{
				for (int retry = 0; retry < 10; retry++)
				{
					D2Point dotPos;

					switch (this.発生領域)
					{
						case 2:
							dotPos = new D2Point(
								DDGround.ICamera.X + this.Random.GetInt(DDConsts.Screen_W),
								DDGround.ICamera.Y + this.Random.GetInt(DDConsts.Screen_H / 2) + DDConsts.Screen_H / 2
								);
							break;

						case 4:
							dotPos = new D2Point(
								DDGround.ICamera.X + this.Random.GetInt(DDConsts.Screen_W / 2),
								DDGround.ICamera.Y + this.Random.GetInt(DDConsts.Screen_H)
								);
							break;

						case 5:
							dotPos = new D2Point(
								DDGround.ICamera.X + this.Random.GetInt(DDConsts.Screen_W),
								DDGround.ICamera.Y + this.Random.GetInt(DDConsts.Screen_H)
								);
							break;

						case 6:
							dotPos = new D2Point(
								DDGround.ICamera.X + this.Random.GetInt(DDConsts.Screen_W / 2) + DDConsts.Screen_W / 2,
								DDGround.ICamera.Y + this.Random.GetInt(DDConsts.Screen_H)
								);
							break;

						case 8:
							dotPos = new D2Point(
								DDGround.ICamera.X + this.Random.GetInt(DDConsts.Screen_W),
								DDGround.ICamera.Y + this.Random.GetInt(DDConsts.Screen_H / 2)
								);
							break;

						default:
							throw null; // never
					}

					I2Point cellPos = GameCommon.ToTablePoint(dotPos);
					MapCell cell = Game.I.Map.GetCell(cellPos);

					if (
						!cell.IsDefault && // ? 画面外ではない。
						100.0 < DDUtils.GetDistance(new D2Point(dotPos.X, dotPos.Y), new D2Point(Game.I.Player.X, Game.I.Player.Y)) && // ? 近すぎない。
						(cell.Kind == MapCell.Kind_e.WALL || cell.Kind == MapCell.Kind_e.DEATH)
						)
					{
						cell.Kind = MapCell.Kind_e.DEATH;

						D2Point enemyPos = new D2Point(
							cellPos.X * GameConsts.TILE_W + GameConsts.TILE_W / 2,
							cellPos.Y * GameConsts.TILE_H + GameConsts.TILE_H / 2
							);

						Game.I.Enemies.Add(new Enemy_Death(enemyPos));
						Game.I.Enemies.Add(new Enemy_Meteor(enemyPos));

						break;
					}
				}
			}
		}

		public override Enemy GetClone()
		{
			return new Enemy_MeteorLoader(new D2Point(this.X, this.Y));
		}
	}
}
