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
    public class MovieController : Controller
    {
        private readonly SampleContext _context;

        public MovieController(SampleContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
              return View(await _context.MovieTbls.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieTbls == null)
            {
                return NotFound();
            }

            var movieTbl = await _context.MovieTbls
                .FirstOrDefaultAsync(m => m.MovieId == id);
            ////HttpContext.Session.SetString("Moviename",movieTbl.MovieName);
            //HttpContext.Session.SetInt32("MovieId", movieTbl.MovieId);
            //HttpContext.Session.SetInt32("Cost", movieTbl.Cost);
            if (movieTbl == null)
            {
                return NotFound();
            }

            //return RedirectToAction("Create", "Booking");

            return View(movieTbl);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> BookNow(int? id)
        {
            if (id == null || _context.MovieTbls == null)
            {
                return NotFound();
            }

            var movieTbl = await _context.MovieTbls
                .FirstOrDefaultAsync(m => m.MovieId == id);
            //HttpContext.Session.SetString("Moviename",movieTbl.MovieName);
            HttpContext.Session.SetInt32("MovieId", movieTbl.MovieId);
            HttpContext.Session.SetString("MovieName", movieTbl.MovieName);
            HttpContext.Session.SetInt32("Cost",movieTbl.Cost);
            HttpContext.Session.SetString("Date", movieTbl.Date.ToString());
            HttpContext.Session.SetInt32("Capacity", movieTbl.capacity);
            if (movieTbl.capacity <= 0)
            {
                ViewBag.ErrMessage = "HouseFull";
                return RedirectToAction("DateSearch");
            }
            if (movieTbl == null)
            {
                return NotFound();
            }
 
            return RedirectToAction("Create", "Booking");

            //return View(movieTbl);
        }

        // GET: Movie/Createc
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MovieTbl movieTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieTbl);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieTbls == null)
            {
                return NotFound();
            }

            var movieTbl = await _context.MovieTbls.FindAsync(id);
            if (movieTbl == null)
            {
                return NotFound();
            }
            return View(movieTbl);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,MovieName,Image,Date,Slot,Cost,capacity")] MovieTbl movieTbl)
        {
            if (id != movieTbl.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTblExists(movieTbl.MovieId))
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
            return View(movieTbl);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieTbls == null)
            {
                return NotFound();
            }

            var movieTbl = await _context.MovieTbls
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieTbl == null)
            {
                return NotFound();
            }

            return View(movieTbl);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieTbls == null)
            {
                return Problem("Entity set 'SampleContext.movieTbls'  is null.");
            }
            var movieTbl = await _context.MovieTbls.FindAsync(id);
            if (movieTbl != null)
            {
                _context.MovieTbls.Remove(movieTbl);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTblExists(int id)
        {
          return _context.MovieTbls.Any(e => e.MovieId == id);
        }
        public async Task<IActionResult> DateSearch(string Search, int? PageNumber, string CurrentFilter)
        {
            //for sorting
            //ViewData["CurrentSort"] = SortOrder;
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            ViewBag.name = HttpContext.Session.GetString("name");



            if (ViewBag.UserName != null)
            {



                if (Search != null)
                {
                    PageNumber = 1;
                }
                else
                {
                    Search = CurrentFilter;
                }




                ViewData["CurrentFilter"] = Search;



                var moviename = from s in _context.MovieTbls
                               select s;
                if (!string.IsNullOrEmpty(Search))
                {
                    moviename = moviename.Where(s => s.Date ==Convert.ToDateTime( Search));
                }



                //else if (!string.IsNullOrEmpty(Search))
                //{
                //    students = students.Where(s => s.Brand == Search);
                //}
                //else
                //{



                //}
                //switch (SortOrder)
                //{
                //    case "name_desc":
                //        students = students.OrderByDescending(s => s.MovieName);
                //        break;
                //    default:
                //        students = students.OrderBy(s => s.MovieName);
                //        break;
                //}
                int pageSize = 3;
                return View(await PaginatedList<MovieTbl>.CreateAsync(moviename.AsNoTracking(), PageNumber ?? 1, pageSize));
            }
            if (ViewBag.name != null)
            {



                if (Search != null)
                {
                    PageNumber = 1;
                }
                else
                {
                    Search = CurrentFilter;
                }




                ViewData["CurrentFilter"] = Search;



                var students = from s in _context.MovieTbls
                               select s;
                if (!string.IsNullOrEmpty(Search))
                {
                    students = students.Where(s => s.Date == Convert.ToDateTime(Search));
                }



                //else if (!string.IsNullOrEmpty(Search))
                //{
                //    students = students.Where(s => s.Brand == Search);
                //}
                //else
                //{



                //}
                //switch (SortOrder)
                //{
                //    case "name_desc":
                //        students = students.OrderByDescending(s => s.MovieName);
                //        break;
                //    default:
                //        students = students.OrderBy(s => s.MovieName);
                //        break;
                //}
                int pageSize = 3;
                return View(await PaginatedList<MovieTbl>.CreateAsync(students.AsNoTracking(), PageNumber ?? 1, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
