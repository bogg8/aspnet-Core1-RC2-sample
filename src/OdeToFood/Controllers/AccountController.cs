namespace OdeToFood.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using OdeToFood.Entities;
    using OdeToFood.ViewModels;

    public class AccountController : Controller
    {
        private UserManager<User> _userManager;

        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ViewResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
