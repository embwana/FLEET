using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FLEET.Data;
using FLEET.Models;
using Microsoft.AspNetCore.Authorization;

namespace FLEET.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class FleetCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        public FleetCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FleetCategories
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.FleetCategory.Include(f => f.COFNumber).Include(f => f.Department).Include(f => f.InsuranceNumber).Include(f => f.Station);
        //    return View(await applicationDbContext.ToListAsync());

        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            //return View(await _context.COF.ToListAsync());
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Licence.ToListAsync();
            var fleetcategories = await _context.FleetCategory.ToListAsync();
            var fleetcategory = from s in _context.FleetCategory
                                //.Include( s =>s.DepartmentName)
                                .Include(s=>s.COFNumber)
                                .Include(s=>s.Department)
                                .Include(s => s.InsuranceNumber)
                                .Include(s => s.Station)

                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                fleetcategory = fleetcategory.Where(s => s.NumberPlate.Contains(searchString));
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
                return View(await PaginatedList<FleetCategory>.CreateAsync(fleetcategory.AsNoTracking(), pageNumber ?? 1, pageSize));


            }
        }

        // GET: FleetCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory
                .Include(f => f.COFNumber)
                .Include(f => f.Department)
                .Include(f => f.InsuranceNumber)
                .Include(f => f.Station)
                .FirstOrDefaultAsync(m => m.FleetCategoryId == id);
            if (fleetCategory == null)
            {
                return NotFound();
            }

            return View(fleetCategory);
        }

        // GET: FleetCategories/Create
        public IActionResult Create()
        {
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "InsuranceNumber");
            ViewData["StationId"] = new SelectList(_context.Station, "StationId", "StationName");
            return View();
        }

        // POST: FleetCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCategoryId,NumberPlate,Model,Year,Cost,TagNumber,Mileage,COFId,DepartmentId,InsuranceId,StationId,COMUL")] FleetCategory fleetCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fleetCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCategory.DepartmentId);
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "Details", fleetCategory.InsuranceId);
            ViewData["StationId"] = new SelectList(_context.Station, "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // GET: FleetCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory.FindAsync(id);
            if (fleetCategory == null)
            {
                return NotFound();
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCategory.DepartmentId);
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "Details", fleetCategory.InsuranceId);
            ViewData["StationId"] = new SelectList(_context.Station, "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // POST: FleetCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FleetCategoryId,NumberPlate,Model,Year,Cost,TagNumber,Mileage,COFId,DepartmentId,InsuranceId,StationId,COMUL")] FleetCategory fleetCategory)
        {
            if (id != fleetCategory.FleetCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCategoryExists(fleetCategory.FleetCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["COFId"] = new SelectList(_context.COF, "COFId", "COFNumber", fleetCategory.COFId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCategory.DepartmentId);
            ViewData["InsuranceId"] = new SelectList(_context.Insurance, "InsuranceId", "Details", fleetCategory.InsuranceId);
            ViewData["StationId"] = new SelectList(_context.Station, "StationId", "StationName", fleetCategory.StationId);
            return View(fleetCategory);
        }

        // GET: FleetCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCategory = await _context.FleetCategory
                .Include(f => f.COFNumber)
                .Include(f => f.Department)
                .Include(f => f.InsuranceNumber)
                .Include(f => f.Station)
                .FirstOrDefaultAsync(m => m.FleetCategoryId == id);
            if (fleetCategory == null)
            {
                return NotFound();
            }

            return View(fleetCategory);
        }

        // POST: FleetCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fleetCategory = await _context.FleetCategory.FindAsync(id);
            _context.FleetCategory.Remove(fleetCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCategoryExists(int id)
        {
            return _context.FleetCategory.Any(e => e.FleetCategoryId == id);
        }
    }
}
