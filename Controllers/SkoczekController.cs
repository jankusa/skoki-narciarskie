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
    public class SkoczekController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";
        private readonly MvcSkokiContext _context;

        public SkoczekController(MvcSkokiContext context)
        {
            _context = context;
        }

        // GET: Skoczek
        public async Task<IActionResult> Index()
        {
                    if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }
              return _context.Skoczek != null ? 
                          View(await _context.Skoczek.ToListAsync()) :
                          Problem("Entity set 'MvcSkokiContext.Skoczek'  is null.");
        }

        // GET: Skoczek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Skoczek == null)
            {
                return NotFound();
            }

            var skoczek = await _context.Skoczek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skoczek == null)
            {
                return NotFound();
            }

            return View(skoczek);
        }

        // GET: Skoczek/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skoczek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Rok_urodzenia,Wzrost,Narodowosc")] Skoczek skoczek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skoczek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skoczek);
        }

        // GET: Skoczek/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Skoczek == null)
            {
                return NotFound();
            }

            var skoczek = await _context.Skoczek.FindAsync(id);
            if (skoczek == null)
            {
                return NotFound();
            }
            return View(skoczek);
        }

        // POST: Skoczek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Rok_urodzenia,Wzrost,Narodowosc")] Skoczek skoczek)
        {
            if (id != skoczek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skoczek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkoczekExists(skoczek.Id))
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
            return View(skoczek);
        }

        // GET: Skoczek/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Skoczek == null)
            {
                return NotFound();
            }

            var skoczek = await _context.Skoczek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skoczek == null)
            {
                return NotFound();
            }

            return View(skoczek);
        }

        // POST: Skoczek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Skoczek == null)
            {
                return Problem("Entity set 'MvcSkokiContext.Skoczek'  is null.");
            }
            var skoczek = await _context.Skoczek.FindAsync(id);
            if (skoczek != null)
            {
                _context.Skoczek.Remove(skoczek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkoczekExists(int id)
        {
          return (_context.Skoczek?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
