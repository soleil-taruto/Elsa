﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceSE
	{
		//public DDSE Dummy = new DDSE("Dummy.mp3");

		public DDSE SE_PLAYERSHOT = new DDSE(@"e20201014_TateShoot\Shoot_old_Resource\beetlepancake\shot004.wav");
		public DDSE SE_KASURI = new DDSE(@"e20201014_TateShoot\Shoot_old_Resource\beetlepancake\kasuri001.wav");

		public ResourceSE()
		{
			this.SE_PLAYERSHOT.Volume = 0.1;
		}
	}
}
