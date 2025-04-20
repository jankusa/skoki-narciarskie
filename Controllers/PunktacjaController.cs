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
    public class PunktacjaController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";
        private readonly MvcSkokiContext _context;

        public PunktacjaController(MvcSkokiContext context)
        {
            _context = context;
        }

        // GET: Punktacja
        public async Task<IActionResult> Index()
        {
                    if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }
            var mvcSkokiContext = _context.Punktacja.Include(p => p.Konkurs).Include(p => p.Skoczek);
            return View(await mvcSkokiContext.ToListAsync());
        }

        // GET: Punktacja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Punktacja == null)
            {
                return NotFound();
            }

            var punktacja = await _context.Punktacja
                .Include(p => p.Konkurs)
                .Include(p => p.Skoczek)
                .FirstOrDefaultAsync(m => m.PunktacjaID == id);
            if (punktacja == null)
            {
                return NotFound();
            }

            return View(punktacja);
        }

        // GET: Punktacja/Create
        public IActionResult Create()
        {
            ViewData["KonkursID"] = new SelectList(_context.Konkurs, "Id", "Id");
            ViewData["SkoczekID"] = new SelectList(_context.Skoczek, "Id", "Id");
            return View();
        }

        // POST: Punktacja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PunktacjaID,SkoczekID,KonkursID,Wynik")] Punktacja punktacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(punktacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KonkursID"] = new SelectList(_context.Konkurs, "Id", "Id", punktacja.KonkursID);
            ViewData["SkoczekID"] = new SelectList(_context.Skoczek, "Id", "Id", punktacja.SkoczekID);
            return View(punktacja);
        }

        // GET: Punktacja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Punktacja == null)
            {
                return NotFound();
            }

            var punktacja = await _context.Punktacja.FindAsync(id);
            if (punktacja == null)
            {
                return NotFound();
            }
            ViewData["KonkursID"] = new SelectList(_context.Konkurs, "Id", "Id", punktacja.KonkursID);
            ViewData["SkoczekID"] = new SelectList(_context.Skoczek, "Id", "Id", punktacja.SkoczekID);
            return View(punktacja);
        }

        // POST: Punktacja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PunktacjaID,SkoczekID,KonkursID,Wynik")] Punktacja punktacja)
        {
            if (id != punktacja.PunktacjaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punktacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PunktacjaExists(punktacja.PunktacjaID))
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
            ViewData["KonkursID"] = new SelectList(_context.Konkurs, "Id", "Id", punktacja.KonkursID);
            ViewData["SkoczekID"] = new SelectList(_context.Skoczek, "Id", "Id", punktacja.SkoczekID);
            return View(punktacja);
        }

        // GET: Punktacja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Punktacja == null)
            {
                return NotFound();
            }

            var punktacja = await _context.Punktacja
                .Include(p => p.Konkurs)
                .Include(p => p.Skoczek)
                .FirstOrDefaultAsync(m => m.PunktacjaID == id);
            if (punktacja == null)
            {
                return NotFound();
            }

            return View(punktacja);
        }

        // POST: Punktacja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Punktacja == null)
            {
                return Problem("Entity set 'MvcSkokiContext.Punktacja'  is null.");
            }
            var punktacja = await _context.Punktacja.FindAsync(id);
            if (punktacja != null)
            {
                _context.Punktacja.Remove(punktacja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunktacjaExists(int id)
        {
          return (_context.Punktacja?.Any(e => e.PunktacjaID == id)).GetValueOrDefault();
        }
    }
}
