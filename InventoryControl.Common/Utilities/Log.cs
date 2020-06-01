using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace InventoryControl.Common.Utilities
{
	public class Log
	{
		private static readonly Log _instance = new Log();
		protected ILog logger;

		private Log()
		{
			logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		/// <summary>  
		/// Used to log Debug messages in an explicit Debug Logger  
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Debug(string message)
		{
			_instance.logger.Debug(message);
		}


		/// <summary>  
		///  Used to log Debug messages along with the exception in an explicit Debug Logger 
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Debug(string message, System.Exception exception)
		{
			_instance.logger.Debug(message, exception);
		}


		/// <summary>  
		///  Used to log information messages.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Info(string message)
		{
			_instance.logger.Info(message);
		}


		/// <summary>  
		///  Used to log information messages along with the exception.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Info(string message, System.Exception exception)
		{
			_instance.logger.Info(message, exception);
		}

		/// <summary>  
		///  Used to log warning messages.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Warn(string message)
		{
			_instance.logger.Warn(message);
		}

		/// <summary>  
		///  Used to log warning mesages along with the exception.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Warn(string message, System.Exception exception)
		{
			_instance.logger.Warn(message, exception);
		}

		/// <summary>  
		///  Used to log error messages along with the exception.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Error(string message)
		{
			_instance.logger.Error(message);
		}

		/// <summary>  
		///  Used to log error messages along with the exception.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Error(string message, System.Exception exception)
		{
			_instance.logger.Error(message, exception);
		}


		/// <summary>  
		///  Used to log fatal messages.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		public static void Fatal(string message)
		{
			_instance.logger.Fatal(message);
		}

		/// <summary>  
		///  Used to log fatal messages along with the exception.
		/// </summary>  
		/// <param name="message">The object message to log</param>  
		/// <param name="exception">The exception to log, including its stack trace </param>  
		public static void Fatal(string message, System.Exception exception)
		{
			_instance.logger.Fatal(message, exception);
		}
	}
}