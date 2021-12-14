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
    public class UserDashboardController : Controller
    {
        private readonly ModelContext _context;

        public UserDashboardController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserDashboard
        public async Task<IActionResult> Index()
        {
         //   var x = TempData["UserID"];
            var fitnessDietInformation = await _context.FitnessUserDiets
               .FirstOrDefaultAsync(m => m.Userid == 1);
           
            return View(fitnessDietInformation);
        }

        // GET: UserDashboard/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessDietInformation = await _context.FitnessDietInformations
                .FirstOrDefaultAsync(m => m.Dietid == id);
            if (fitnessDietInformation == null)
            {
                return NotFound();
            }

            return View(fitnessDietInformation);
        }

        // GET: UserDashboard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserDashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiet([Bind("Height,Weight")] FitnessUserInformation fitnessUserInformation)
        {
            if (ModelState.IsValid)
            {
                fitnessUserInformation.Userinformationid = 2;
                fitnessUserInformation.Userid = 1;
                _context.Add(fitnessUserInformation);
                var fitnessDietInformation = await _context.FitnessDietInformations
               .FirstOrDefaultAsync(m => fitnessUserInformation.Height<=m.Maxheight && fitnessUserInformation.Weight<=m.Maxweight);
                if (fitnessDietInformation != null)
                {
                    FitnessUserDiet fitnessUserDiet = new FitnessUserDiet();
                    fitnessUserDiet.Id = 2;
                    fitnessUserDiet.Userid = 1;
                    fitnessUserDiet.Diettext = fitnessDietInformation.Diettext;

                    _context.Add(fitnessUserDiet);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fitnessUserInformation);
        }

        // GET: UserDashboard/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessDietInformation = await _context.FitnessDietInformations.FindAsync(id);
            if (fitnessDietInformation == null)
            {
                return NotFound();
            }
            return View(fitnessDietInformation);
        }

        // POST: UserDashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Dietid,Diettext,Maxheight,Maxweight")] FitnessDietInformation fitnessDietInformation)
        {
            if (id != fitnessDietInformation.Dietid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitnessDietInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnessDietInformationExists(fitnessDietInformation.Dietid))
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
            return View(fitnessDietInformation);
        }

        // GET: UserDashboard/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessDietInformation = await _context.FitnessDietInformations
                .FirstOrDefaultAsync(m => m.Dietid == id);
            if (fitnessDietInformation == null)
            {
                return NotFound();
            }

            return View(fitnessDietInformation);
        }

        // POST: UserDashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var fitnessDietInformation = await _context.FitnessDietInformations.FindAsync(id);
            _context.FitnessDietInformations.Remove(fitnessDietInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnessDietInformationExists(decimal id)
        {
            return _context.FitnessDietInformations.Any(e => e.Dietid == id);
        }
    }
}
