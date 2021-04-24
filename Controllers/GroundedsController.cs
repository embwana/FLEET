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
    public class GroundedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroundedsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Licence.ToListAsync();
            var groundeds = await _context.Grounded.ToListAsync();
            var grounded = from s in _context.Grounded
                          
                            .Include(s => s.NumberPlate)
                             .Include(s => s.Department)
                           .Include(s=>s.Station)
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                //licence = licence.Where(s => s.LicenceNumber.Contains(searchString));
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
                return View(await PaginatedList<Grounded>.CreateAsync(grounded.AsNoTracking(), pageNumber ?? 1, pageSize));
                //  return View(await PaginatedList<Insurance>.CreateAsync(insurance.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

            // GET: Groundeds
            //public async Task<IActionResult> Index()
            //{
            //    var applicationDbContext = _context.Grounded.Include(g => g.Department).Include(g => g.NumberPlate).Include(g => g.Station);
            //    return View(await applicationDbContext.ToListAsync());
        }

        // GET: Groundeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded
               
                .Include(g => g.NumberPlate)
                 .Include(g => g.Department)
                .Include(g => g.Station)
                .FirstOrDefaultAsync(m => m.GroundedId == id);
            if (grounded == null)
            {
                return NotFound();
            }

            return View(grounded);
        }

        // GET: Groundeds/Create
        public IActionResult Create()
        {
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName");
            return View();
        }

        // POST: Groundeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroundedId,FleetCategoryId,DepartmentId,StationId,Remarks")] Grounded grounded)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grounded);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", grounded.DepartmentId);
            //ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // GET: Groundeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded.FindAsync(id);
            if (grounded == null)
            {
                return NotFound();
            }
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", grounded.DepartmentId);
            //ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // POST: Groundeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroundedId,FleetCategoryId,DepartmentId,StationId,Remarks")] Grounded grounded)
        {
            if (id != grounded.GroundedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grounded);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroundedExists(grounded.GroundedId))
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
            ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", grounded.DepartmentId);
            //ViewData["FleetCategoryId"] = new SelectList(_context.FleetCategory, "FleetCategoryId", "NumberPlate", grounded.FleetCategoryId);
            ViewData["StationId"] = new SelectList(_context.Set<Station>(), "StationId", "StationName", grounded.StationId);
            return View(grounded);
        }

        // GET: Groundeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grounded = await _context.Grounded
                 .Include(g => g.NumberPlate)
                .Include(g => g.Department)
                .Include(g => g.Station)
                .FirstOrDefaultAsync(m => m.GroundedId == id);
            if (grounded == null)
            {
                return NotFound();
            }

            return View(grounded);
        }

        // POST: Groundeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grounded = await _context.Grounded.FindAsync(id);
            _context.Grounded.Remove(grounded);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroundedExists(int id)
        {
            return _context.Grounded.Any(e => e.GroundedId == id);
        }
    }
}
