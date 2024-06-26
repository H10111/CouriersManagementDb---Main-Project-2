﻿using System;
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
    public class ShipmentsController : Controller
    {
        private readonly CouriersManagementDbContext _context;

        public ShipmentsController(CouriersManagementDbContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["DateSortParam"] = sortOrder == "DateAsc" ? "date_desc" : "DateAsc";
            ViewData["StatusSortParam"] = sortOrder == "StatusDesc" ? "StatusAsc" : "StatusDesc";

            var shipments = from s in _context.Shipments
                            .Include(s => s.Courier)
                            .Include(s => s.Customer)
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                shipments = shipments.Where(s => s.Courier.BaseLocation.Contains(searchString)
                                                 || s.Customer.Address.Contains(searchString)
                                                 || s.DeliveryStatus.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "DateAsc":
                    shipments = shipments.OrderBy(s => s.ArrivalDate);
                    break;
                case "date_desc":
                    shipments = shipments.OrderByDescending(s => s.ArrivalDate);
                    break;
                case "StatusAsc":
                    shipments = shipments.OrderBy(s => s.DeliveryStatus);
                    break;
                case "StatusDesc":
                    shipments = shipments.OrderByDescending(s => s.DeliveryStatus);
                    break;
                default:
                    shipments = shipments.OrderBy(s => s.ShipmentID);
                    break;
            }

            return View(await shipments.ToListAsync());
        }


        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Courier)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.ShipmentID == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "BaseLocation");
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentID,DeliveryStatus,ArrivalDate,DispatchDate,CourierID,CustomerID")] Shipment shipment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "BaseLocation", shipment.CourierID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", shipment.CustomerID);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "BaseLocation", shipment.CourierID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", shipment.CustomerID);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipmentID,DeliveryStatus,ArrivalDate,DispatchDate,CourierID,CustomerID")] Shipment shipment)
        {
            if (id != shipment.ShipmentID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.ShipmentID))
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
            ViewData["CourierID"] = new SelectList(_context.Couriers, "CourierID", "BaseLocation", shipment.CourierID);
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "Address", shipment.CustomerID);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Courier)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.ShipmentID == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.ShipmentID == id);
        }
    }
}
