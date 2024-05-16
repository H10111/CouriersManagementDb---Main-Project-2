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
    public class PackagesController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public PackagesController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Package> packages = _context.Packages
                .Include(p => p.Shipments)
                .Include(p => p.Pallet);

            if (!string.IsNullOrEmpty(searchString))
            {
                packages = packages.Where(p => p.Dimensions.Contains(searchString)
                                            || p.Contents.Contains(searchString)
                                            || p.DeliveryStatus.Contains(searchString));
            }

            return View(await packages.ToListAsync());
        }
        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .Include(p => p.Pallet)
                .Include(p => p.Shipments)
                .FirstOrDefaultAsync(m => m.PackageID == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            ViewData["PalletID"] = new SelectList(_context.Pallets, "PalletID", "PalletID");
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "DeliveryStatus");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageID,Dimensions,Contents,DeliveryStatus,ShipmentID,PalletID")] Package package)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PalletID"] = new SelectList(_context.Pallets, "PalletID", "PalletID", package.PalletID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "DeliveryStatus", package.ShipmentID);
            return View(package);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            ViewData["PalletID"] = new SelectList(_context.Pallets, "PalletID", "PalletID", package.PalletID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "DeliveryStatus", package.ShipmentID);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageID,Dimensions,Contents,DeliveryStatus,ShipmentID,PalletID")] Package package)
        {
            if (id != package.PackageID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageID))
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
            ViewData["PalletID"] = new SelectList(_context.Pallets, "PalletID", "PalletID", package.PalletID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "DeliveryStatus", package.ShipmentID);
            return View(package);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .Include(p => p.Pallet)
                .Include(p => p.Shipments)
                .FirstOrDefaultAsync(m => m.PackageID == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = await _context.Packages.FindAsync(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.PackageID == id);
        }
    }
}
