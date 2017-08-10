using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheNewsWebsite.Models.TheNewsWebsite;


namespace TheNewsWebsite.Controllers
{
    public class CommentsAdminController : Controller
    {
        private readonly TheNewsContext _context;

        public CommentsAdminController(TheNewsContext context)
        {
            _context = context;    
        }

        // GET: CommentsAdmin
        public async Task<IActionResult> Index()
        {
            //var theNewsContext = _context.Comments.OrderByDescending(c=>c.CmtDate).Include(c => c.Post).Include(c => c.User);
            //return View(await theNewsContext.ToListAsync());
            return View();
        }
        public JsonResult GetComments()
        {
            var cmts = from s in _context.Comments.Include(c => c.Post).Include(c => c.User)
                        select new
                        {
                            Id = s.Id,
                            Content = s.Content,
                            Post = s.Post.Title,
                            User = s.User.Name,
                            PostId=s.PostId
                        };
            return Json(from cmt in cmts
                        select new
                        {
                            cmtId = cmt.Id,
                            cmtContent = cmt.Content,
                            cmtPost = cmt.Post,
                            cmtUser = cmt.User,
                            cmtPostId=cmt.PostId
                        });
        }
        // GET: CommentsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View("~/PostsAdmin / Details / "+id);
        }

        // GET: CommentsAdmin/Create
        //public IActionResult Create()
        //{
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name");
        //    return View();
        //}

        // POST: CommentsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Cmt,CmtDate,Status,UserId,PostId")] Comment comment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string cmt = comment.Cmt;
        //        int index = cmt.IndexOf(" ",80);
        //        string cmtCut = cmt.Substring(0, index);
        //        string dot = "...";
        //        comment.Content = string.Concat(cmtCut, dot);
        //        comment.CmtDate = DateTime.Now;
        //        _context.Add(comment);
        //        Post post = _context.Posts.Where(s=>s.Id == comment.PostId).FirstOrDefault();
        //        post.Comments.Add(comment);
        //        _context.Update(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title", comment.PostId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", comment.UserId);
        //    return View(comment);
        //}

        // GET: CommentsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", comment.UserId);
            return View(comment);
        }

        // POST: CommentsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cmt,CmtDate,Status,UserId,PostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", comment.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", comment.UserId);
            return View(comment);
        }

        // GET: CommentsAdmin/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments
        //        .Include(c => c.Post)
        //        .Include(c => c.User)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        // POST: CommentsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
