using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Games.Walls
{
	public abstract class Wall
	{
		private Func<bool> _draw = null;

		/// <summary>
		/// 壁紙を描画する。
		/// </summary>
		/// <param name="xRate">マップにおける画面の位置(X位置_レート)</param>
		/// <param name="yRate">マップにおける画面の位置(Y位置_レート)</param>
		public void Draw(double xRate, double yRate)
		{
			if (_draw == null)
				_draw = SCommon.Supplier(this.E_Draw());

			this.DrawXRate = xRate;
			this.DrawYRate = yRate;

			if (!_draw())
				throw null; // never
		}

		/// <summary>
		/// マップにおける画面の位置(X位置_レート)
		/// </summary>
		protected double DrawXRate;

		/// <summary>
		/// マップにおける画面の位置(Y位置_レート)
		/// </summary>
		protected double DrawYRate;

		/// <summary>
		/// 壁紙を描画する。
		/// 以下のフィールドの値が保証される。
		/// -- this.DrawXRate
		/// -- this.DrawYRate
		/// </summary>
		/// <returns>列挙：真を返し続けること</returns>
		protected abstract IEnumerable<bool> E_Draw();
	}
}
