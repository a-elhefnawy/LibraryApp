using LibraryApp.DAL.Models;
using LibraryApp.PAL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.PAL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users= await userManager.Users.ToListAsync();
            List<UserVM> usersVM = new List<UserVM>();
            foreach (var user in users)
            {
                UserVM userVM = new UserVM()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email=user.Email,
                    UserName = user.UserName,
                    Password=user.PasswordHash
                };
                usersVM.Add(userVM);
            }

            return View(usersVM);
        }

        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                if (await userManager.FindByNameAsync(userVM.UserName) != null)
                {
                    ModelState.AddModelError("", "This UserName has been used before ");
                    return View(userVM);
                }
                if (await userManager.FindByEmailAsync(userVM.Email) != null)
                {
                    ModelState.AddModelError("", "This Email has been used before");
                    return View(userVM);
                }
                AppUser user = new AppUser()
                {
                    Name = userVM.Name,
                    Email = userVM.Email,
                    UserName = userVM.UserName,
                    PasswordHash = userVM.Password
                };
                var result = await userManager.CreateAsync(user, userVM.Password);
                if (!result.Succeeded) { return View(userVM); }
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            UserVM userVm = new UserVM()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                UserName = user.UserName,
            };
            return View(userVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserVM userVM)
        {
            var user = await userManager.FindByIdAsync(userVM.Id);
            if(user == null) { return NotFound(); }
            user.Name= userVM.Name;
            user.Email= userVM.Email;
            user.UserName= userVM.UserName;
            var result = await userManager.UpdateAsync(user);
            if(!result.Succeeded) { return View(userVM); }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user= await userManager.FindByIdAsync(id);
            if(user == null) { return NotFound(); };
            var result=await userManager.DeleteAsync(user);
            if(!result.Succeeded) { return View("Error"); }
            return RedirectToAction("Index");

        }
    }
}
