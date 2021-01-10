using Microsoft.AspNetCore.Mvc;

namespace ThAmCo.Accounts.Controllers.WebApp
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            var isCookieStored = Request.Cookies.TryGetValue("access_token", out _);
            if (isCookieStored)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
