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
    [Authorize(Roles = "Admin,Manager,User")]
    public class FleetCustodiansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FleetCustodiansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FleetCustodians
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            //var applicationDbContext = _context.FleetCustodian.Include(f => f.DepartmentName).Include(f => f.LicenceNumber).Include(f => f.NumberPlate).Include(f => f.StationName);
            var fleetcustodian = from s in _context.FleetCustodian
                               //.Include( s =>s.DepartmentName)
                               .Include(s => s.DepartmentName)
                               .Include(s => s.LicenceNumber)
                               .Include(s => s.NumberPlate)
                               .Include(s => s.StationName)

                                select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    fleetcustodian = fleetcustodian.Where(s => s.NumberPlate.Contains(searchString));
            //    // || s.FirstMidName.Contains(searchString));
            //}

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
                return View(await PaginatedList<FleetCustodian>.CreateAsync(fleetcustodian.AsNoTracking(), pageNumber ?? 1, pageSize));


            }

            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: FleetCustodians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian
                .Include(f => f.DepartmentName)
                .Include(f => f.LicenceNumber)
                .Include(f => f.NumberPlate)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetCustodianId == id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }

            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment");
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "LicenceNumber");
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location");
            return View();
        }

        // POST: FleetCustodians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetCustodianId,LicenceId,FleetCategoryId,StationId,DepartmentId,CollectedOn,ReturnedOn")] FleetCustodian fleetCustodian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fleetCustodian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian.FindAsync(id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // POST: FleetCustodians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FleetCustodianId,LicenceId,FleetCategoryId,StationId,DepartmentId,CollectedOn,ReturnedOn")] FleetCustodian fleetCustodian)
        {
            if (id != fleetCustodian.FleetCustodianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetCustodian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetCustodianExists(fleetCustodian.FleetCustodianId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetCustodian.DepartmentId);
            ViewData["LicenceId"] = new SelectList(_context.Licences, "LicenceId", "Department", fleetCustodian.LicenceId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetCustodian.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetCustodian.StationId);
            return View(fleetCustodian);
        }

        // GET: FleetCustodians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetCustodian = await _context.FleetCustodian
                .Include(f => f.DepartmentName)
                .Include(f => f.LicenceNumber)
                .Include(f => f.NumberPlate)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetCustodianId == id);
            if (fleetCustodian == null)
            {
                return NotFound();
            }

            return View(fleetCustodian);
        }

        // POST: FleetCustodians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fleetCustodian = await _context.FleetCustodian.FindAsync(id);
            _context.FleetCustodian.Remove(fleetCustodian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetCustodianExists(int id)
        {
            return _context.FleetCustodian.Any(e => e.FleetCustodianId == id);
        }
    }
}
