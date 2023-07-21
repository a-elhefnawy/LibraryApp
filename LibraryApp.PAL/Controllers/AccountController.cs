using LibraryApp.DAL.Models;
using LibraryApp.PAL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.PAL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userVM.UserName);

                if (user != null)
                {
                    var flag = await userManager.CheckPasswordAsync(user, userVM.Password);
                    if (flag)
                    {
                        await signInManager.PasswordSignInAsync(user, userVM.Password,false,false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Password Or UserName");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Wrong Password Or UserName");
                }

            }
            return View(userVM);

        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }


    }
}
