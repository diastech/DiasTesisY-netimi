using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Threading.Tasks;

namespace DIAS.UI.Helpers
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private IUrlHelperFactory _helper;
        private IActionContextAccessor _accessor;
        public CustomCookieAuthenticationEvents(IUrlHelperFactory helper, IActionContextAccessor accessor)
        {
            _helper = helper;
            _accessor = accessor;
        }
        public override Task SignedIn(CookieSignedInContext context)
        {
            return base.SignedIn(context);  
        }
        //public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        //{
        //    return null;
        //}
        
    }
}
