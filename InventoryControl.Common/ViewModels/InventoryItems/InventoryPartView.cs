using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;

namespace InventoryControl.Common.ViewModels.InventoryItems
{
	public class InventoryPartView
	{
		public int ID { get; set; }
		[StringLength(50)]
		[Required(ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_NameMandatory")]
		[Display(Name = "Label_Name", ResourceType = typeof(InventoryRes))]
		public string Name { get; set; }

		[Required(ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_AvailableNoOfUnitsMandatory")]
		[Display(Name = "Label_AvailabeNoOfUnits", ResourceType = typeof(InventoryRes))]
		[Range(0, int.MaxValue, ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_NegativeValuesNotAllowed")]
		public int AvailabeNoOfUnits { get; set; }

		[Display(Name = "Label_ReorderLevel", ResourceType = typeof(InventoryRes))]
		[Range(0, int.MaxValue, ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_NegativeValuesNotAllowed")]
		public int ReorderLevel { get; set; }

		[Required(ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_UnitPriceMandatory")]
		[Display(Name = "Label_UnitPrice", ResourceType = typeof(InventoryRes))]
		[Range(0, double.MaxValue, ErrorMessageResourceType = typeof(InventoryRes),
			ErrorMessageResourceName = "Error_NegativeValuesNotAllowed")]
		public double UnitPrice { get; set; }
	}
}