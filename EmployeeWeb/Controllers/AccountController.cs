using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace EmployeeWeb.Controllers;

public class AccountController : Controller
{
    public IActionResult SignOut()
    {
        if (AppServicesAuthenticationInformation.IsAppServicesAadAuthenticationEnabled)
            return LocalRedirect(AppServicesAuthenticationInformation.LogoutUrl);

        var scheme = OpenIdConnectDefaults.AuthenticationScheme;
        var callbackUrl = Url.ActionLink("SignOut", "Account", null, Request.Scheme);
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = callbackUrl
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            scheme);
    }
}