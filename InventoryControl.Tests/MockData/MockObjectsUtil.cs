using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryControl.Models.InventoryItems;
using Moq;

namespace InventoryControl.Tests.MockData
{
	public static class MockObjectsUtil
	{
		public static InventoryPart GetMockInventoryPart(int id)
		{
			return new InventoryPart()
			{
				ID = 1,
				Name = "test inventory part",
				ReorderLevel = 5,
				AvailabeNoOfUnits = 40,
				UnitPrice = 100
			};
		}
	}
}
