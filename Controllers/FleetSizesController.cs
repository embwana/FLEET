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
    public class FleetSizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FleetSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FleetSizes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FleetSize.Include(f => f.DepartmentName).Include(f => f.FleetCategory).Include(f => f.StationName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FleetSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetSize = await _context.FleetSize
                .Include(f => f.DepartmentName)
                .Include(f => f.FleetCategory)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetSizeId == id);
            if (fleetSize == null)
            {
                return NotFound();
            }

            return View(fleetSize);
        }

        // GET: FleetSizes/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment");
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location");
            return View();
        }

        // POST: FleetSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FleetSizeId,FleetCategoryId,StationId,DepartmentId")] FleetSize fleetSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fleetSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetSize.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetSize.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetSize.StationId);
            return View(fleetSize);
        }

        // GET: FleetSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetSize = await _context.FleetSize.FindAsync(id);
            if (fleetSize == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetSize.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetSize.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetSize.StationId);
            return View(fleetSize);
        }

        // POST: FleetSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FleetSizeId,FleetCategoryId,StationId,DepartmentId")] FleetSize fleetSize)
        {
            if (id != fleetSize.FleetSizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fleetSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleetSizeExists(fleetSize.FleetSizeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Comment", fleetSize.DepartmentId);
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "Mileage", fleetSize.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "Location", fleetSize.StationId);
            return View(fleetSize);
        }

        // GET: FleetSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fleetSize = await _context.FleetSize
                .Include(f => f.DepartmentName)
                .Include(f => f.FleetCategory)
                .Include(f => f.StationName)
                .FirstOrDefaultAsync(m => m.FleetSizeId == id);
            if (fleetSize == null)
            {
                return NotFound();
            }

            return View(fleetSize);
        }

        // POST: FleetSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fleetSize = await _context.FleetSize.FindAsync(id);
            _context.FleetSize.Remove(fleetSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleetSizeExists(int id)
        {
            return _context.FleetSize.Any(e => e.FleetSizeId == id);
        }
    }
}
