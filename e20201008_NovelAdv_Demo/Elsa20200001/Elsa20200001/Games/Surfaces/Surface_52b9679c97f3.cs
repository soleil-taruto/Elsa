﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;
using Charlotte.Commons;

namespace Charlotte.Games.Surfaces
{
	public class Surface_効果音 : Surface
	{
		private class SEInfo
		{
			public string Name;
			public DDSE SE;

			public SEInfo(string name, DDSE se)
			{
				this.Name = name;
				this.SE = se;
			}
		}

		#region SEList

		private SEInfo[] SEList = new SEInfo[]
		{
			new SEInfo("Coin01", Ground.I.SE.Coin01),
			new SEInfo("Coin02", Ground.I.SE.Coin02),
			new SEInfo("Coin03", Ground.I.SE.Coin03),
			new SEInfo("Coin04", Ground.I.SE.Coin04),
			new SEInfo("Coin05", Ground.I.SE.Coin05),
			new SEInfo("Coin06", Ground.I.SE.Coin06),
			new SEInfo("Coin07", Ground.I.SE.Coin07),
			new SEInfo("Coin08", Ground.I.SE.Coin08),
			new SEInfo("Pickup01", Ground.I.SE.Pickup01),
			new SEInfo("Pickup02", Ground.I.SE.Pickup02),
			new SEInfo("Pickup03", Ground.I.SE.Pickup03),
			new SEInfo("Pickup04", Ground.I.SE.Pickup04),
			new SEInfo("Poka01", Ground.I.SE.Poka01),
			new SEInfo("Poka02", Ground.I.SE.Poka02),
			new SEInfo("Poka03", Ground.I.SE.Poka03),
			new SEInfo("ShPickup01", Ground.I.SE.ShPickup01),
			new SEInfo("ShPickup02", Ground.I.SE.ShPickup02),
			new SEInfo("ShPickup03", Ground.I.SE.ShPickup03),
			new SEInfo("Laser01", Ground.I.SE.Laser01),
			new SEInfo("Laser02", Ground.I.SE.Laser02),
			new SEInfo("Laser03", Ground.I.SE.Laser03),
			new SEInfo("Laser04", Ground.I.SE.Laser04),
			new SEInfo("Laser05", Ground.I.SE.Laser05),
			new SEInfo("Laser06", Ground.I.SE.Laser06),
			new SEInfo("Laser07", Ground.I.SE.Laser07),
			new SEInfo("Laser08", Ground.I.SE.Laser08),
			new SEInfo("Shoot01", Ground.I.SE.Shoot01),
			new SEInfo("Shoot02", Ground.I.SE.Shoot02),
			new SEInfo("Shoot03", Ground.I.SE.Shoot03),
			new SEInfo("Shoot04", Ground.I.SE.Shoot04),
			new SEInfo("Shoot05", Ground.I.SE.Shoot05),
			new SEInfo("Attack01", Ground.I.SE.Attack01),
			new SEInfo("Attack02", Ground.I.SE.Attack02),
			new SEInfo("Attack03", Ground.I.SE.Attack03),
			new SEInfo("Attack04", Ground.I.SE.Attack04),
			new SEInfo("Explosion01", Ground.I.SE.Explosion01),
			new SEInfo("Explosion02", Ground.I.SE.Explosion02),
			new SEInfo("Explosion03", Ground.I.SE.Explosion03),
			new SEInfo("Explosion04", Ground.I.SE.Explosion04),
			new SEInfo("Explosion05", Ground.I.SE.Explosion05),
			new SEInfo("Explosion06", Ground.I.SE.Explosion06),
			new SEInfo("Explosion07", Ground.I.SE.Explosion07),
			new SEInfo("Explosion08", Ground.I.SE.Explosion08),
			new SEInfo("Explosion09", Ground.I.SE.Explosion09),
			new SEInfo("Powerdown01", Ground.I.SE.Powerdown01),
			new SEInfo("Powerdown02", Ground.I.SE.Powerdown02),
			new SEInfo("Powerdown03", Ground.I.SE.Powerdown03),
			new SEInfo("Powerdown04", Ground.I.SE.Powerdown04),
			new SEInfo("Powerdown05", Ground.I.SE.Powerdown05),
			new SEInfo("Powerdown06", Ground.I.SE.Powerdown06),
			new SEInfo("Powerdown07", Ground.I.SE.Powerdown07),
			new SEInfo("Powerup01", Ground.I.SE.Powerup01),
			new SEInfo("Powerup02", Ground.I.SE.Powerup02),
			new SEInfo("Powerup03", Ground.I.SE.Powerup03),
			new SEInfo("Powerup04", Ground.I.SE.Powerup04),
			new SEInfo("Powerup05", Ground.I.SE.Powerup05),
			new SEInfo("Powerup06", Ground.I.SE.Powerup06),
			new SEInfo("Powerup07", Ground.I.SE.Powerup07),
			new SEInfo("Powerup08", Ground.I.SE.Powerup08),
			new SEInfo("Powerup09", Ground.I.SE.Powerup09),
			new SEInfo("Powerup10", Ground.I.SE.Powerup10),
			new SEInfo("Hit01", Ground.I.SE.Hit01),
			new SEInfo("Hit02", Ground.I.SE.Hit02),
			new SEInfo("Hit03", Ground.I.SE.Hit03),
			new SEInfo("Hit04", Ground.I.SE.Hit04),
			new SEInfo("Hit05", Ground.I.SE.Hit05),
			new SEInfo("Hit06", Ground.I.SE.Hit06),
			new SEInfo("Hit07", Ground.I.SE.Hit07),
			new SEInfo("Jump01", Ground.I.SE.Jump01),
			new SEInfo("Jump02", Ground.I.SE.Jump02),
			new SEInfo("Jump03", Ground.I.SE.Jump03),
			new SEInfo("Jump04", Ground.I.SE.Jump04),
			new SEInfo("Jump05", Ground.I.SE.Jump05),
			new SEInfo("Jump06", Ground.I.SE.Jump06),
			new SEInfo("Jump07", Ground.I.SE.Jump07),
			new SEInfo("Jump08", Ground.I.SE.Jump08),
			new SEInfo("Jump09", Ground.I.SE.Jump09),
			new SEInfo("Jump10", Ground.I.SE.Jump10),
			new SEInfo("Jump11", Ground.I.SE.Jump11),
			new SEInfo("Jump12", Ground.I.SE.Jump12),
			new SEInfo("Jump13", Ground.I.SE.Jump13),
			new SEInfo("Blip01", Ground.I.SE.Blip01),
			new SEInfo("Blip02", Ground.I.SE.Blip02),
			new SEInfo("Blip03", Ground.I.SE.Blip03),
			new SEInfo("Blip04", Ground.I.SE.Blip04),
			new SEInfo("Select01", Ground.I.SE.Select01),
			new SEInfo("Select02", Ground.I.SE.Select02),
			new SEInfo("Select03", Ground.I.SE.Select03),
			new SEInfo("Select04", Ground.I.SE.Select04),
			new SEInfo("Select05", Ground.I.SE.Select05),
			new SEInfo("Select06", Ground.I.SE.Select06),
			new SEInfo("Select07", Ground.I.SE.Select07),
			new SEInfo("Select08", Ground.I.SE.Select08),
			new SEInfo("Select09", Ground.I.SE.Select09),
			new SEInfo("Select10", Ground.I.SE.Select10),
		};

		#endregion

		public override void Draw()
		{
			// noop
		}

		protected override void Invoke_02(string command, params string[] arguments)
		{
			int c = 0;

			if (command == "再生")
			{
				string name = arguments[c++];
				int index = SCommon.IndexOf(this.SEList, v => v.Name == name);

				if (index == -1)
					throw new DDError("Bad (se) name: " + name);

				this.Act.Add(() =>
				{
					this.SEList[index].SE.Play();
					return false;
				});
			}
			else
			{
				throw new DDError();
			}
		}
	}
}
