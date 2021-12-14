using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness.Models;

namespace Fitness.Controllers
{
    public class FitnessUserLoginsController : Controller
    {
        private readonly ModelContext _context;

        [TempData]
        public decimal UserID { get; set; }

        public FitnessUserLoginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: FitnessUserLogins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.FitnessUserLogins.Include(f => f.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: FitnessUserLogins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserLogin = await _context.FitnessUserLogins
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (fitnessUserLogin == null)
            {
                return NotFound();
            }

            return View(fitnessUserLogin);
        }

        // GET: FitnessUserLogins/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid");
            return View();
        }

        // POST: FitnessUserLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userloginid,Username,Password,Userid")] FitnessUserLogin fitnessUserLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fitnessUserLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserLogin.Userid);
            return View(fitnessUserLogin);
        }

        // GET: FitnessUserLogins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserLogin = await _context.FitnessUserLogins.FindAsync(id);
            if (fitnessUserLogin == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserLogin.Userid);
            return View(fitnessUserLogin);
        }

        // POST: FitnessUserLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userloginid,Username,Password,Userid")] FitnessUserLogin fitnessUserLogin)
        {
            if (id != fitnessUserLogin.Userloginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitnessUserLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnessUserLoginExists(fitnessUserLogin.Userloginid))
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
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserLogin.Userid);
            return View(fitnessUserLogin);
        }

        // GET: FitnessUserLogins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserLogin = await _context.FitnessUserLogins
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Userloginid == id);
            if (fitnessUserLogin == null)
            {
                return NotFound();
            }

            return View(fitnessUserLogin);
        }

        // POST: FitnessUserLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var fitnessUserLogin = await _context.FitnessUserLogins.FindAsync(id);
            _context.FitnessUserLogins.Remove(fitnessUserLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnessUserLoginExists(decimal id)
        {
            return _context.FitnessUserLogins.Any(e => e.Userloginid == id);
        }

        [HttpPost]
        public IActionResult Login([Bind("Username,Password")] FitnessUserLogin fitnessUserLogin)
        {
            var user = _context.FitnessUserLogins.Where(x => x.Password == fitnessUserLogin.Password && x.Username == fitnessUserLogin.Username).SingleOrDefault();
            if (user != null)
            {
                UserID = (decimal)fitnessUserLogin.Userid;
                return RedirectToAction("Index", "UserDashboard");

            }
            else
            {
                
            }
            return View();
        }
    }
}
