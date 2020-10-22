using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	public static class DDFontUtils
	{
		public static List<DDFont> Fonts = new List<DDFont>();

		public static void Add(DDFont font)
		{
			Fonts.Add(font);
		}

		public static void UnloadAll()
		{
			foreach (DDFont font in Fonts)
				font.Unload();
		}

		public static DDFont GetFont(string fontName, int fontSize, int fontThick = 6, bool antiAliasing = true, int edgeSize = 0, bool italicFlag = false)
		{
			DDFont font = Fonts.FirstOrDefault(v =>
				v.FontName == fontName &&
				v.FontSize == fontSize &&
				v.FontThick == fontThick &&
				v.AntiAliasing == antiAliasing &&
				v.EdgeSize == edgeSize &&
				v.ItalicFlag == italicFlag
				);

			if (font == null)
				font = new DDFont(fontName, fontSize, fontThick, antiAliasing, edgeSize, italicFlag);

			return font;
		}

		public static void DrawString(int x, int y, string str, DDFont font, bool tategakiFlag = false)
		{
			DrawString(x, y, str, font, tategakiFlag, new I3Color(255, 255, 255));
		}

		public static void DrawString(int x, int y, string str, DDFont font, bool tategakiFlag, I3Color color)
		{
			DrawString(x, y, str, font, tategakiFlag, color, new I3Color(0, 0, 0));
		}

		public static void DrawString(int x, int y, string str, DDFont font, bool tategakiFlag, I3Color color, I3Color edgeColor)
		{
			if (DDGround.UseVirtualSubScreenSize)
			{
				// 何故か背景の色を文字が吸ってしまうので、文字の色で塗りつぶす。
				DX.FillRectGraph(DDGround.MainScreenForPrint.GetHandle(), 0, 0, DDConsts.Screen_W, DDConsts.Screen_H, color.R, color.G, color.B, 0);

				using (DDGround.MainScreenForPrint.Section())
				{
#if !true
					DrawStringMain(x, y, str, font, tategakiFlag, color, edgeColor);
#else // zantei
					// HACK: MainScreenForPrint に描画してから MainScreen に転写すると何故か薄くなる。
					// -- 苦肉の策として2回描画する。

					DrawStringMain(x, y, str, font, tategakiFlag, color, edgeColor); // 1
					DrawStringMain(x, y, str, font, tategakiFlag, color, edgeColor); // 2
#endif
				}
				if (DX.DrawExtendGraph(0, 0, DDGround.RealScreen_W, DDGround.RealScreen_H, DDGround.MainScreenForPrint.GetHandle(), 1) != 0) // ? 失敗
					throw new DDError();
			}
			else
				DrawStringMain(x, y, str, font, tategakiFlag, color, edgeColor);
		}

		private static void DrawStringMain(int x, int y, string str, DDFont font, bool tategakiFlag, I3Color color, I3Color edgeColor)
		{
			DX.DrawStringToHandle(x, y, str, DDUtils.GetColor(color), font.GetHandle(), DDUtils.GetColor(edgeColor), tategakiFlag ? 1 : 0);
		}

		public static void DrawString_XCenter(int x, int y, string str, DDFont font, bool tategakiFlag = false)
		{
			x -= GetDrawStringWidth(str, font, tategakiFlag) / 2;

			DrawString(x, y, str, font, tategakiFlag);
		}

		public static void DrawString_XCenter(int x, int y, string str, DDFont font, bool tategakiFlag, I3Color color)
		{
			x -= GetDrawStringWidth(str, font, tategakiFlag) / 2;

			DrawString(x, y, str, font, tategakiFlag, color);
		}

		public static void DrawString_XCenter(int x, int y, string str, DDFont font, bool tategakiFlag, I3Color color, I3Color edgeColor)
		{
			x -= GetDrawStringWidth(str, font, tategakiFlag) / 2;

			DrawString(x, y, str, font, tategakiFlag, color, edgeColor);
		}

		public static int GetDrawStringWidth(string str, DDFont font, bool tategakiFlag = false)
		{
			return DX.GetDrawStringWidthToHandle(str, SCommon.ENCODING_SJIS.GetByteCount(str), font.GetHandle(), tategakiFlag ? 1 : 0);
		}
	}
}
