using FLEET.Data;
using FLEET.Models;
using FLEET.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FLEET.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class LicenceController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext _context;


        public LicenceController(ApplicationDbContext context,  IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;

        }
        [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Licence.ToListAsync();
            var licences = await _context.Licences.ToListAsync();
            var licence = from s in _context.Licences
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                licence = licence.Where(s => s.LicenceNumber.Contains(searchString));
                // || s.FirstMidName.Contains(searchString));
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            {
                int pageSize = 2;
                return View(await PaginatedList<Licence>.CreateAsync(licence.AsNoTracking(), pageNumber ?? 1, pageSize));
                //  return View(await PaginatedList<Insurance>.CreateAsync(insurance.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            //return View(await applicationDbContext.ToListAsync());
            //var licence = await _context.Licence.ToListAsync();
            //return View(licence);
        }
        [Authorize]
        //[Authorize(Roles = "Manager")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> New(LicenceViewModel model)  
        {  
            if (ModelState.IsValid)  
            {  
                string uniqueFileName = UploadedFile(model);  
  
                Licence licence= new Licence  
                {
                    EmployeeNumber = model.EmployeeNumber,
                    Email = model.Email,
                    EmployeeName = model.EmployeeName,
                    LicenceNumber = model.LicenceNumber,
                    IssuedDate = model.IssuedDate,
                    ExpiredDate = model.ExpiredDate,
                    RenewedDate = model.RenewedDate,
                    Department = model.Department,  
                    ProfilePicture = uniqueFileName,  
                };  
                _context.Add(licence);  
                await _context.SaveChangesAsync();  
                return RedirectToAction(nameof(Index));  
            }  
            return View();  
        }  
  
        private string UploadedFile(LicenceViewModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.ProfileImage != null)  
            {  
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.ProfileImage.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }  
    }
}
