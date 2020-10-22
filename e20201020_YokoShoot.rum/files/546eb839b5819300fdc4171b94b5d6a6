using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Games.Walls
{
	public abstract class Wall
	{
		/// <summary>
		/// この壁紙を消滅させるか？
		/// この壁紙を消滅させたい場合 true をセットすること。
		/// これにより壁紙リストから除去される。
		/// </summary>
		public bool DeadFlag = false;

		/// <summary>
		/// この壁紙によって画面全体が描画されているか？
		/// この壁紙によって画面全体が描画されている状態が以後ずっと維持される場合 true に設定すること。
		/// -- 一旦 true に設定したら false に戻してはならない。
		/// これにより裏側の壁紙が削除される。
		/// -- E_Draw によって設定される。
		/// </summary>
		public bool FilledFlag = false;

		private Func<bool> _draw = null;

		public void Draw()
		{
			if (_draw == null)
				_draw = SCommon.Supplier(this.E_Draw());

			if (!_draw())
				throw null; // never
		}

		/// <summary>
		/// 現在のフレームにおける描画を行う。
		/// するべきこと：
		/// -- 行動
		/// -- 描画
		/// -- 必要に応じて FilledFlag を設定する。
		/// </summary>
		/// <returns>[列挙]常に真を返すこと</returns>
		public abstract IEnumerable<bool> E_Draw();
	}
}
