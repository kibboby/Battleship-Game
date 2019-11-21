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

		[Test ()]
		public void TestBattleShipPlacement ()
		{
			Ship ship = new Ship (ShipName.Tug);
			ship.Deployed (Direction.UpDown, 5, 5);

			Assert.IsTrue (ship.Column == 5);
		}

		[Test ()]
		public void TestShipPlacement ()
		{
			Ship ship = new Ship (ShipName.Tug);
			ship.Deployed (Direction.UpDown, 5, 5);

			Assert.IsTrue (ship.Direction == Direction.UpDown);
		}

	}
}
