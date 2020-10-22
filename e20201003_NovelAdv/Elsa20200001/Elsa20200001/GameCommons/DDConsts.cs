﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.GameCommons
{
	public static class DDConsts
	{
		public const string ConfigFile = "Config.conf";
		public const string SaveDataFile = "SaveData.dat";
		public const string ResourceFile_01 = "Resource.dat";
		public const string ResourceFile_02 = "res.dat";
		public const string ResourceDir_01 = @"C:\Dat\Elsa";
		public const string ResourceDir_02 = @"..\..\..\..\res";
		public const string UserDatStringsFile = "Properties.dat";

		public const int Screen_W = 960;
		public const int Screen_H = 540;

		public const int Screen_W_Min = 100;
		public const int Screen_H_Min = 100;
		public const int Screen_W_Max = 4000;
		public const int Screen_H_Max = 3000;

		public const double DefaultVolume = 0.45;
	}
}
