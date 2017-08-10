using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheNewsWebsite.Models.TheNewsWebsite;

namespace TheNewsWebsite.Controllers
{
    public class AdminsController : Controller
    {
        private readonly TheNewsContext _context;

        public AdminsController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            var list = from s in _context.Admins where s.Status == true select s;
            return View(list.ToList());
        }
        public async Task<IActionResult> ShowAll()
        {
            return View(await _context.Admins.ToListAsync());
        }
        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins
                .SingleOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public struct ListAuthority
        {
            public ListAuthority(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public IActionResult Create()
        {
            List<ListAuthority> authority = new List<ListAuthority>()
            {
                new ListAuthority{Id=1,Name="Super Admin"},
                new ListAuthority{Id=0,Name="Admin"},
            };
            ViewBag.Authority = new SelectList(authority.ToList(), "Id", "Name");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Name,Authority")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                admin.Status = true;
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            List<ListAuthority> authority = new List<ListAuthority>()
            {
                new ListAuthority{Id=1,Name="Super Admin"},
                new ListAuthority{Id=0,Name="Admin"},
            };
            ViewBag.Authority = new SelectList(authority.ToList(), "Id", "Name",admin.Authority);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.SingleOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }
            List<ListAuthority> authority = new List<ListAuthority>()
            {
                new ListAuthority{Id=1,Name="Super Admin"},
                new ListAuthority{Id=0,Name="Admin"},
            };
            ViewBag.Authority = new SelectList(authority.ToList(), "Id", "Name", admin.Authority);
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Name,Authority,Status")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            List<ListAuthority> authority = new List<ListAuthority>()
            {
                new ListAuthority{Id=1,Name="Super Admin"},
                new ListAuthority{Id=0,Name="Admin"},
            };
            ViewBag.Authority = new SelectList(authority.ToList(), "Id", "Name", admin.Authority);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .SingleOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.SingleOrDefaultAsync(m => m.Id == id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
