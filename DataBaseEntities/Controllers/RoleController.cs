using DataBaseEntities.Data;
using DataBaseEntities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            ViewBag.UserRoles = _dbContext.UserRoles.ToList();
            return View(_roleManager.Roles);
        }


        [HttpPost]
        public IActionResult MakeAdmin([FromForm] string personId) {
            var userRole = _dbContext.UserRoles.FirstOrDefault(a => a.UserId == personId);
            var role = _dbContext.Roles.FirstOrDefault(a => a.Name == "Admin");
            userRole.RoleId = role.Id;
            _dbContext.UserRoles.Remove(userRole);
            _dbContext.Add(userRole);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
