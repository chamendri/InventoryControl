using InventoryControl.Common.ViewModels.InventoryItems;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.Tests.MockData
{
	public static class MockObjectsUtil
	{
		public static InventoryPartDto GetMockInventoryPart(int id)
		{
			return new InventoryPartDto()
			{
				ID = 1,
				Name = "test inventory part",
				ReorderLevel = 5,
				AvailabeNoOfUnits = 40,
				UnitPrice = 100
			};
		}

		public static InventoryPartView GetMockInventoryPartView(int id)
		{
			return new InventoryPartView()
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
