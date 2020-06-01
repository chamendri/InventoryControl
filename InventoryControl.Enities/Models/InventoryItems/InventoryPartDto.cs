using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryControl.Models.InventoryItems
{
	[Table("InventoryPart")]
	public class InventoryPartDto
	{
		public int ID { get; set; }
		[StringLength(50)]
		[Required]
		public string Name { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int AvailabeNoOfUnits { get; set; }

		[Range(0, int.MaxValue)]
		public int ReorderLevel { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public double UnitPrice { get; set; }
	}
}