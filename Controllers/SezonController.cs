using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSkoki.Data;
using MvcSkoki.Models;

namespace MvcSkoki.Controllers
{
    public class SezonController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";
        private readonly MvcSkokiContext _context;

        public SezonController(MvcSkokiContext context)
        {
            _context = context;
        }

        // GET: Sezon
        public async Task<IActionResult> Index()
        {
                    if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }
              return _context.Sezon != null ? 
                          View(await _context.Sezon.ToListAsync()) :
                          Problem("Entity set 'MvcSkokiContext.Sezon'  is null.");
        }

        // GET: Sezon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sezon == null)
            {
                return NotFound();
            }

            var sezon = await _context.Sezon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sezon == null)
            {
                return NotFound();
            }

            return View(sezon);
        }

        // GET: Sezon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sezon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rok")] Sezon sezon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sezon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sezon);
        }

        // GET: Sezon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sezon == null)
            {
                return NotFound();
            }

            var sezon = await _context.Sezon.FindAsync(id);
            if (sezon == null)
            {
                return NotFound();
            }
            return View(sezon);
        }

        // POST: Sezon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rok")] Sezon sezon)
        {
            if (id != sezon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sezon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SezonExists(sezon.Id))
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
            return View(sezon);
        }

        // GET: Sezon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sezon == null)
            {
                return NotFound();
            }

            var sezon = await _context.Sezon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sezon == null)
            {
                return NotFound();
            }

            return View(sezon);
        }

        // POST: Sezon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sezon == null)
            {
                return Problem("Entity set 'MvcSkokiContext.Sezon'  is null.");
            }
            var sezon = await _context.Sezon.FindAsync(id);
            if (sezon != null)
            {
                _context.Sezon.Remove(sezon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SezonExists(int id)
        {
          return (_context.Sezon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
