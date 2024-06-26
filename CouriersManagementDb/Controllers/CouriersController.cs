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
    public class CouriersController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public CouriersController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Couriers
        // This method allows sorting and filtering of couriers in the index view.
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            // ViewData variables to maintain the state of sort orders and search filters in the view
            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "last_asc" ? "last_desc" : "last_asc";
            ViewData["EmailSortParm"] = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            // Query all couriers from the context
            var couriers = from c in _context.Couriers
                           select c;

            // Filter couriers when searchString is not empty
            if (!String.IsNullOrEmpty(searchString))
            {
                couriers = couriers.Where(s => s.FirstName.Contains(searchString)
                                       || s.LastName.Contains(searchString)
                                       || s.Email.Contains(searchString));
            }

            // Apply sorting based on sortOrder parameter
            switch (sortOrder)
            {
                case "name_desc":
                    couriers = couriers.OrderByDescending(s => s.FirstName);
                    break;
                case "last_asc":
                    couriers = couriers.OrderBy(s => s.LastName);
                    break;
                case "last_desc":
                    couriers = couriers.OrderByDescending(s => s.LastName);
                    break;
                case "email_asc":
                    couriers = couriers.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    couriers = couriers.OrderByDescending(s => s.Email);
                    break;
                default:
                    couriers = couriers.OrderBy(s => s.FirstName);
                    break;
            }
            int pageSize = 3; 
            // Return the view with the sorted and/or filtered list of couriers
            return View(await PaginatedList<Courier>.CreateAsync(couriers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Couriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .FirstOrDefaultAsync(m => m.CourierID == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // GET: Couriers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Couriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourierID,FirstName,LastName,Email,PhoneNumber,BaseLocation")] Courier courier)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(courier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courier);
        }

        // GET: Couriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null)
            {
                return NotFound();
            }
            return View(courier);
        }

        // POST: Couriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourierID,FirstName,LastName,Email,PhoneNumber,BaseLocation")] Courier courier)
        {
            if (id != courier.CourierID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(courier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourierExists(courier.CourierID))
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
            return View(courier);
        }

        // GET: Couriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .FirstOrDefaultAsync(m => m.CourierID == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: Couriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier != null)
            {
                _context.Couriers.Remove(courier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourierExists(int id)
        {
            return _context.Couriers.Any(e => e.CourierID == id);
        }
    }
}
