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
    public class AccountantDashboardController : Controller
    {
        private readonly ModelContext _context;

        public AccountantDashboardController(ModelContext context)
        {
            _context = context;
        }

        // GET: AccountantDashboard
        public async Task<IActionResult> Index()
        {
            var fullouterjoin = from fu in _context.FitnessUsers
                                join fd in _context.FitnessUserDiets
                                on fu.Userid equals fd.Userid
                                select new Test
                                {
                                    Userid = fu.Userid,
                                    Fullname = fu.Fullname,
                                    Phonenumber = fu.Phonenumber,
                                    Email = fu.Email,
                                    Roleid = fu.Roleid,
                                    Diettext = fd.Diettext,
                                };

            ViewBag.data = fullouterjoin.ToListAsync();


            return View(await fullouterjoin.ToListAsync());
        }

        // GET: AccountantDashboard/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserDiet = await _context.FitnessUserDiets
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnessUserDiet == null)
            {
                return NotFound();
            }

            return View(fitnessUserDiet);
        }

        // GET: AccountantDashboard/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid");
            return View();
        }

        // POST: AccountantDashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Userid,Diettext")] FitnessUserDiet fitnessUserDiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fitnessUserDiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserDiet.Userid);
            return View(fitnessUserDiet);
        }

        // GET: AccountantDashboard/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserDiet = await _context.FitnessUserDiets.FindAsync(id);
            if (fitnessUserDiet == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserDiet.Userid);
            return View(fitnessUserDiet);
        }

        // POST: AccountantDashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Userid,Diettext")] FitnessUserDiet fitnessUserDiet)
        {
            if (id != fitnessUserDiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitnessUserDiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnessUserDietExists(fitnessUserDiet.Id))
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
            ViewData["Userid"] = new SelectList(_context.FitnessUsers, "Userid", "Userid", fitnessUserDiet.Userid);
            return View(fitnessUserDiet);
        }

        // GET: AccountantDashboard/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUserDiet = await _context.FitnessUserDiets
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnessUserDiet == null)
            {
                return NotFound();
            }

            return View(fitnessUserDiet);
        }

        // POST: AccountantDashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var fitnessUserDiet = await _context.FitnessUserDiets.FindAsync(id);
            _context.FitnessUserDiets.Remove(fitnessUserDiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnessUserDietExists(decimal id)
        {
            return _context.FitnessUserDiets.Any(e => e.Id == id);
        }
    }
}
