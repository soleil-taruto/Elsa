using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.GameCommons
{
	public static class DDResource
	{
		private static bool ReleaseMode;

		public static void INIT()
		{
			ReleaseMode = Directory.Exists(DDConsts.ResourceDir_11);
		}

		public static byte[] Load(string file)
		{
			if (ReleaseMode)
			{
				string datFile = Path.Combine(DDConsts.ResourceDir_11, file);

				if (!File.Exists(datFile))
				{
					datFile = Path.Combine(DDConsts.ResourceDir_12, file);

					if (!File.Exists(datFile))
						throw new Exception(datFile);
				}
				return File.ReadAllBytes(datFile);
			}
			else
			{
				string datFile = Path.Combine(DDConsts.ResourceDir_01, file);

				if (!File.Exists(datFile))
				{
					datFile = Path.Combine(DDConsts.ResourceDir_02, file);

					if (!File.Exists(datFile))
						throw new Exception(datFile);
				}
				return File.ReadAllBytes(datFile);
			}
		}

		public static void Save(string file, byte[] fileData)
		{
			throw null; // 不使用
		}

		/// <summary>
		/// <para>ファイルリストを取得する。</para>
		/// <para>ソート済み</para>
		/// <para>'_' で始まるファイルの除去済み</para>
		/// </summary>
		/// <returns>ファイルリスト</returns>
		public static IEnumerable<string> GetFiles()
		{
			IEnumerable<string> files;

			if (ReleaseMode)
			{
				files = SCommon.Concat(new IEnumerable<string>[]
				{
					Directory.GetFiles(DDConsts.ResourceDir_11, "*", SearchOption.AllDirectories).Select(file => SCommon.ChangeRoot(file, DDConsts.ResourceDir_11)),
					Directory.GetFiles(DDConsts.ResourceDir_12, "*", SearchOption.AllDirectories).Select(file => SCommon.ChangeRoot(file, DDConsts.ResourceDir_12)),
				});
			}
			else
			{
				files = SCommon.Concat(new IEnumerable<string>[]
				{
					Directory.GetFiles(DDConsts.ResourceDir_01, "*", SearchOption.AllDirectories).Select(file => SCommon.ChangeRoot(file, DDConsts.ResourceDir_01)),
					Directory.GetFiles(DDConsts.ResourceDir_02, "*", SearchOption.AllDirectories).Select(file => SCommon.ChangeRoot(file, DDConsts.ResourceDir_02)),
				});
			}

			// '_' で始まるファイルの除去
			// makeDDResourceFile は '_' で始まるファイルを含めない。
			files = files.Where(file => Path.GetFileName(file)[0] != '_');

			// ソート
			// makeDDResourceFile はファイルリストを sortJLinesICase している。
			// ここでソートする必要は無いが、戻り値に統一性を持たせるため(毎回ファイルの並びが違うということのないように)ソートしておく。
			files = SCommon.Sort(files, SCommon.CompIgnoreCase);

			return files;
		}
	}
}
