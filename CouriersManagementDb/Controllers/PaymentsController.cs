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
        private string? amountRange;

        public PaymentsController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index(string searchString, decimal? minAmount, decimal? maxAmount, DateTime? startDate, DateTime? endDate)
        {
            var paymentsQuery = _context.Payments
                .Include(p => p.Customers)
                .Include(p => p.Packages)
                .Include(p => p.Shipments)
                .Include(p => p.Employee)
                .AsQueryable();

            // Filtering by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                paymentsQuery = paymentsQuery.Where(p =>
                    p.Customers.CustomerName.Contains(searchString) ||
                    p.Packages.Contents.Contains(searchString) ||
                    p.Shipments.DeliveryStatus.ToString().Contains(searchString));
            }

            // Filtering by amount range
            if (minAmount.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Amount >= minAmount.Value);
            }
            if (maxAmount.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Amount <= maxAmount.Value);
            }

            // Filtering by date range
            if (startDate.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Timestamp >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                paymentsQuery = paymentsQuery.Where(p => p.Timestamp <= endDate.Value);
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
            PopulateDropDowns();
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentID,Timestamp,Amount,ShipmentID,PackageID,CustomerID,EmployeeID")] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDowns(payment);
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
            PopulateDropDowns(payment);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,Timestamp,Amount,ShipmentID,PackageID,CustomerID,EmployeeID")] Payment payment)
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
            PopulateDropDowns(payment);
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
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentID == id);
        }

        private void PopulateDropDowns(Payment payment = null)
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", payment?.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "EmployeeName", payment?.EmployeeID);
            ViewData["PackageID"] = new SelectList(_context.Packages, "PackageID", "Contents", payment?.PackageID);
            ViewData["ShipmentID"] = new SelectList(_context.Shipments, "ShipmentID", "DeliveryStatus", payment?.ShipmentID);
        }
    }
}
