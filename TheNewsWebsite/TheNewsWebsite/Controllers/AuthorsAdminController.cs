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
    public class AuthorsAdminController : Controller
    {

        private readonly TheNewsContext _context;

        public AuthorsAdminController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: AuthorsAdmin
        public async Task<IActionResult> Index()
        {
            //var list = from s in _context.Authors where s.Status ==1 select s;
            //return View(list.ToList());
            return View();
        }
        [HttpGet]
        public JsonResult GetAuthors()
        {
            var authors = from s in _context.Authors.Include(s=>s.Posts) where (s.Status == 1) select new {
                Id=s.Id,
                Name=s.Name,
               Numpost=s.Posts.Count()
            };
            return Json(from auth in authors
                        select new
                        {
                            authorId = auth.Id,
                            authorName = auth.Name,
                           authorNumPost=auth.Numpost
                        });
        }
        public async Task<IActionResult> ShowAll()
        {
            var list = from s in _context.Authors select s;
            return View(list.ToList());
        }
        [HttpGet]
        public JsonResult GetAllAuthors()
        {
            var authors = from s in _context.Authors.Include(s => s.Posts)
                          select new
                          {
                              Id = s.Id,
                              Name = s.Name,
                              Numpost = s.Posts.Count()
                          };
            return Json(from auth in authors
                        select new
                        {
                            authorId = auth.Id,
                            authorName = auth.Name,
                            authorNumPost = auth.Numpost
                        });
        }
        // GET: AuthorsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            if(author.Status==1)
            {
                ViewBag.Status = "Đang cộng tác";
            }
            else
            {
                ViewBag.Status = "Đã nghỉ";
            }
            return View(author);
        }

        // GET: AuthorsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Author author)
        {
            if (ModelState.IsValid)
            {
               // author.NumPost = 0;
                author.Status =1;
                _context.Add(author);
                await _context.SaveChangesAsync();
                return View("Index");
            }
            return View(author);
        }

        // GET: AuthorsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            List<SelectListItem> listStatus = new List<SelectListItem>();
            listStatus.Add(new SelectListItem() { Text = "Đã nghỉ", Value = "0" });
            listStatus.Add(new SelectListItem() { Text = "Đang cộng tác", Value = "1" });
            ViewData["StatusAuthor"] = new SelectList(listStatus, "Value", "Text");
            return View(author);
        }

        // POST: AuthorsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                List<SelectListItem> listStatus = new List<SelectListItem>();
                listStatus.Add(new SelectListItem() { Text = "Đã nghỉ", Value = "0" });
                listStatus.Add(new SelectListItem() { Text = "Đang cộng tác", Value = "1" });
                ViewData["StatusAuthor"] = new SelectList(listStatus, "Value", "Text");
                return RedirectToAction("Index");
            }
            
            return View(author);
        }

        // GET: AuthorsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.Include(s => s.Posts)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            if (author.Status == 1)
            {
                ViewBag.Status = "Đang cộng tác";
            }
            else
            {
                ViewBag.Status = "Đã nghỉ";
            }
            return View(author);
        }

        // POST: AuthorsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            author.Status = 0;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
