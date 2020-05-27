using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryControl.Models.InventoryItems;

namespace InventoryControl.DAL
{
	public class InventoryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<InventoryContext>
	{
		protected override void Seed(InventoryContext context)
		{
			var inventoryParts = new List<InventoryPart>
			{
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
				new InventoryPart{Name="HD TV",AvailabeNoOfUnits =10,ReorderLevel= 5, UnitPrice=50000},
				new InventoryPart{Name="Camera",AvailabeNoOfUnits =4,ReorderLevel= 10, UnitPrice=10000},
				new InventoryPart{Name="Phone",AvailabeNoOfUnits =20,ReorderLevel= 6, UnitPrice=50000},
			};
				context.SaveChanges();
			}
		}
	}