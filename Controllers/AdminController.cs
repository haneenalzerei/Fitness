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
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }

        // GET: Admin
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
                                    Diettext=fd.Diettext,
                                };

            ViewBag.data = fullouterjoin.ToListAsync();
           

            return View(await fullouterjoin.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUser = await _context.FitnessUsers
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (fitnessUser == null)
            {
                return NotFound();
            }

            return View(fitnessUser);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.FitnessRoles, "Roleid", "Rolename");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAddEmployee([Bind("Fullname,Phonenumber,Email,Roleid")] FitnessEmployee fitnessEmployee)
        {
            if (ModelState.IsValid)
            {

                FitnessEmployee lastEmployeeId = _context.FitnessEmployees.OrderByDescending(x => x.Empid).FirstOrDefault();
                decimal empID = 0;

                if (lastEmployeeId == null)
                {
                    empID = 1;
                }
                else
                {
                    empID = lastEmployeeId.Empid + 1;
                }
                fitnessEmployee.Empid = empID;
                _context.Add(fitnessEmployee);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUser = await _context.FitnessUsers.FindAsync(id);
            if (fitnessUser == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.FitnessRoles, "Roleid", "Rolename", fitnessUser.Roleid);
            return View(fitnessUser);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Phonenumber,Email,Roleid")] FitnessUser fitnessUser)
        {
            if (id != fitnessUser.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitnessUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnessUserExists(fitnessUser.Userid))
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
            ViewData["Roleid"] = new SelectList(_context.FitnessRoles, "Roleid", "Rolename", fitnessUser.Roleid);
            return View(fitnessUser);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnessUser = await _context.FitnessUsers
                .Include(f => f.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (fitnessUser == null)
            {
                return NotFound();
            }

            return View(fitnessUser);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var fitnessUser = await _context.FitnessUsers.FindAsync(id);
            _context.FitnessUsers.Remove(fitnessUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnessUserExists(decimal id)
        {
            return _context.FitnessUsers.Any(e => e.Userid == id);
        }
    }
}
