﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	public static class DDPrint
	{
		// Extra >

		private class ExtraInfo
		{
			public DDTaskList TL = null;
			public I3Color Color = new I3Color(255, 255, 255);
			public I3Color BorderColor = new I3Color(-1, 0, 0);
			public int BorderWidth = 0;
		}

		private static ExtraInfo Extra = new ExtraInfo();

		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		public static void SetTaskList(DDTaskList tl)
		{
			Extra.TL = tl;
		}

		public static void SetColor(I3Color color)
		{
			Extra.Color = color;
		}

		public static void SetBorder(I3Color color, int width = 2)
		{
			Extra.BorderColor = color;
			Extra.BorderWidth = width;
		}

		// < Extra

		private static int P_BaseX;
		private static int P_BaseY;
		private static int P_YStep;
		private static int P_X;
		private static int P_Y;

		public static void SetPrint(int x = 0, int y = 0, int yStep = 32)
		{
			P_BaseX = x;
			P_BaseY = y;
			P_YStep = yStep;
			P_X = 0;
			P_Y = 0;
		}

		public static void PrintRet()
		{
			P_X = 0;
			P_Y += P_YStep;
		}

		private static void Print_Main2(string line, int x, int y, I3Color color, double centeringRate)
		{
			DDFont font = DDFontUtils.GetFont("Kゴシック", 32);

			// centeringRate:
			// 0.0 == 右寄せ (最初の文字の左側面が x になる)
			// 0.5 == 中央寄せ
			// 1.0 == 左寄せ (最後の文字の右側面が x になる)

			x -= SCommon.ToInt(DDFontUtils.GetDrawStringWidth(line, font) * centeringRate);

			DDFontUtils.DrawString(x, y, line, font, false, color);
		}

		private static void Print_Main(string line, int x, int y, double centeringRate)
		{
			if (Extra.BorderWidth != 0)
				for (int xc = -Extra.BorderWidth; xc <= Extra.BorderWidth; xc++)
					for (int yc = -Extra.BorderWidth; yc <= Extra.BorderWidth; yc++)
						Print_Main2(line, x + xc, y + yc, Extra.BorderColor, centeringRate);

			Print_Main2(line, x, y, Extra.Color, centeringRate);
		}

		public static void Print(string line, double centeringRate = 0.0)
		{
			if (line == null)
				throw new DDError();

			int x = P_BaseX + P_X;
			int y = P_BaseY + P_Y;

			if (Extra.TL == null)
			{
				Print_Main(line, x, y, centeringRate);
			}
			else
			{
				ExtraInfo storedExtra = Extra;

				Extra.TL.Add(() =>
				{
					ExtraInfo currExtra = Extra;

					Extra = storedExtra;
					Print_Main(line, x, y, centeringRate);
					Extra = currExtra;

					return false;
				});
			}

			int w = DX.GetDrawStringWidth(line, SCommon.ENCODING_SJIS.GetByteCount(line));

			if (w < 0 || SCommon.IMAX < w)
				throw new DDError();

			P_X += w;
		}

		public static void PrintLine(string line)
		{
			Print(line);
			PrintRet();
		}
	}
}
