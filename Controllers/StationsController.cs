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
    public class StationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        //[Authorize(Roles = "Manager")]
        // GET: Stations
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Station.ToListAsync();
            var stations  = await _context.Station.ToListAsync();
            var station = from s in _context.Station
                          .Include(c => c.DepartmentName)
                          .AsNoTracking()
                            select s;
            //List<Department> cl = new List<Department>();
            //cl = (from c in _context.Department select c).ToList();
            //cl.Insert(0, new Department { DepartmentId = 0, DepartmentName = "--Select Department Name--" });
            if (!String.IsNullOrEmpty(searchString))
            {
                station = station.Where(s => s.StationName.Contains(searchString));
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
                int pageSize = 5;

                //ViewBag.message = cl;
                //return View(await PaginatedList<Insurance>.CreateAsync(Insurance.AsNoTracking(), pageNumber ?? 1, pageSize));
                return View(await  PaginatedList<Station>.CreateAsync(station.AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            //public async Task<IActionResult> Index()
            //{
            //    var applicationDbContext = _context.Station.Include(s => s.DepartmentName);
            //    return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.DepartmentName)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Stations/Create
        public IActionResult Create()
        {
            
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,StationName,Location,DepartmentId,FleetNumber")] Station station)
        {
            if (ModelState.IsValid)
            {
                _context.Add(station);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", station.DepartmentId);
            return View(station);
        }

        // GET: Stations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", station.DepartmentId);
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,StationName,Location,DepartmentId,FleetNumber")] Station station)
        {
            if (id != station.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.StationId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName", station.DepartmentId);
            return View(station);
        }

        // GET: Stations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Station
                .Include(s => s.DepartmentName)
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var station = await _context.Station.FindAsync(id);
            _context.Station.Remove(station);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
            return _context.Station.Any(e => e.StationId == id);
        }
    }
}
