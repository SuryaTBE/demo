﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using demo.Models;

namespace demo.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly SampleContext _context;

        public OrderDetailsController(SampleContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.OrderDetails.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.OrderDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderDetails = await _context.OrderDetails
        //        .FirstOrDefaultAsync(m => m.OrderDetailId == id);
        //    if (orderDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orderDetails);
        //}

        //// GET: OrderDetails/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: OrderDetails/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderDetailId,MovieId,OrderMasterId,NoOfTickets,Cost")] OrderDetails orderDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(orderDetails);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderDetails);
        //}

        //// GET: OrderDetails/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.OrderDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderDetails = await _context.OrderDetails.FindAsync(id);
        //    if (orderDetails == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(orderDetails);
        //}

        //// POST: OrderDetails/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,MovieId,OrderMasterId,NoOfTickets,Cost")] OrderDetails orderDetails)
        //{
        //    if (id != orderDetails.OrderDetailId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(orderDetails);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderDetailsExists(orderDetails.OrderDetailId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderDetails);
        //}

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'SampleContext.OrderDetails'  is null.");
            }
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsExists(int id)
        {
          return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        }
    }
}