using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.GameCommons;

namespace Charlotte.Novels.Surfaces
{
	/// <summary>
	/// 現在登場中のキャラクタやオブジェクトの状態を保持する。
	/// </summary>
	public abstract class Surface
	{
		public string TypeName; // ロード時に必要だった_今は不要
		public string InstanceName;

		public Surface(string typeName, string instanceName)
		{
			this.TypeName = typeName;
			this.InstanceName = instanceName;
		}

		/// <summary>
		/// <para>アクションのリスト</para>
		/// <para>Act.Draw が false を返したとき this.Draw を実行しなければならない。</para>
		/// <para>セーブ・ロード時にこのフィールドは保存・再現されない。</para>
		/// <para>アクションは途中で破棄されても良いように実装すること。</para>
		/// <para>-- スキップモード等で Act.Clear が実行されるかもしれない。</para>
		/// </summary>
		public NovelAct Act = new NovelAct();

		public int X = DEFAULT_X;
		public int Y = DEFAULT_Y;
		public int Z = DEFAULT_Z;

		public const int DEFAULT_X = DDConsts.Screen_W / 2;
		public const int DEFAULT_Y = DDConsts.Screen_H / 2;
		public const int DEFAULT_Z = 0;

		/// <summary>
		/// <para>コマンドを実行する。</para>
		/// <para>ここでは共通のコマンドを処理し、個別のコマンドを処理するために Invoke_02 を呼び出す。</para>
		/// </summary>
		/// <param name="command">コマンド名</param>
		/// <param name="arguments">コマンド引数列</param>
		public void Invoke(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "位置")
			{
				if (arguments.Length == 3)
				{
					this.X = int.Parse(arguments[c++]);
					this.Y = int.Parse(arguments[c++]);
					this.Z = int.Parse(arguments[c++]);
				}
				else if (arguments.Length == 2)
				{
					this.X = int.Parse(arguments[c++]);
					this.Y = int.Parse(arguments[c++]);
				}
				else
				{
					throw new DDError();
				}
			}
			else if (command == "X")
			{
				this.X = int.Parse(arguments[c++]);
			}
			else if (command == "Y")
			{
				this.Y = int.Parse(arguments[c++]);
			}
			else if (command == "Z")
			{
				this.Z = int.Parse(arguments[c++]);
			}
			else if (command == "End")
			{
				Novel.I.Status.Surfaces.RemoveAll(v => v == this); // HACK: 多くとも1つしか無いはず。
			}
			else if (command == "EffOff") // Effect Off
			{
				this.Act.Clear();
			}
			else if (command == "Sleep") // 描画せずに待つ
			{
				int frame = int.Parse(arguments[c++]);

				if (frame < 1)
					throw new DDError("Bad (sleeping) frame: " + frame);

				this.Act.Add(SCommon.Supplier(Enumerable.Range(0, frame).Select(v => true)));
			}
			else if (command == "Keep") // 描画しながら待つ
			{
				int frame = int.Parse(arguments[c++]);

				if (frame < 1)
					throw new DDError("Bad (keeping) frame: " + frame);

				this.Act.Add(SCommon.Supplier(Enumerable.Range(0, frame).Select(v =>
				{
					this.Draw();
					return true;
				}
				)));
			}
			else
			{
				this.Invoke_02(command, arguments);
			}
		}

		private Func<bool> _draw = null;

		public void Draw()
		{
			if (_draw == null)
				_draw = SCommon.Supplier(this.E_Draw());

			if (!_draw())
				Novel.I.Status.Surfaces.RemoveAll(v => v == this);
		}

		/// <summary>
		/// 描画する。
		/// </summary>
		/// <returns>このサーフェスを継続するか</returns>
		public abstract IEnumerable<bool> E_Draw();

		/// <summary>
		/// 固有のコマンドを実行する。
		/// </summary>
		/// <param name="command">コマンド名</param>
		/// <param name="arguments">コマンド引数列</param>
		protected virtual void Invoke_02(string command, params string[] arguments)
		{
			throw new DDError();
		}
	}
}
