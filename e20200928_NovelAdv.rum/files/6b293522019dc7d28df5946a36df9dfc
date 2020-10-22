using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HandleDam
	{
		/// <summary>
		/// 終了時に hDam に Add した全てのハンドルを Dispose する。
		/// </summary>
		/// <typeparam name="T">routine の戻り値の型</typeparam>
		/// <param name="routine">hDam を渡して実行する routine</param>
		/// <returns>routine の戻り値</returns>
		public static T Section_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		/// <summary>
		/// 終了時に hDam に Add した全てのハンドルを Dispose する。
		/// </summary>
		/// <param name="routine">hDam を渡して実行する処理</param>
		public static void Section(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		/// <summary>
		/// 例外発生時のみ hDam に Add した全てのハンドルを Dispose する。
		/// </summary>
		/// <typeparam name="T">routine の戻り値</typeparam>
		/// <param name="routine">hDam を渡して実行する routine</param>
		/// <returns>routine の戻り値</returns>
		public static T Transaction_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				throw null; // never
			}
		}

		/// <summary>
		/// 例外発生時のみ hDam に Add した全てのハンドルを Dispose する。
		/// </summary>
		/// <param name="routine">hDam を渡して実行する処理</param>
		public static void Transaction(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				//throw null; // never
			}
		}

		private List<IDisposable> Handles = new List<IDisposable>();

		/// <summary>
		/// ハンドルを追加する。
		/// </summary>
		/// <typeparam name="T">ハンドルの型</typeparam>
		/// <param name="handle">追加するハンドル</param>
		/// <returns>追加したハンドル</returns>
		public T Add<T>(T handle) where T : IDisposable
		{
			this.Handles.Add(handle);
			return handle;
		}

		/// <summary>
		/// <para>全てのハンドルを Dispose する。</para>
		/// <para>1つ以上の Dispose が例外を投げた場合、全ての Dispose を実行し終えた後で AggregateException にまとめて投げる。</para>
		/// </summary>
		public void Burst()
		{
			ExceptionDam.Section(eDam =>
			{
				this.Burst(eDam);
			});
		}

		/// <summary>
		/// <para>全てのハンドルを Dispose する。</para>
		/// <para>全ての Dispose を実行し終えた後で e と Dispose が投げた例外を AggregateException にまとめて投げる。</para>
		/// </summary>
		public void Burst(Exception e)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Add(e);
				this.Burst(eDam);
			});
		}

		private void Burst(ExceptionDam eDam)
		{
			for (int index = this.Handles.Count - 1; 0 <= index; index--)
				eDam.Invoke(() => this.Handles[index].Dispose());

			this.Handles.Clear();
		}
	}
}
