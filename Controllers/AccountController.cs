using Microsoft.AspNetCore.Mvc;
using ACRPhone.Webhook.ViewModels;

namespace ACRPhone.Webhook.Controllers
{

    [Route("api")]
    public class AccountController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            return User.Identity.IsAuthenticated
                ? StatusCode(200, "User is authenticated")
                : StatusCode(401, "User is not authenticated");
        }
    }
}
