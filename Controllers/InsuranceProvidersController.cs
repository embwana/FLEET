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
    [Authorize(Roles = "Admin")]
    public class InsuranceProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Manager")]
        // GET: InsuranceProviders
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsuranceProvider.ToListAsync());
        }

        // GET: InsuranceProviders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider
                .FirstOrDefaultAsync(m => m.InsuranceProviderId == id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }

            return View(insuranceProvider);
        }

        // GET: InsuranceProviders/Create
        public IActionResult Create()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        // POST: InsuranceProviders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceProviderId,ProviderName,ServiceOffered,EstablishedYear")] InsuranceProvider insuranceProvider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceProvider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceProvider);
        }

        // GET: InsuranceProviders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider.FindAsync(id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }
            return View(insuranceProvider);
        }

        // POST: InsuranceProviders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceProviderId,ProviderName,ServiceOffered,EstablishedYear")] InsuranceProvider insuranceProvider)
        {
            if (id != insuranceProvider.InsuranceProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceProvider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceProviderExists(insuranceProvider.InsuranceProviderId))
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
            return View(insuranceProvider);
        }
        [Authorize(Roles = "Admin")]
        // GET: InsuranceProviders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceProvider = await _context.InsuranceProvider
                .FirstOrDefaultAsync(m => m.InsuranceProviderId == id);
            if (insuranceProvider == null)
            {
                return NotFound();
            }

            return View(insuranceProvider);
        }

        // POST: InsuranceProviders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceProvider = await _context.InsuranceProvider.FindAsync(id);
            _context.InsuranceProvider.Remove(insuranceProvider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceProviderExists(int id)
        {
            return _context.InsuranceProvider.Any(e => e.InsuranceProviderId == id);
        }
    }
}
