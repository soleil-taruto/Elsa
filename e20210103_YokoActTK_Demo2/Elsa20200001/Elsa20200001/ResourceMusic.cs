﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceMusic
	{
		//public DDMusic Dummy = new DDMusic("Dummy.mp3");

		public DDMusic Title = new DDMusic(@"e20200003_dat\ユーフルカ\Voyage_loop\Voyage_loop.ogg").SetLoop(655565, 4860197);

		public DDMusic Field_01 = new DDMusic(@"e20200003_dat\ユーフルカ\The-sacred-place_loop\The-sacred-place_loop.ogg").SetLoop(800621, 4233349);

		public DDMusic 神さびた古戦場 = new DDMusic(@"e20200003_dat\みるふぃ\nc200903.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1;
		}
	}
}
