using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceMusic
	{
		//public DDMusic Dummy = new DDMusic("Dummy.mp3");

		public DDMusic MUS_TITLE = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\hmix\c22.mp3");
		public DDMusic MUS_GAMEOVER = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\hmix\c26.mp3");
		public DDMusic MUS_STAGE_01 = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\hmix\v8.mp3");
		public DDMusic MUS_BOSS_01 = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\maou-damashii\bgm_maoudamashii_orchestra12.mp3");
		public DDMusic MUS_STAGE_02 = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\hmix\n62.mp3");
		public DDMusic MUS_BOSS_02 = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\maou-damashii\bgm_maoudamashii_orchestra11_muon-cut.mp3");
		public DDMusic MUS_EXTRA_STAGE = new DDMusic(@"e20201010_TateShoot\Shoot_old_Resource\hmix\n4.mp3");
		public DDMusic MUS_EXTRA_BOSS = DDGround.GeneralResource.無音; // 未定
	}
}
