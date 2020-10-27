﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourcePicture
	{
		//public DDPicture Dummy = DDPictureLoaders.Standard("Dummy.png");

		public DDPicture Copyright = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Logo\Copyright.png");

		public DDPicture Player = DDPictureLoaders.BgTrans(@"e20201025_YokoActTM\pata2-magic\free_pochitto.png"); // プレイヤー画像 大元

		// ---- プレイヤー画像 ここから

		public DDPicture[][] PlayerStands = new DDPicture[5][];
		public DDPicture[][] PlayerTalks = new DDPicture[5][];
		public DDPicture PlayerShagami;
		public DDPicture[] PlayerWalk = new DDPicture[2];
		public DDPicture[] PlayerDash = new DDPicture[2];
		public DDPicture[] PlayerStop = new DDPicture[2];
		public DDPicture[] PlayerJump = new DDPicture[3];
		public DDPicture PlayerAttack;
		public DDPicture PlayerAttackShagami;
		public DDPicture[] PlayerAttackWalk = new DDPicture[2];
		public DDPicture[] PlayerAttackDash = new DDPicture[2];
		public DDPicture PlayerAttackJump;
		public DDPicture[] PlayerDamage = new DDPicture[8];

		// ---- プレイヤー画像 ここまで

		public ResourcePicture()
		{
			// ---- プレイヤー画像 ここから

			for (int x = 0; x < 5; x++)
			{
				PlayerStands[x] = new DDPicture[2];
				PlayerTalks[x] = new DDPicture[2];

				for (int y = 0; y < 2; y++)
					for (int k = 0; k < 2; k++)
						new[] { PlayerStands, PlayerTalks }[y][x][k] = DDDerivations.GetPicture(Player, x * 208 + k * 96, 16 + y * 144, 94, 112);
			}
			PlayerShagami = DDDerivations.GetPicture(Player, 0, 304, 94, 112);

			for (int x = 0; x < 3; x++)
				for (int k = 0; k < 2; k++)
					new[] { PlayerWalk, PlayerDash, PlayerStop }[x][k] = DDDerivations.GetPicture(Player, 112 + x * 208 + k * 96, 304, 94, 112);

			for (int x = 0; x < 3; x++)
				PlayerJump[x] = DDDerivations.GetPicture(Player, 736 + x * 112, 304, 94, 112);

			{
				List<DDPicture> buff = new List<DDPicture>();

				for (int x = 0; x < 2; x++)
					buff.Add(DDDerivations.GetPicture(Player, x * 112, 448, 94, 112));

				{
					int c = 0;

					PlayerAttack = buff[c++];
					PlayerAttackShagami = buff[c++];
				}
			}

			for (int x = 0; x < 2; x++)
				for (int k = 0; k < 2; k++)
					new[] { PlayerAttackWalk, PlayerAttackDash }[x][k] = DDDerivations.GetPicture(Player, 224 + x * 208 + k * 96, 448, 94, 112);

			PlayerAttackJump = DDDerivations.GetPicture(Player, 640, 448, 94, 112);

			for (int x = 0; x < 8; x++)
				PlayerDamage[x] = DDDerivations.GetPicture(Player, 0 + x * 112, 592, 94, 112);

			// ---- プレイヤー画像 ここまで
		}

		public DDPicture Tile_None = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Tile\Tile_None.png");
		public DDPicture Tile_B0001 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Tile\Tile_B0001.png");
		public DDPicture Tile_B0002 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Tile\Tile_B0002.png");
		public DDPicture Tile_B0003 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Tile\Tile_B0003.png");
		public DDPicture Tile_B0004 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Tile\Tile_B0004.png");

		public DDPicture Wall_B0001 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Wall\Wall_B0001.jpg");
		public DDPicture Wall_B0002 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Wall\Wall_B0002.jpg");
		public DDPicture Wall_B0003 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Wall\Wall_B0003.jpg");
		public DDPicture Wall_B0004 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Wall\Wall_B0004.jpg");
		public DDPicture Wall_B0005 = DDPictureLoaders.Standard(@"e20201025_YokoActTM\Wall\Wall_B0005.jpg");
	}
}
