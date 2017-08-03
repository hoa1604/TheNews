using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheNewsWebsite.Models.TheNewsWebsite;
using Microsoft.AspNetCore.Authorization;

namespace TheNewsWebsite.Controllers
{
    [Authorize]
    public class CategariesAdminController : Controller
    {

        private readonly TheNewsContext _context;

        public CategariesAdminController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: CategariesAdmin
        public async Task<IActionResult> Index()
        {
            var list = from s in _context.Categaries where s.Status == true select s;
            return View(list.ToList());
        }
        public async Task<IActionResult> ShowAll()
        {
            return View(await _context.Categaries.ToListAsync());
        }

        // GET: CategariesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categary = await _context.Categaries
                .SingleOrDefaultAsync(m => m.Id == id);
            if (categary == null)
            {
                return NotFound();
            }

            return View(categary);
        }

        // GET: CategariesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategariesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Categary categary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categary);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categary);
        }

        // GET: CategariesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categary = await _context.Categaries.SingleOrDefaultAsync(m => m.Id == id);
            if (categary == null)
            {
                return NotFound();
            }
            ViewBag.Status = categary.Status;
            return View(categary);
        }

        // POST: CategariesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Categary categary)
        {
            if (id != categary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategaryExists(categary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(categary);
        }

        // GET: CategariesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categary = await _context.Categaries
                .SingleOrDefaultAsync(m => m.Id == id);
            if (categary == null)
            {
                return NotFound();
            }

            return View(categary);
        }

        // POST: CategariesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categary = await _context.Categaries.SingleOrDefaultAsync(m => m.Id == id);
            categary.Status = false;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategaryExists(int id)
        {
            return _context.Categaries.Any(e => e.Id == id);
        }
    }
}
