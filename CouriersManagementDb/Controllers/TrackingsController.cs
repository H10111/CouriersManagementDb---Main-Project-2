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
    public class TrackingsController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public TrackingsController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Trackings
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["LocationSortParam"] = String.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "Date" ? "date_desc" : "Date";

            var trackings = from t in _context.Trackings
                            .Include(t => t.Location)
                            .Include(t => t.Package)
                            select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                trackings = trackings.Where(t => t.Package.Contents.Contains(searchString)
                                                 || t.Location.AddressLine1.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "location_desc":
                    trackings = trackings.OrderByDescending(t => t.Location.AddressLine1);
                    break;
                case "Date":
                    trackings = trackings.OrderBy(t => t.Timestamp);
                    break;
                case "date_desc":
                    trackings = trackings.OrderByDescending(t => t.Timestamp);
                    break;
                default:
                    trackings = trackings.OrderBy(t => t.Location.AddressLine1);
                    break;
            }

            return View(await trackings.ToListAsync());
        }


        // GET: Trackings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings
                .Include(t => t.Location)
                .Include(t => t.Package)
                .FirstOrDefaultAsync(m => m.TrackingID == id);
            if (tracking == null)
            {
                return NotFound();
            }

            return View(tracking);
        }

        // GET: Trackings/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "AddressLine1");
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents");
            return View();
        }

        // POST: Trackings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrackingID,DeliveryStatus,Timestamp,PackageID,LocationID")] Tracking tracking)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(tracking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "AddressLine1", tracking.LocationID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", tracking.PackageID);
            return View(tracking);
        }

        // GET: Trackings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings.FindAsync(id);
            if (tracking == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "AddressLine1", tracking.LocationID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", tracking.PackageID);
            return View(tracking);
        }

        // POST: Trackings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrackingID,DeliveryStatus,Timestamp,PackageID,LocationID")] Tracking tracking)
        {
            if (id != tracking.TrackingID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackingExists(tracking.TrackingID))
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
            ViewData["LocationID"] = new SelectList(_context.Locations, "LocationID", "AddressLine1", tracking.LocationID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", tracking.PackageID);
            return View(tracking);
        }

        // GET: Trackings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tracking = await _context.Trackings
                .Include(t => t.Location)
                .Include(t => t.Package)
                .FirstOrDefaultAsync(m => m.TrackingID == id);
            if (tracking == null)
            {
                return NotFound();
            }

            return View(tracking);
        }

        // POST: Trackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tracking = await _context.Trackings.FindAsync(id);
            if (tracking != null)
            {
                _context.Trackings.Remove(tracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackingExists(int id)
        {
            return _context.Trackings.Any(e => e.TrackingID == id);
        }
    }
}
