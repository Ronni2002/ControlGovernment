using ControlGovernmentFront.Models;
using ControlGovernmentFront.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControlGovernmentFront.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _authService.LoginAsync(model.Username, model.Password);
                    if (!string.IsNullOrEmpty(token))
                    {
                       
                        HttpContext.Session.SetString("JwtToken", token);
                        return RedirectToAction("Index", "GovernmentEntities");
                    }
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }

            return View(model);
        }
    }
}
