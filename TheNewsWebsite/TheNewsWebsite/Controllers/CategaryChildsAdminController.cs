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
    public class CategaryChildsAdminController : Controller
    {
        private readonly TheNewsContext _context;

        public CategaryChildsAdminController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: CategaryChildsAdmin
        public async Task<IActionResult> Index()
        {
            var theNewsContext = from s in _context.CategaryChilds.Include(s => s.Categaries) where s.Status == true && s.Categaries.Status==true select s;
            return View(theNewsContext.ToList());
        }
        public async Task<IActionResult> ShowAll()
        {
            var theNewsContext = _context.CategaryChilds.Include(c => c.Categaries);
            return View(await theNewsContext.ToListAsync());
        }

        // GET: CategaryChildsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categaryChild = await _context.CategaryChilds
                .Include(c => c.Categaries)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (categaryChild == null)
            {
                return NotFound();
            }
            return View(categaryChild);
        }

        // GET: CategaryChildsAdmin/Create
        public IActionResult Create()
        {
            var cate = from s in _context.Categaries where s.Status == true select s;
            ViewData["CategariesId"] = new SelectList(cate.ToList(), "Id", "Name");
            return View();
        }

        // POST: CategaryChildsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,CategariesId")] CategaryChild categaryChild)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categaryChild);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var cate = from s in _context.Categaries where s.Status == true select s;
            ViewData["CategariesId"] = new SelectList(cate.ToList(), "Id", "Name");
            return View(categaryChild);
        }

        // GET: CategaryChildsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categaryChild = await _context.CategaryChilds.SingleOrDefaultAsync(m => m.Id == id);
            if (categaryChild == null)
            {
                return NotFound();
            }
            ViewBag.Status = categaryChild.Status;
            ViewData["CategariesId"] = new SelectList(_context.Categaries, "Id", "Name", categaryChild.CategariesId);
            return View(categaryChild);
        }

        // POST: CategaryChildsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,CategariesId")] CategaryChild categaryChild)
        {
            if (id != categaryChild.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categaryChild);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategaryChildExists(categaryChild.Id))
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
            ViewData["CategariesId"] = new SelectList(_context.Categaries, "Id", "Name", categaryChild.Categaries.Name);
            return View(categaryChild);
        }

        // GET: CategaryChildsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categaryChild = await _context.CategaryChilds
                .Include(c => c.Categaries)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (categaryChild == null)
            {
                return NotFound();
            }

            return View(categaryChild);
        }

        // POST: CategaryChildsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categaryChild = await _context.CategaryChilds.SingleOrDefaultAsync(m => m.Id == id);
            categaryChild.Status = false;
            // _context.CategaryChilds.Remove(categaryChild);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CategaryChildExists(int id)
        {
            return _context.CategaryChilds.Any(e => e.Id == id);
        }
    }
}
