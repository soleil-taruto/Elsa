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

		public DDPicture Copyright = DDPictureLoaders.Standard(@"e20200003_dat\Logo\Copyright.png");

		//public DDPicture Title = DDPictureLoaders.Standard(@"e20200003_dat\Title.png");

		private const int CHARA_TIP_EXPNUM = 2;

		// -- プレイヤー・キャラチップ --

		public DDPicture Chara_A01_Jump = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_jump01.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Jump_Attack = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_jump02attack.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Jump_Damage = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_jump03damage.png", CHARA_TIP_EXPNUM);
		public DDPicture[] Chara_A01_Run = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run01.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run02.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run03.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture[] Chara_A01_Run_Attack = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run04_attack.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run05_attack.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_run06_attack.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture Chara_A01_Sliding = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_sliding01.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Telepo_01 = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_telepo01.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Telepo_02 = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_telepo02.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Telepo_03 = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_telepo03.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Wait = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_wait01.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Wait_Mabataki = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_wait02.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Wait_Start = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_wait03start.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Wait_Attack = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_wait04attack.png", CHARA_TIP_EXPNUM);
		public DDPicture[] Chara_A01_Climb = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_climb01.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_climb02.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture Chara_A01_Climb_Attack = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_climb03_attack.png", CHARA_TIP_EXPNUM);
		public DDPicture Chara_A01_Climb_Top = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\chara_a01_climb04.png", CHARA_TIP_EXPNUM);

		// -- エフェクト・キャラチップ --

		public DDPicture[] Effect_A01_A_Explosion = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_a01.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_a02.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_a03.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_a04.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_a05.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture[] Effect_A01_B_Explosion = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b01.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b02.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b03.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b04.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b05.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b06.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b07.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b08.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b09.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b10.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b11.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b12.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b13.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b14.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b15.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_explosion_b16.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture Effect_A01_Shock_A = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_shock01.png", CHARA_TIP_EXPNUM);
		public DDPicture[] Effect_A01_Shock_B = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_shock02.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_shock03.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_shock04.png", CHARA_TIP_EXPNUM),
		};
		public DDPicture[] Effect_A01_Sliding = new DDPicture[]
		{
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_sliding01.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_sliding02.png", CHARA_TIP_EXPNUM),
			DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\effect_a01_sliding03.png", CHARA_TIP_EXPNUM),
		};

		// -- ショット --

		public DDPicture Shot_Normal = DDPictureLoaders.Expand(@"e20200003_dat\chara_a01\game_shot_a01.png", CHARA_TIP_EXPNUM);

		// --

		public DDPicture Tile_None = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Tile_None.png", CHARA_TIP_EXPNUM);
		public DDPicture Tile_B0001 = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Tile_B0001.png", CHARA_TIP_EXPNUM);
		public DDPicture Tile_B0002 = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Tile_B0002.png", CHARA_TIP_EXPNUM);
		public DDPicture Tile_B0003 = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Tile_B0003.png", CHARA_TIP_EXPNUM);
		public DDPicture Tile_B0004 = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Tile_B0004.png", CHARA_TIP_EXPNUM);

		public DDPicture Space_B0001 = DDPictureLoaders.Expand(@"e20200003_dat\Tile\Space_B0001.png", CHARA_TIP_EXPNUM);

		//public DDPicture Wall_B0001 = DDPictureLoaders.Standard(@"e20200003_dat\Wall_B0001.png");
		//public DDPicture Wall_B0002 = DDPictureLoaders.Standard(@"e20200003_dat\Wall_B0002.png");
		//public DDPicture Wall_B0003 = DDPictureLoaders.Standard(@"e20200003_dat\Wall_B0003.png");

		public DDPicture Enemy_B0001_01 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0001_01.png");
		public DDPicture Enemy_B0001_02 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0001_02.png");
		public DDPicture Enemy_B0001_03 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0001_03.png");
		public DDPicture Enemy_B0001_04 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0001_04.png");

		public DDPicture Enemy_B0002_01 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0002_01.png");
		public DDPicture Enemy_B0002_02 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0002_02.png");

		public DDPicture Enemy_B0003 = DDPictureLoaders.Standard(@"e20200003_dat\Enemy_B0003.png");
	}
}
