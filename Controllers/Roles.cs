using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEET.Controllers
{
    //    public class Roles : Controller
    //    {
    //        public IActionResult Index()
    //        {
    //            return View();
    //        }
    //    }
    //}
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
{
    RoleManager<IdentityRole> roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }
        [Authorize(Policy = "readpolicy")]
        public IActionResult Index()
    {
        var roles = roleManager.Roles.ToList();
        return View(roles);
    }
        [Authorize(Policy = "writepolicy")]

        public IActionResult Create()
    {
        return View(new IdentityRole());
    }

    [HttpPost]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IdentityRole role)
    {
        await roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
}  
}  