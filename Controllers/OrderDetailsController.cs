using System;
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
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.OrderDetails == null)
                {
                    return NotFound();
                }

                var OrderTbl = await _context.OrderDetails
                    .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            HttpContext.Session.SetInt32("MId", OrderTbl.MovieId);

            if (OrderTbl == null)
                {
                    return NotFound();
                }
                    return View(OrderTbl);
            }
        public async Task<IActionResult> Cancel(int? id)
        {
            var orderdetail = await _context.OrderDetails.FindAsync(id);
            int no = orderdetail.NoOfTickets;
            int mid = (int)HttpContext.Session.GetInt32("MId");
            var s = (from i in _context.MovieTbls
                     where i.MovieId == mid
                     select i).SingleOrDefault();
            s.capacity += no;
            _context.OrderDetails.Remove(orderdetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
