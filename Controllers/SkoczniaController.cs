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
    public class SkoczniaController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";
        private readonly MvcSkokiContext _context;

        public SkoczniaController(MvcSkokiContext context)
        {
            _context = context;
        }

        // GET: Skocznia
        public async Task<IActionResult> Index()
        {
                    if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }
            var mvcSkokiContext = _context.Skocznia.Include(s => s.Skoczek);
            return View(await mvcSkokiContext.ToListAsync());
        }

        // GET: Skocznia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Skocznia == null)
            {
                return NotFound();
            }

            var skocznia = await _context.Skocznia
                .Include(s => s.Skoczek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skocznia == null)
            {
                return NotFound();
            }

            return View(skocznia);
        }

        // GET: Skocznia/Create
        public IActionResult Create()
        {
            ViewData["SkoczekID"] = new SelectList(_context.Set<Skoczek>(), "Id", "Id");
            return View();
        }

        // POST: Skocznia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Miejscowosc,Panstwo,K,HS,Rekord,SkoczekID")] Skocznia skocznia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skocznia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkoczekID"] = new SelectList(_context.Set<Skoczek>(), "Id", "Id", skocznia.SkoczekID);
            return View(skocznia);
        }

        // GET: Skocznia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Skocznia == null)
            {
                return NotFound();
            }

            var skocznia = await _context.Skocznia.FindAsync(id);
            if (skocznia == null)
            {
                return NotFound();
            }
            ViewData["SkoczekID"] = new SelectList(_context.Set<Skoczek>(), "Id", "Id", skocznia.SkoczekID);
            return View(skocznia);
        }

        // POST: Skocznia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Miejscowosc,Panstwo,K,HS,Rekord,SkoczekID")] Skocznia skocznia)
        {
            if (id != skocznia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skocznia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkoczniaExists(skocznia.Id))
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
            ViewData["SkoczekID"] = new SelectList(_context.Set<Skoczek>(), "Id", "Id", skocznia.SkoczekID);
            return View(skocznia);
        }

        // GET: Skocznia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Skocznia == null)
            {
                return NotFound();
            }

            var skocznia = await _context.Skocznia
                .Include(s => s.Skoczek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skocznia == null)
            {
                return NotFound();
            }

            return View(skocznia);
        }

        // POST: Skocznia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Skocznia == null)
            {
                return Problem("Entity set 'MvcSkokiContext.Skocznia'  is null.");
            }
            var skocznia = await _context.Skocznia.FindAsync(id);
            if (skocznia != null)
            {
                _context.Skocznia.Remove(skocznia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkoczniaExists(int id)
        {
          return (_context.Skocznia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
