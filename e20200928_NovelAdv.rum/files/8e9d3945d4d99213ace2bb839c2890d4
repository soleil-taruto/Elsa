using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte.Tools
{
	public static class CanvasTools
	{
		public static bool IsFairImageSize(int w, int h, int maxDotNum)
		{
			return 1 <= w && w <= maxDotNum && 1 <= h && h <= maxDotNum && w <= maxDotNum / h;
		}

		public static Image CopyImage(Image src)
		{
			Image dest = new Bitmap(src.Width, src.Height);

			using (Graphics g = Graphics.FromImage(dest))
			{
				//g.DrawImage(src, 0, 0); // 画像サイズと異なる幅・高さで描画されることがある。-> 幅・高さは指定するべき。@ 2020.4.20
				g.DrawImage(src, 0, 0, src.Width, src.Height);
			}
			return dest;
		}

		public static Image GetImage(byte[] raw)
		{
			using (MemoryStream mem = new MemoryStream(raw))
			{
				return Bitmap.FromStream(mem);
			}
		}

		public static byte[] GetBytes(Image image)
		{
			return GetBytes(image, ImageFormat.Png);
		}

		public static byte[] GetBytes(Image image, ImageFormat format, int quality = -1)
		{
			// quarity: 0 ～ 100 == 低画質 ～ 高画質, -1 == 無効

			using (MemoryStream mem = new MemoryStream())
			{
				if (format == ImageFormat.Jpeg && quality != -1)
				{
					if (quality < 0 || 100 < quality)
						throw new ArgumentException("Bad quality: " + quality);

					using (EncoderParameters eps = new EncoderParameters(1))
					using (EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality))
					{
						eps.Param[0] = ep;
						ImageCodecInfo ici = GetICI(format);
						image.Save(mem, ici, eps);
					}
				}
				else
				{
					image.Save(mem, format);
				}
				return mem.ToArray();
			}
		}

		private static ImageCodecInfo GetICI(ImageFormat format)
		{
			return ImageCodecInfo.GetImageEncoders().First(ici => ici.FormatID == format.Guid);
		}

		public static Color Cover(Color back, Color fore)
		{
			int fa = fore.A;
			int ba = back.A;

			ba = (int)((ba * (255 - fa)) / 255.0 + 0.5);

			return Color.FromArgb(
				ba + fa,
				(int)((ba * back.R + fa * fore.R) / (double)(ba + fa) + 0.5),
				(int)((ba * back.G + fa * fore.G) / (double)(ba + fa) + 0.5),
				(int)((ba * back.B + fa * fore.B) / (double)(ba + fa) + 0.5)
				);
		}
	}
}
