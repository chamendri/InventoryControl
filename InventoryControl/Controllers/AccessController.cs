using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace InventoryControl.Controllers
{
    public class AccessController : BaseController
    {
		/// <summary>
		/// Sends an OpenIDConnect Sign-In Request.		
		/// </summary>
		public void SignIn()
		{
			if(!Request.IsAuthenticated)
			{
				HttpContext.GetOwinContext()
					.Authentication.Challenge(
						new AuthenticationProperties { RedirectUri = "/" },
						OpenIdConnectAuthenticationDefaults.AuthenticationType);
			}
		}

		/// <summary>
		/// Signs the user out and clears the cache of access tokens.  	
		/// </summary>
		public void SignOut()
		{
			HttpContext.GetOwinContext().Authentication.SignOut(
				OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
		}
	}
}