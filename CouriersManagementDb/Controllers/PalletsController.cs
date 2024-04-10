using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CouriersManagementDb.Areas.Identity.Data;
using CouriersManagementDb.Models;

namespace CouriersManagementDb.Controllers
{
    public class PalletsController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public PalletsController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Pallets
        public async Task<IActionResult> Index()
        {
            var couriersManagementDbContext = _context.Pallets.Include(p => p.Courier);
            return View(await couriersManagementDbContext.ToListAsync());
        }

        // GET: Pallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .Include(p => p.Courier)
                .FirstOrDefaultAsync(m => m.PalletID == id);
            if (pallet == null)
            {
                return NotFound();
            }

            return View(pallet);
        }

        // GET: Pallets/Create
        public IActionResult Create()
        {
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "Name");
            return View();
        }

        // POST: Pallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PalletID,CourierID")] Pallet pallet)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(pallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "Name", pallet.CourierID);
            return View(pallet);
        }

        // GET: Pallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets.FindAsync(id);
            if (pallet == null)
            {
                return NotFound();
            }
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "Name", pallet.CourierID);
            return View(pallet);
        }

        // POST: Pallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PalletID,CourierID")] Pallet pallet)
        {
            if (id != pallet.PalletID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(pallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalletExists(pallet.PalletID))
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
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "Name", pallet.CourierID);
            return View(pallet);
        }

        // GET: Pallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .Include(p => p.Courier)
                .FirstOrDefaultAsync(m => m.PalletID == id);
            if (pallet == null)
            {
                return NotFound();
            }

            return View(pallet);
        }

        // POST: Pallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pallet = await _context.Pallets.FindAsync(id);
            if (pallet != null)
            {
                _context.Pallets.Remove(pallet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalletExists(int id)
        {
            return _context.Pallets.Any(e => e.PalletID == id);
        }
    }
}
