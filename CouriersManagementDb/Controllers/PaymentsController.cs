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
    public class PaymentsController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public PaymentsController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["AmountSortParam"] = sortOrder == "AmountAsc" ? "AmountDesc" : "AmountAsc";  // Toggles the sort order

            var paymentsQuery = _context.Payments
                .Include(p => p.Customers)
                .Include(p => p.Employee)
                .Include(p => p.Packages)
                .Include(p => p.Shipments)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                paymentsQuery = paymentsQuery.Where(p =>
                    p.Customers.FirstName.Contains(searchString) ||
                    p.Customers.LastName.Contains(searchString) ||
                    p.Employee.FirstName.Contains(searchString) ||
                    p.Employee.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "AmountAsc":
                    paymentsQuery = paymentsQuery.OrderBy(p => p.Amount);
                    break;
                case "AmountDesc":
                    paymentsQuery = paymentsQuery.OrderByDescending(p => p.Amount);
                    break;
                default:
                    paymentsQuery = paymentsQuery.OrderBy(p => p.PaymentID); // Default sorting by PaymentID
                    break;
            }

            return View(await paymentsQuery.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Customers)
                .Include(p => p.Employee)
                .Include(p => p.Packages)
                .Include(p => p.Shipments)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address");
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Email");
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents");
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "ShipmentID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,PaymentDate,Amount,ShipmentID,PackageID,CustomerID,EmployeeID")] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", payment.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Email", payment.EmployeeID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", payment.PackageID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "ShipmentID", payment.ShipmentID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", payment.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Email", payment.EmployeeID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", payment.PackageID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "ShipmentID", payment.ShipmentID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,PaymentDate,Amount,ShipmentID,PackageID,CustomerID,EmployeeID")] Payment payment)
        {
            if (id != payment.PaymentID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", payment.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "Email", payment.EmployeeID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", payment.PackageID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "ShipmentID", payment.ShipmentID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Customers)
                .Include(p => p.Employee)
                .Include(p => p.Packages)
                .Include(p => p.Shipments)
                .FirstOrDefaultAsync(m => m.PaymentID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentID == id);
        }
    }
}
