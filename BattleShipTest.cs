using System;
using SwinGameSDK;
using System.Collections.Generic;
using NUnit.Framework;

namespace BattleShips
{
	[TestFixture()]
	public class BattleShipTest
	{
		[Test()]
		public void TestSelectedBattleShip ()
		{
			ShipName selectedBS = ShipName.Tug;
			ShipName choose = default (ShipName);

			if (choose == ShipName.None) {
				selectedBS = choose;
			}

			Assert.AreEqual (choose, selectedBS);
		}
	}
}
