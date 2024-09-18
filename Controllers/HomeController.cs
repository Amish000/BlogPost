using BlogPost.Data;
using BlogPost.Models;
using BlogPost.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogPost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPosts.Where(t=>t.IsActive).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPosts blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogPosts blogPosts)
        {
            var blogPost = await _context.BlogPosts.FindAsync(blogPosts.Id);
            if (blogPost is not null)
            {
                blogPost.Title = blogPosts.Title;
                blogPost.Content = blogPosts.Content;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost is not null)
            {
                blogPost.IsActive = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Recycle()
        {
            return View(await _context.BlogPosts.Where(t => !t.IsActive).ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if(blogPost is not null)
            {
                blogPost.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost is not null)
            {
                _context.BlogPosts.Remove(blogPost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Recycle");
        }

    }
}