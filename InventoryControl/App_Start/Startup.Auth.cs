using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;

namespace InventoryControl
{
	public partial class Startup
	{
		// Calling the keys values from Web.config file  
		private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
		private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
		private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
		private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];


		// Concatenate aadInstance, tenant to form authority value       
		private string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);


		/// <summary>
		/// Configures the authentication.
		/// </summary>
		/// <param name="app">The application.</param>
		public void ConfigureAuth(IAppBuilder app)
		{
			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions());

			app.UseOpenIdConnectAuthentication(
				new OpenIdConnectAuthenticationOptions
				{
					ClientId = clientId,
					Authority = authority,
					PostLogoutRedirectUri = postLogoutRedirectUri,
					Notifications = new OpenIdConnectAuthenticationNotifications
					{
						AuthenticationFailed = (context) =>
						{
							context.HandleResponse();
							context.OwinContext.Response.Redirect("/Home/Index");
							return Task.FromResult(0);
						}
					}
				}
			);
		}  
	}
}