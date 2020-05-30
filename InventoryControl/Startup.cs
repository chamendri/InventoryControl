//using System;
//using System.Threading.Tasks;
//using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(InventoryControl.Startup))]

namespace InventoryControl
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
