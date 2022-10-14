﻿using demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace demo.Controllers
{
    public class BookingController : Controller
    {
        private readonly SampleContext _context;

        public BookingController(SampleContext context)
        {
            _context = context;
        }
        public int SeatCheck(string a, DateTime d, int id)
        {
            List<BookingTbl> Book = _context.BookingTbl.ToList();
            string[] SeatList = a.Split(",");
            for(int b=0;b<SeatList.Length; b++)
            {
                //if ((Convert.ToInt32(SeatList[b]) > 50) && (Convert.ToInt32(SeatList[b]) <= 0))
                //{

                //}
                
                    foreach (var i in Book)
                    {
                        string[] Seatnos = i.SeatNo.Split(",");
                        for (int j = 0; j < Seatnos.Length; j++)
                        {
                            if ((i.Date == d) && (i.MovieId == id) && (Seatnos[j] == SeatList[b]))
                            {
                                return 0;
                            }
                        }
                    }
                
            }
            return 1;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            
            int id = (int)HttpContext.Session.GetInt32("UserId");
            var sampleContext = _context.BookingTbl.Include(b => b.Movie).Include(b => b.User);
           
            List<BookingTbl> cart = (from i in _context.BookingTbl where i.UserId == id select i).ToList();
            if(cart.Count== 0)
            {
                ViewBag.ErrorMessage = "Your Cart Is Empty..";
                return View(await sampleContext.ToListAsync());
            }
            return View(await sampleContext.ToListAsync());
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookingTbl == null)
            {
                return NotFound();
            }

            var bookingTbl = await _context.BookingTbl
                .Include(b => b.Movie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingTbl == null)
            {
                return NotFound();
            }

            return View(bookingTbl);
        }

        // GET: Booking/Create
        public IActionResult Create(int id)
        {
            
            //ViewData["MovieId"] = new SelectList(_context.movieTbls, "MovieId", "MovieId");
            //ViewData["UserId"] = new SelectList(_context.userTbls, "UserId", "UserId");

            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingTbl bookingTbl)
        {
           
                bookingTbl.MovieId = (int)HttpContext.Session.GetInt32("MovieId");
                bookingTbl.UserId = (int)HttpContext.Session.GetInt32("UserId");
            //var id = (from i in _context.BookingTbl
            //          where i.UserId == bookingTbl.UserId && i.MovieId == bookingTbl.MovieId && i.Date == bookingTbl.Date
            //          select i).SingleOrDefault();
            //if (id == null)
            //{
                int cost = (int)HttpContext.Session.GetInt32("Cost");
                bookingTbl.AmountTotal = bookingTbl.NoOfTickets * cost;
                bookingTbl.Date=Convert.ToDateTime(HttpContext.Session.GetString("Date"));
            //This one is for search area
            //if ((bookingTbl.Date >= DateTime.Now) && (bookingTbl.Date <= DateTime.Today.AddDays(5)))
            //{
            int i = SeatCheck(bookingTbl.SeatNo, bookingTbl.Date, bookingTbl.MovieId);
            if (i == 1)
            {
                _context.Add(bookingTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //ViewBag.ErrorMessage = bookingTbl.SeatNo;
                //HttpContext.Session.SetString("Already Booked",bookingTbl.SeatNo);
                ViewBag.ErrorMessage = "Already Booked.";
                //return RedirectToAction("create");
                return View();
            }
            //}

            //else
            //{
            //    ViewBag.ErrorMessage = "Movie is not available for the selected date...\n Please select any alternate date";
            //    return View();
            //}

            //else
            //{
            //    id.NoOfTickets += bookingTbl.NoOfTickets;
            //    id.AmountTotal = id.NoOfTickets * (from i in _context.BookingTbl
            //                                       where i.MovieId == bookingTbl.MovieId && i.Date == bookingTbl.Date
            //                                       select i.Movie.Cost).SingleOrDefault();
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));

            //}
            //ViewData["MovieId"] = new SelectList(_context.movieTbls, "MovieId", "MovieId", bookingTbl.MovieId);
            //ViewData["UserId"] = new SelectList(_context.userTbls, "UserId", "UserId", bookingTbl.UserId);
            return View(bookingTbl);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookingTbl == null)
            {
                return NotFound();
            }

            var bookingTbl = await _context.BookingTbl.FindAsync(id);
            if (bookingTbl == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.movieTbls, "MovieId", "MovieId", bookingTbl.MovieId);
            ViewData["UserId"] = new SelectList(_context.userTbls, "UserId", "UserId", bookingTbl.UserId);
            return View(bookingTbl);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,MovieId,UserId,NoOfTickets,AmountTotal")] BookingTbl bookingTbl)
        {
            if (id != bookingTbl.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingTblExists(bookingTbl.BookingId))
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
            ViewData["MovieId"] = new SelectList(_context.movieTbls, "MovieId", "MovieId", bookingTbl.MovieId);
            ViewData["UserId"] = new SelectList(_context.userTbls, "UserId", "UserId", bookingTbl.UserId);
            return View(bookingTbl);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookingTbl == null)
            {
                return NotFound();
            }

            var bookingTbl = await _context.BookingTbl
                .Include(b => b.Movie)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingTbl == null)
            {
                return NotFound();
            }

            return View(bookingTbl);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookingTbl == null)
            {
                return Problem("Entity set 'SampleContext.BookingTbl'  is null.");
            }
            var bookingTbl = await _context.BookingTbl.FindAsync(id);
            if (bookingTbl != null)
            {
                _context.BookingTbl.Remove(bookingTbl);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
public IActionResult Payment(int id) 
        { 
            var OrderMaster = _context.OrderMasterTbls.Find(id);
            return View(OrderMaster); 
        }

        [HttpPost]
        public IActionResult Payment(OrderMasterTbl m)
        {

            if (m.Paid == m.Amount)
            {

                _context.OrderMasterTbls.Update(m);
                _context.SaveChanges();
                return RedirectToAction("Thankyou");
            }
            else 
            { 
                ViewBag.ErrorMessage = "amount not valid"; 
                return View(m); 
            }

        }
        [HttpPost]
        public async Task<IActionResult> ProceedtoBuy()
        {
            var UserId = HttpContext.Session.GetInt32("UserId"); 
            List<BookingTbl> cart = (from i in _context.BookingTbl where i.UserId == UserId select i).ToList();
            List<OrderDetails> od = new List<OrderDetails>();
            OrderMasterTbl om = new OrderMasterTbl();

                om.OrderDate = DateTime.Today;
                om.UserId = (int)UserId;
                om.Amount = 0;
                foreach (var item in cart)
                {

                    om.Amount += item.AmountTotal;
                }
                _context.Add(om);

                _context.SaveChanges();
                HttpContext.Session.SetInt32("Total", (int)om.Amount);
                foreach (var item in cart)
                {
                    OrderDetails detail = new OrderDetails();
                    detail.MovieId = item.MovieId;
                    detail.NoOfTickets = item.NoOfTickets;
                    detail.Cost = item.AmountTotal;
                    detail.OrderMasterId = om.OrderMasterId;
                    od.Add(detail);
                }
                _context.AddRange(od);
                _context.SaveChanges();
                _context.BookingTbl.RemoveRange(cart);
                _context.SaveChanges();

                return RedirectToAction("Payment", new { id = om.OrderMasterId });
            

        }
        public IActionResult Thankyou()
        {
            return View();
        }

        private bool BookingTblExists(int id)
        {
          return _context.BookingTbl.Any(e => e.BookingId == id);
        }
    }
    
}