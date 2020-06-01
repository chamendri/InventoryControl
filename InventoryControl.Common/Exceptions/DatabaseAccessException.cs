using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Common.Exceptions
{
	/// <summary>
	/// This exception is thrown when there is a problem accessing
	/// the database
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class DatabaseAccessException : Exception
	{
		public DatabaseAccessException()
		{
		}

		public DatabaseAccessException(string message) : base(message)
		{
		}

		public DatabaseAccessException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
