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
    public class UsersAdminController : Controller
    {
        private readonly TheNewsContext _context;

        public UsersAdminController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: UsersAdmin
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public JsonResult GetUsers()
        {
            var users = from s in _context.Users
                        where (s.Status == 1)
                        select new
                        {
                            Id = s.Id,
                             Name = s.Name,
                              Gender =s.Gender,
                              Email =s.Email
                          };
            return Json(from user in users
                        select new
                        {
                            userId=user.Id,
                            userName = user.Name,
                            userGender=user.Gender,
                            userEmail=user.Email
                        });
        }
        public async Task<IActionResult> ShowBlock()
        {
            return View(await _context.Users.ToListAsync());
        }
        public JsonResult GetBlockUsers()
        {
            var users = from s in _context.Users
                        where (s.Status == 2)
                        select new
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Gender = s.Gender,
                            Email = s.Email
                        };
            return Json(from user in users
                        select new
                        {
                            userId = user.Id,
                            userName = user.Name,
                            userGender = user.Gender,
                            userEmail = user.Email
                        });
        }
        // GET: UsersAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Email,Password,Status,DateCreate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.DateCreate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: UsersAdmin/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: UsersAdmin/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Email,Password,Status,DateCreate")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            user.DateCreate = DateTime.Now;
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

        // GET: UsersAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            user.Status = 0;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Block(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersAdmin/Delete/5
        [HttpPost, ActionName("Block")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            user.Status = 2;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Unlock(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersAdmin/Delete/5
        [HttpPost, ActionName("Unlock")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            user.Status = 1;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
