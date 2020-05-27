using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryControl.Models.InventoryItems
{
	public class InventoryPart
	{
		public int ID { get; set; }
		[StringLength(50)]
		public string Name { get; set; }
		public int AvailabeNoOfUnits { get; set; }
		public int ReorderLevel { get; set; }
		public double UnitPrice { get; set; }
	}
}