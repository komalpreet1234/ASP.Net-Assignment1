using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrugStoreManagement.Data;
using DrugStoreManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace DrugStoreManagement.Controllers
{
    public class DrugsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public DrugsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Drugs
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View(await _context.Drugs.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Drugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var drug = await _context.Drugs
                    .FirstOrDefaultAsync(m => m.DrugCode == id);
                if (drug == null)
                {
                    return NotFound();
                }

                return View(drug);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Drugs/Create
        public IActionResult Create()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Drugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugCode,DrugName,Category,DrugCompany,Description,Quantity")] Drug drug)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(drug);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(drug);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Drugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var drug = await _context.Drugs.FindAsync(id);
                if (drug == null)
                {
                    return NotFound();
                }
                return View(drug);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Drugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrugCode,DrugName,Category,DrugCompany,Description,Quantity")] Drug drug)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (id != drug.DrugCode)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(drug);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DrugExists(drug.DrugCode))
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

                return View(drug);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Drugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var drug = await _context.Drugs
                    .FirstOrDefaultAsync(m => m.DrugCode == id);
                if (drug == null)
                {
                    return NotFound();
                }

                return View(drug);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var drug = await _context.Drugs.FindAsync(id);
                _context.Drugs.Remove(drug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.DrugCode == id);
        }
    }
}
