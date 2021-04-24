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
    public class COFsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public COFsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
          //[Authorize]
        // GET: COFs
        ////public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(int? pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            //return View(await _context.COF.ToListAsync());
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            //var applicationDbContext = _context.Licence.ToListAsync();
            var cofs = await _context.COF.ToListAsync();
            var cof = from s in _context.COF
                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                cof = cof.Where(s => s.COFNumber.Contains(searchString));
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
                return View(await PaginatedList<COF>.CreateAsync(cof.AsNoTracking(), pageNumber ?? 1, pageSize));


            }
        }
            // GET: COFs/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cOF = await _context.COF
                .FirstOrDefaultAsync(m => m.COFId == id);
            if (cOF == null)
            {
                return NotFound();
            }

            return View(cOF);
        }
        //[Authorize(Roles = "Admin")]
        // GET: COFs/Create
        public IActionResult Create()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        // POST: COFs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("COFId,COFNumber,Issued,Expired,Remarks")] COF cOF)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cOF);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cOF);
        }

        // GET: COFs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cOF = await _context.COF.FindAsync(id);
            if (cOF == null)
            {
                return NotFound();
            }
            return View(cOF);
        }

        // POST: COFs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("COFId,COFNumber,Issued,Expired,Remarks")] COF cOF)
        {
            if (id != cOF.COFId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cOF);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!COFExists(cOF.COFId))
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
            return View(cOF);
        }
        [Authorize(Roles = "Admin")]

        // GET: COFs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cOF = await _context.COF
                .FirstOrDefaultAsync(m => m.COFId == id);
            if (cOF == null)
            {
                return NotFound();
            }

            return View(cOF);
        }
        //[Authorize(Roles = "Admin")]
        // POST: COFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cOF = await _context.COF.FindAsync(id);
            _context.COF.Remove(cOF);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool COFExists(int id)
        {
            return _context.COF.Any(e => e.COFId == id);
        }
    }
}
