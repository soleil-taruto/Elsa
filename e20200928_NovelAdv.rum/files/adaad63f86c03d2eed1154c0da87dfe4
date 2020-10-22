using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Charlotte.Tools
{
	public class Canvas2
	{
		private Image Image;

		public Canvas2(int w, int h)
		{
			if (CanvasTools.IsFairImageSize(w, h, 100000000) == false)
				throw new ArgumentException("Bad w, h: " + w + ", " + h);

			this.Image = new Bitmap(w, h);
		}

		public Canvas2(String file)
			: this(File.ReadAllBytes(file))
		{ }

		public Canvas2(byte[] raw)
			: this(CanvasTools.GetImage(raw))
		{ }

		public Canvas2(Image image)
		{
			this.Image = CanvasTools.CopyImage(image);
		}

		public int GetWidth()
		{
			return this.Image.Width;
		}

		public int GetHeight()
		{
			return this.Image.Height;
		}

		public Image GetImage()
		{
			return this.Image;
		}

		public byte[] GetBytes()
		{
			return CanvasTools.GetBytes(this.Image);
		}

		public byte[] GetBytes(ImageFormat format, int quality = -1)
		{
			return CanvasTools.GetBytes(this.Image, format, quality);
		}

		public void Save(string file)
		{
			File.WriteAllBytes(file, this.GetBytes());
		}

		public void Save(string file, ImageFormat format, int quality = -1)
		{
			File.WriteAllBytes(file, this.GetBytes(format, quality));
		}

		public Canvas ToCanvas()
		{
			return new Canvas(this.Image);
		}

		public Graphics GetGraphics(bool antiAliasing = true)
		{
			Graphics g = Graphics.FromImage(this.Image);

			if (antiAliasing)
			{
				g.TextRenderingHint = TextRenderingHint.AntiAlias;
				g.SmoothingMode = SmoothingMode.AntiAlias;
			}
			return g;
		}

		// memo: g.DrawString() の x, y は、描画した文字列の左上の座標っぽい。余白が入るので文字本体は座標より少し離れる。

		// xRate == -0.5 ==> 中央に寄ってくれる。
		// yRate == -0.5 ==> 多少ズレる。多分上下にある余白のせい。メイリオでは上にズレる。多分フォントに依る。

		public const double DRAW_STRING_DEFAULT_X_RATE = -0.5;
		public const double DRAW_STRING_DEFAULT_Y_RATE = -0.5;

		public void DrawString(String str, Font font, Color color, int x, int y, double xRate = DRAW_STRING_DEFAULT_X_RATE, double yRate = DRAW_STRING_DEFAULT_Y_RATE)
		{
			using (Graphics g = this.GetGraphics())
			{
				SizeF size = g.MeasureString(str, font);

				g.DrawString(str, font, new SolidBrush(color), (float)(x + size.Width * xRate), (float)(y + size.Height * yRate));
			}
		}
	}
}
