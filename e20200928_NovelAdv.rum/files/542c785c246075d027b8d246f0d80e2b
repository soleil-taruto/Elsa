using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte.Tools
{
	public class Canvas
	{
		private uint[] Dots;
		private int Width;

		public Canvas(int w, int h)
		{
			if (CanvasTools.IsFairImageSize(w, h, 9000000) == false)
				throw new ArgumentException("Bad w, h: " + w + ", " + h);

			this.Dots = new uint[w * h];
			this.Width = w;
		}

		public Canvas(string file)
			: this(File.ReadAllBytes(file))
		{ }

		public Canvas(byte[] raw)
			: this(CanvasTools.GetImage(raw))
		{ }

		public Canvas(Image image)
		{
			int w = image.Width;
			int h = image.Height;

			this.Dots = new uint[w * h];
			this.Width = w;

			using (Bitmap bmp = new Bitmap(image))
			{
				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						this.Set(x, y, bmp.GetPixel(x, y));
					}
				}
			}
		}

		public int GetWidth()
		{
			return this.Width;
		}

		public int GetHeight()
		{
			return this.Dots.Length / this.Width;
		}

		private bool IsFairPoint(int x, int y)
		{
			return 0 <= x && x < this.GetWidth() && 0 <= y && y < this.GetHeight();
		}

		public static Color DEFAULT_COLOR = Color.FromArgb(255, 255, 255, 0);

		public Color Get(int x, int y)
		{
			if (this.IsFairPoint(x, y))
			{
				return Color.FromArgb((int)this.Dots[x + y * this.Width]);
			}
			else
			{
				return DEFAULT_COLOR;
			}
		}

		public void Set(int x, int y, Color color)
		{
			if (this.IsFairPoint(x, y))
			{
				this.Dots[x + y * this.Width] = (uint)color.ToArgb();
			}
		}

		public void Put(int x, int y, Color color)
		{
			this.Set(x, y, CanvasTools.Cover(this.Get(x, y), color));
		}

		public Image GetImage()
		{
			Bitmap image = new Bitmap(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					image.SetPixel(x, y, this.Get(x, y));
				}
			}
			return image;
		}

		public byte[] GetBytes()
		{
			return CanvasTools.GetBytes(this.GetImage());
		}

		public byte[] GetBytes(ImageFormat format, int quality = -1)
		{
			return CanvasTools.GetBytes(this.GetImage(), format, quality);
		}

		public void Save(string file)
		{
			File.WriteAllBytes(file, this.GetBytes());
		}

		public void Save(string file, ImageFormat format, int quality = -1)
		{
			File.WriteAllBytes(file, this.GetBytes(format, quality));
		}

		public Canvas2 ToCanvas2()
		{
			return new Canvas2(this.GetImage());
		}

		public void Paste(Canvas prm, int l = 0, int t = 0)
		{
			for (int x = 0; x < prm.GetWidth(); x++)
			{
				for (int y = 0; y < prm.GetHeight(); y++)
				{
					this.Set(l + x, t + y, prm.Get(x, y));
				}
			}
		}

		public void Cover(Canvas prm, int l = 0, int t = 0)
		{
			for (int x = 0; x < prm.GetWidth(); x++)
			{
				for (int y = 0; y < prm.GetHeight(); y++)
				{
					this.Put(l + x, t + y, prm.Get(x, y));
				}
			}
		}

		public Canvas Copy()
		{
			return this.Copy(0, 0, this.GetWidth(), this.GetHeight());
		}

		public Canvas Copy(I4Rect rect)
		{
			return this.Copy(rect.L, rect.T, rect.W, rect.H);
		}

		public Canvas Copy(int l, int t, int w, int h)
		{
			Canvas ret = new Canvas(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					ret.Set(x, y, this.Get(l + x, t + y));
				}
			}
			return ret;
		}

		public void Fill(Color color)
		{
			this.FillRect(color, 0, 0, this.GetWidth(), this.GetHeight());
		}

		public void Fill(Color color, I4Rect rect)
		{
			this.FillRect(color, rect.L, rect.T, rect.W, rect.H);
		}

		public void FillRect(Color color, int l, int t, int w, int h)
		{
			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					this.Set(l + x, t + y, color);
				}
			}
		}

		public void Cover(Color color)
		{
			this.CoverRect(color, 0, 0, this.GetWidth(), this.GetHeight());
		}

		public void CoverRect(Color color, I4Rect rect)
		{
			this.CoverRect(color, rect.L, rect.T, rect.W, rect.H);
		}

		public void CoverRect(Color color, int l, int t, int w, int h)
		{
			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					this.Put(l + x, t + y, color);
				}
			}
		}

		public void DrawCircle(Color color, double centerX, double centerY, double r, double r2 = 0.0)
		{
			const int LV = 16;

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					int inside = 0;

					for (int xc = 0; xc < LV; xc++)
					{
						for (int yc = 0; yc < LV; yc++)
						{
							double xx = x - 0.5 + (xc + 0.5) / LV;
							double yy = y - 0.5 + (yc + 0.5) / LV;

							xx -= centerX;
							yy -= centerY;

							double rr = xx * xx + yy * yy;

							if (r2 * r2 <= rr && rr <= r * r)
							{
								inside++;
							}
						}
					}

					this.Put(x, y, Color.FromArgb(
						(int)(color.A * inside / (double)(LV * LV) + 0.5),
						color.R,
						color.G,
						color.B
						));
				}
			}
		}

		public Canvas Twist()
		{
			Canvas ret = new Canvas(this.GetHeight(), this.GetWidth());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret.Set(y, x, this.Get(x, y));
				}
			}
			return ret;
		}

		public Canvas VTurn()
		{
			Canvas ret = new Canvas(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret.Set(x, this.GetHeight() - 1 - y, this.Get(x, y));
				}
			}
			return ret;
		}

		public Canvas HTurn()
		{
			Canvas ret = new Canvas(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret.Set(this.GetWidth() - 1 - x, y, this.Get(x, y));
				}
			}
			return ret;
		}

		public Canvas Rotate(int degree)
		{
			degree %= 360;
			degree += 360;
			degree %= 360;

			switch (degree)
			{
				case 0:
					//return this;
					return this.Copy();

				case 90:
					return this.VTurn().Twist();

				case 180:
					return this.VTurn().HTurn();

				case 270:
					return this.Twist().VTurn();

				default:
					throw new ArgumentException();
			}
		}

		public Canvas CutoutUnmatch(Predicate<I2Point> match)
		{
			return this.Copy(GetRectMatch(match));
		}

		public I4Rect GetRectMatch(Predicate<I2Point> match)
		{
			int l = int.MaxValue;
			int t = int.MaxValue;
			int r = -1;
			int b = -1;

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					if (match(new I2Point(x, y)))
					{
						l = Math.Min(l, x);
						t = Math.Min(t, y);
						r = Math.Max(r, x);
						b = Math.Max(b, y);
					}
				}
			}
			if (r == -1)
				throw new Exception("マッチするピクセルはありませんでした。");

			int w = r - l + 1;
			int h = b - t + 1;

			return new I4Rect(l, t, w, h);
		}

		public I4Rect GetRectSpread(int startX, int startY, Predicate<I2Point> match)
		{
			int l = int.MaxValue;
			int t = int.MaxValue;
			int r = -1;
			int b = -1;

			this.Spread(startX, startY, pt =>
			{
				if (match(pt))
				{
					int x = pt.X;
					int y = pt.Y;

					l = Math.Min(l, x);
					t = Math.Min(t, y);
					r = Math.Max(r, x);
					b = Math.Max(b, y);

					return true;
				}
				return false;
			});

			if (r == -1)
				throw new Exception("マッチするピクセルはありませんでした。");

			int w = r - l + 1;
			int h = b - t + 1;

			return new I4Rect(l, t, w, h);
		}

		public I4Rect GetRectSameColor(int startX, int startY)
		{
			Color targetColor = this.Get(startX, startY);

			return this.GetRectSpread(startX, startY, pt =>
			{
				int x = pt.X;
				int y = pt.Y;

				return this.Get(x, y) == targetColor;
			});
		}

		public void FillSameColor(int startX, int startY, Color color)
		{
			this.SpreadSameColor(startX, startY, pt => this.Set(pt.X, pt.Y, color));
		}

		public void SpreadSameColor(int startX, int startY, Action<I2Point> reaction)
		{
			Color targetColor = this.Get(startX, startY);

			this.Spread(startX, startY, pt =>
			{
				int x = pt.X;
				int y = pt.Y;

				if (this.Get(x, y) == targetColor)
				{
					reaction(pt);
					return true;
				}
				return false;
			});
		}

		public void Spread(int startX, int startY, Predicate<I2Point> match)
		{
			BitTable reachedMap = new BitTable(this.GetWidth(), this.GetHeight());
			Queue<I2Point> pts = new Queue<I2Point>();

			pts.Enqueue(new I2Point(startX, startY));

			while (1 <= pts.Count)
			{
				I2Point pt = pts.Dequeue();
				int x = pt.X;
				int y = pt.Y;

				if (this.IsFairPoint(x, y) && reachedMap.GetBit(x, y) == false && match(pt))
				{
					reachedMap.SetBit(x, y, true);
					pts.Enqueue(new I2Point(x - 1, y));
					pts.Enqueue(new I2Point(x + 1, y));
					pts.Enqueue(new I2Point(x, y - 1));
					pts.Enqueue(new I2Point(x, y + 1));
				}
			}
		}
	}
}
