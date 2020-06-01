using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Common.Exceptions
{
	/// <summary>
	/// This exception is thrown when the requeted data does not
	/// exists in the database.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class DataNotFoundException : Exception
	{
		public DataNotFoundException()
		{
		}

		public DataNotFoundException(string message) : base(message)
		{
		}

		public DataNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
