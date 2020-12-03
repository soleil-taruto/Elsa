using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Games.Enemies;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Shots
{
	public class Shot_ほむらシールド : Shot
	{
		public PlayerTracer.MomentInfo[] Moments;

		// <---- prm

		public Shot_ほむらシールド()
			: base(0, 0, false, 0, true, true)
		{ }

		public override IEnumerable<bool> E_Draw()
		{
			// motions 最初の状態を復元
			{
				Game.I.Player.HP = this.Moments[0].HP;
				Game.I.Player.X = this.Moments[0].X;
				Game.I.Player.Y = this.Moments[0].Y;
			}

			Game.I.Enemies.Clear();

			foreach (Enemy enemy in this.Moments[0].Enemies)
				Game.I.Enemies.Add(enemy);

			foreach (PlayerTracer.MomentInfo moment in this.Moments)
			{
				foreach (Action motion in moment.Motions)
				{
					DDTaskList draw_el_bk = Game.I.Player.Draw_EL;

					Game.I.Player.Draw_EL = new DDTaskList();

					motion();

					Game.I.Player.Draw_EL.ExecuteAllTask();
					Game.I.Player.Draw_EL = draw_el_bk;
				}
				yield return true;
			}
			DDGround.EL.Add(SCommon.Supplier(Effects.ほむらシールド終了(
				this.Moments[this.Moments.Length - 1].X,
				this.Moments[this.Moments.Length - 1].Y
				)));
		}

#if false // not used
		private void 終了カットイン()
		{
			DDMain.KeepMainScreen();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DDDraw.DrawSimple(DDGround.KeptMainScreen.ToPicture(), 0, 0);

				DDDraw.SetBright(0, 0, 0);
				DDDraw.SetAlpha(scene.Rate);
				DDDraw.DrawBegin(
					DDGround.GeneralResource.WhiteCircle,
					Game.I.Player.X - DDGround.ICamera.X,
					Game.I.Player.Y - DDGround.ICamera.Y
					);
				DDDraw.DrawZoom(0.3 + 20.0 * (1.0 - scene.Rate));
				DDDraw.DrawEnd();
				DDDraw.Reset();

				DDEngine.EachFrame();
			}
		}
#endif
	}
}
