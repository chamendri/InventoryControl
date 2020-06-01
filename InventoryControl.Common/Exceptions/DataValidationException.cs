using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Common.Exceptions
{
	/// <summary>
	/// This exception is thrown when there is a data validation
	/// error.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class DataValidationException : Exception
	{
		#region constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DataValidationException"/> class.
		/// </summary>
		public DataValidationException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataValidationException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public DataValidationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DataValidationException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public DataValidationException(string message, Exception inner) : base(message, inner)
		{
		}
		#endregion
	}
}
