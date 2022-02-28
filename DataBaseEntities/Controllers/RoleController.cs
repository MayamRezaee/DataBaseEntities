using DataBaseEntities.Data;
using DataBaseEntities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Controllers
{
    [Authorize(Roles="Admin")]
    public class RoleController :Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _dbContext;

        public RoleController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
       {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = context;

       }

        public IActionResult Index()
        {

            return View(_roleManager.Roles);
        }


        

        public IActionResult MakeAdmin()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name", "Name");
            ViewData["Users"] = new SelectList(_userManager.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string role, string user)
        {
            var _user = await _userManager.FindByIdAsync(user);

            IdentityResult result = await _userManager.AddToRoleAsync(_user, role);
            return RedirectToAction("Index");
         }
    }
}
