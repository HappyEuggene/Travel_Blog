using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Blog.Context;
using Travel_Blog.External;
using Travel_Blog.Models;

namespace Travel_Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly DBContext _context;
        private readonly BlobService _blobService;

        public BlogController(DBContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Blogs.Include(b => b.User);
            return View(await dBContext.ToListAsync());
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Image)
                .Include(b => b.Destination)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModelBlog blogVm)
        {
            var imageUri = await _blobService.UploadFileAsync(blogVm.ImageFile.OpenReadStream(), blogVm.ImageFile.FileName);

            var blog = new Blog()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Title = blogVm.Blog.Title,
                Content = blogVm.Blog.Content,
                Image = new Image { Url = imageUri }
            };
            _context.Blogs.Add(blog);
            _context.Destinations.Add(blogVm.Destination);
            blog.Destination.Add(blogVm.Destination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.Include(b => b.Destination).FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            var viewModel = new ViewModelBlog
            {
                Blog = blog,
                Destination = blog.Destination.FirstOrDefault()  // Assume each blog has at least one destination
            };
            return View(viewModel);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ViewModelBlog viewModel)
        {
            if (id != viewModel.Blog.Id)
            {
                return NotFound();
            }

                try
                {
                    var blog = await _context.Blogs.Include(b => b.Destination)
                                                   .FirstOrDefaultAsync(b => b.Id == id);
                    if (blog == null)
                    {
                        return NotFound();
                    }

                    blog.Title = viewModel.Blog.Title;
                    blog.Content = viewModel.Blog.Content;
                    blog.Destination.FirstOrDefault().Country = viewModel.Destination.Country;  // Assume each blog has at least one destination
                    blog.Destination.FirstOrDefault().City = viewModel.Destination.City;

                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(viewModel.Blog.Id))
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

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'DBContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
