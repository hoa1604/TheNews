using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheNewsWebsite.Models.TheNewsWebsite;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace TheNewsWebsite.Controllers
{
    public class PostsAdminController : Controller
    {
       
        private readonly TheNewsContext _context;
        private readonly IHostingEnvironment _environment;
        public PostsAdminController(TheNewsContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: PostsAdmin
        public async Task<IActionResult> Index()
        {
            //  var theNewsContext = _context.Posts.Include(p => p.Author).Include(p => p.CategaryChild);
            //  return View(await theNewsContext.ToListAsync());
            ViewData["ViewData"] = 9;
            return View();
        }
        [HttpGet]
        public JsonResult GetPosts()
        {
            var posts = from s in _context.Posts.OrderByDescending(s => s.DateCreate) where (s.Status != 4) select s;
            return Json(from post in posts
                        select new
                        {
                            postId = post.Id,
                            postTitle = post.Title,
                            postDescription = post.Description,
                            postAuthor = post.Author.Name
                        });
        }
        public async Task<IActionResult> ShowAll()
        {
            var list = from s in _context.Posts.OrderByDescending(s => s.DateCreate) select s;
            return View(list.ToList());
        }
        public JsonResult GetAllPosts()
        {
            var posts = from s in _context.Posts.OrderByDescending(s => s.DateCreate) select s;
            return Json(from post in posts
                        select new
                        {
                            postId = post.Id,
                            postTitle = post.Title,
                            postDescription = post.Description,
                            postAuthor = post.Author.Name
                        });
        }
        // GET: PostsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.CategaryChild)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.Status == 1)
            {
                ViewBag.Status = "Chưa đăng";
            }
            else
            {
                if (post.Status == 2)
                {
                    ViewBag.Status = "Đang đăng";
                }
                else
                {
                    if (post.Status == 3)
                    {
                        ViewBag.Status = "Đã gỡ";
                    }
                    else
                    {
                        ViewBag.Status = "Đã xóa";
                    }
                }
            }
            return View(post);
        }
        // GET: PostsAdmin/Create
        public IActionResult Create()
        {
            List<ListCate> status = new List<ListCate>()
            {
                new ListCate{Id=1,Name="Chưa đăng"},
                new ListCate{Id=2,Name="Đang đăng"},
                new ListCate{Id=3,Name="Đã gỡ"},
                new ListCate{Id=4,Name="Đã xóa"},

            };
            ViewBag.Status = new SelectList(status.ToList(), "Id", "Name");
            var cate = from c in _context.Categaries where c.Status == true select c;
            ViewBag.Cate = new SelectList(cate.ToList(), "Id", "Name");
            var aut = from c in _context.Authors where c.Status == 1 select c;
            ViewData["AuthorId"] = new SelectList(aut.ToList(), "Id", "Name");
            var cateChild = from c in _context.CategaryChilds where c.Status == true select c;
            ViewData["CategaryChildId"] = new SelectList(cateChild.ToList(), "Id", "Name");
            return View();
        }

        // POST: PostsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public struct ListCate
        {
            public ListCate(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [HttpPost]
        public JsonResult Categary(int cateId)
        {
            var cateChild = from c in _context.CategaryChilds where c.Status == true && c.CategariesId == cateId select c;
            var resp2 = cateChild.ToArray();
            //  List<string> name = new List<string>();
            // List<int> id = new List<int>();
            List<ListCate> resp = new List<ListCate>();
            foreach (var item in resp2)
            {
                var cate = new ListCate();
                cate.Id = item.Id;
                cate.Name = item.Name;
                resp.Add(cate);
            }
            return Json(resp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Content,DateCreate,DatePublish,NumView,Keyword,Status,CategaryChildId,PublishBy,AuthorId")] Post post, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var uploadpath = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(Path.Combine(uploadpath));
                    string filename = Image.FileName;
                    if (filename.Contains('\\'))
                    {
                        filename = filename.Split('\\').Last();
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(uploadpath, filename), FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);

                        post.Image = "/images/" + Image.FileName;
                    }
                }
                post.DateCreate = DateTime.Now;
                //   post.Author.NumPost = post.Author.NumPost + 1;
                _context.Add(post);
                // var auth = from s in _context.Authors where s.Id == post.Author.Id select s;
                //var auth = _context.Authors.SingleOrDefault(s => s.Id == post.AuthorId);
                //auth.NumPost = auth.NumPost + 1;
             //   _context.Update(auth);
                try { await _context.SaveChangesAsync(); }
                catch (Exception ex)
                {

                }

                return View("Index");
            }
            List<ListCate> status = new List<ListCate>()
            {
                new ListCate{Id=1,Name="Chưa đăng"},
                new ListCate{Id=2,Name="Đang đăng"},
                new ListCate{Id=4,Name="Đã gỡ"},
                new ListCate{Id=4,Name="Đã xóa"},

            };
            ViewBag.Status = new SelectList(status.ToList(), "Id", "Name", post.Status);
            var cate = from c in _context.Categaries where c.Status == true select c;
            ViewBag.Cate = new SelectList(cate.ToList(), "Id", "Name");
            var aut = from c in _context.Authors where c.Status == 1 select c;
            ViewData["AuthorId"] = new SelectList(aut.ToList(), "Id", "Name", post.AuthorId);
            var cateChild = from c in _context.CategaryChilds where c.Status == true select c;
            ViewData["CategaryChildId"] = new SelectList(cateChild.ToList(), "Id", "Name", post.CategaryChildId);

            return View();
        }

        // GET: PostsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            List<ListCate> status = new List<ListCate>()
            {
                new ListCate{Id=1,Name="Chưa đăng"},
                new ListCate{Id=2,Name="Đang đăng"},
                new ListCate{Id=3,Name="Đã gỡ"},
                new ListCate{Id=4,Name="Đã xóa"},

            };
            ViewBag.Status = new SelectList(status.ToList(), "Id", "Name", post.Status);
            var cate = from c in _context.Categaries where c.Status == true select c;
            ViewBag.Cate = new SelectList(cate.ToList(), "Id", "Name");
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", post.AuthorId);
            ViewData["CategaryChildId"] = new SelectList(_context.CategaryChilds, "Id", "Name", post.CategaryChildId);
            return View(post);
        }

        // POST: PostsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Content,DateCreate,DatePublish,NumView,Keyword,Status,CategeryId,CategaryChildId,PublishBy,AuthorId")] Post post, IFormFile image)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            else
            {
                Post currentPost =await _context.Posts.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);


                if (ModelState.IsValid)
                {
                    if (image != null && image.Length > 0)
                    {
                        var uploadpath = Path.Combine(_environment.WebRootPath, "images");
                        Directory.CreateDirectory(Path.Combine(uploadpath));
                        string filename = image.FileName;
                        if (filename.Contains('\\'))
                        {
                            filename = filename.Split('\\').Last();
                        }
                        using (FileStream fileStream = new FileStream(Path.Combine(uploadpath, filename), FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);

                            post.Image = "/images/" + image.FileName;
                        }
                    }
                    else
                    {
                        post.Image = currentPost.Image;
                    }
              

                try
                    {
                        _context.Update(post);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostExists(post.Id))
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
            }

            List<ListCate> status = new List<ListCate>()
            {
                new ListCate{Id=1,Name="Chưa đăng"},
                new ListCate{Id=2,Name="Đang đăng"},
                new ListCate{Id=4,Name="Đã gỡ"},
                new ListCate{Id=4,Name="Đã xóa"},

            };
            ViewBag.Status = new SelectList(status.ToList(), "Id", "Name", post.Status);
            var cate = from c in _context.Categaries where c.Status == true select c;
            ViewBag.Cate = new SelectList(cate.ToList(), "Id", "Name");
            var aut = from c in _context.Authors where c.Status == 1 select c;
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", post.AuthorId);
            ViewData["CategaryChildId"] = new SelectList(_context.CategaryChilds, "Id", "Name", post.CategaryChildId);
            return View(post);
        }

        // GET: PostsAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.CategaryChild)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: PostsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            post.Status = 4;
            _context.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
