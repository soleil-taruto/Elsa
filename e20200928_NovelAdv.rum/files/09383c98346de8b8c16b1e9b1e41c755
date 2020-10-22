using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Charlotte.Tools
{
	public static class ExtraTools
	{
		public static void AntiWindowsDefenderSmartScreen()
		{
			// Chocolate.dll と同じ場所でないと実行出来ないので、CheckAloneExe()は不要と判断した。@ 201x.x.x

			ProcMain.WriteLog("awdss_1");

			if (Is初回起動())
			{
				ProcMain.WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(ProcMain.SelfDir, "*.exe", SearchOption.TopDirectoryOnly))
				{
					try
					{
						ProcMain.WriteLog("awdss_exeFile: " + exeFile);

						if (StringTools.EqualsIgnoreCase(exeFile, ProcMain.SelfFile))
						{
							ProcMain.WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						ProcMain.WriteLog("awdss_OK");
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}
				}
				ProcMain.WriteLog("awdss_3");
			}
			ProcMain.WriteLog("awdss_4");
		}

		private static bool? _初回起動 = null;

		public static bool Is初回起動()
		{
			if (_初回起動 == null)
			{
				string sigFile = ProcMain.SelfFile + ".awdss.sig";

				_初回起動 = File.Exists(sigFile) == false;

				File.WriteAllBytes(sigFile, BinTools.EMPTY);
			}
			return _初回起動.Value;
		}

		public static void SetEnabledDoubleBuffer(Control control)
		{
			control.GetType().InvokeMember(
				"DoubleBuffered",
				BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
				null,
				control,
				new object[] { true }
				);
		}

		private static string SemiRemoveDestDir = null;

		public static void SemiRemovePath(string path, bool keepSrcPath = false)
		{
			if (SemiRemoveDestDir == null)
				SemiRemoveDestDir = MakeFreeDir();

			string destPath = Path.Combine(SemiRemoveDestDir, Path.GetFileName(path));
			destPath = ToCreatablePath(destPath);

			if (File.Exists(path))
			{
				if (keepSrcPath)
					File.Copy(path, destPath);
				else
					File.Move(path, destPath);
			}
			else if (Directory.Exists(path))
			{
				if (keepSrcPath)
					FileTools.CopyDir(path, destPath);
				else
					FileTools.MoveDir(path, destPath);
			}
		}

		/// <summary>
		/// C:\Factory\Common\Mutex.c の COMMONMUTEXNAME と同じ。
		/// </summary>
		public const string FACTORY_COMMONMUTEXNAME = "cerulean.charlotte Factory common mutex object";

		public static string MakeFreeDir()
		{
			using (new MSection(FACTORY_COMMONMUTEXNAME))
			{
				for (int c = 1; ; c++)
				{
					string dir = @"C:\" + c;

					if (Accessible(dir) == false)
					{
						FileTools.CreateDir(dir);
						return dir;
					}
				}
			}
		}

		public static string ToCreatablePath(string path)
		{
			if (Accessible(path))
			{
				string prefix = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
				string suffix = Path.GetExtension(path);

				for (int c = 2; ; c++)
				{
					path = prefix + "~" + c + suffix;

					if (Accessible(path) == false)
						break;
				}
			}
			return path;
		}

		public static bool Accessible(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}

		public static T DesertElement<T>(List<T> list, int index)
		{
			T ret = list[index];
			list.RemoveAt(index);
			return ret;
		}

		public static T UnaddElement<T>(List<T> list)
		{
			return DesertElement(list, list.Count - 1);
		}

		public static T FastDesertElement<T>(List<T> list, int index)
		{
			T ret = UnaddElement(list);

			if (index < list.Count)
			{
				T ret2 = list[index];
				list[index] = ret;
				ret = ret2;
			}
			return ret;
		}

		public static string GetHomeDir(string marker = "_home")
		{
			return GetHomeDir(marker, Directory.GetCurrentDirectory());
		}

		public static string GetHomeDir(string marker, string dir)
		{
			dir = FileTools.MakeFullPath(dir);

			for (; ; )
			{
				string file = Path.Combine(dir, marker);

				if (File.Exists(file))
					break;

				if (dir.Length <= 3) // ? ルートディレクトリに達した。
					throw new Exception("no " + marker);

				dir = Path.GetDirectoryName(dir);
			}
			return dir;
		}

		public const int PATH_SIZE = 250; // C:\Factory\Common\DataConv.h の PATH_SIZE と同じ。

		public static string MakeFullPath_FAT32(string path)
		{
			path = FileTools.MakeFullPath(path);

			if (path != JString.ToJString(path, true, false, false, true))
				throw new Exception("Shift_JIS に変換出来ない文字を含むパスは使用出来ません。");

			if (ExtraTools.PATH_SIZE < StringTools.ENCODING_SJIS.GetByteCount(path))
				throw new Exception("パスが長すぎます。");

			return path;
		}
	}
}
