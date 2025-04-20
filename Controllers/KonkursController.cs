using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcSkoki.Data;
using MvcSkoki.Models;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MvcSkoki.Controllers
{
    public class KonkursController : Controller
    {
        private const string SessionKeyLoggedIn = "_LoggedIn";
        private readonly MvcSkokiContext _context;

        public KonkursController(MvcSkokiContext context)
        {
            _context = context;
        }
        // GET: Konkurs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
            {
                return RedirectToAction("Logowanie", "IO");
            }
            var mvcSkokiContext = _context.Konkurs.Include(k => k.Sezon).Include(k => k.Skocznia);
            return View(await mvcSkokiContext.ToListAsync());
        }

        // GET: Konkurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Konkurs == null)
            {
                return NotFound();
            }

            var konkurs = await _context.Konkurs
                .Include(k => k.Sezon)
                .Include(k => k.Skocznia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konkurs == null)
            {
                return NotFound();
            }

            return View(konkurs);
        }

        public async Task<IActionResult> Ranking(int? id)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=MvcSkoki.Data.db;Version=3;");
            connection.Open();

            if (id == null || _context.Konkurs == null)
            {
                return NotFound();
            }


            List<List<object>> wyniki = new List<List<object>>();

            using (SQLiteCommand command = new SQLiteCommand($"SELECT row_number() over (ORDER BY cast(Wynik as real) DESC) as Miejsce, Imie, Nazwisko, Narodowosc, Wynik FROM Punktacja join Skoczek on Skoczek.Id = Punktacja.SkoczekID WHERE KonkursID = {id} ORDER BY cast(Wynik as real) DESC", connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    // Dodaj nazwy kolumn do listy punktacji
                    List<object> nazwyKolumn = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        nazwyKolumn.Add(reader.GetName(i));
                    }
                    wyniki.Add(nazwyKolumn);

                    // Dodaj dane do listy punktacji
                    while (reader.Read())
                    {
                        List<object> punktacja = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            punktacja.Add(reader.GetValue(i));
                        }
                        wyniki.Add(punktacja);
                    }
                }
            }

            ViewData["Punktacje"] = wyniki;

            var konkurs = await _context.Konkurs
                .Include(k => k.Sezon)
                .Include(k => k.Skocznia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (konkurs == null)
            {
                return NotFound();
            }

            return View(konkurs);
        }

        // GET: Konkurs/Create
        public IActionResult Create()
        {
            ViewData["SezonID"] = new SelectList(_context.Set<Sezon>(), "Id", "Id");
            ViewData["SkoczniaID"] = new SelectList(_context.Set<Skocznia>(), "Id", "Id");
            return View();
        }

        // POST: Konkurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkoczniaID,SezonID,Data,Opis")] Konkurs konkurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(konkurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SezonID"] = new SelectList(_context.Set<Sezon>(), "Id", "Id", konkurs.SezonID);
            ViewData["SkoczniaID"] = new SelectList(_context.Set<Skocznia>(), "Id", "Id", konkurs.SkoczniaID);
            return View(konkurs);
        }

        // GET: Konkurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Konkurs == null)
            {
                return NotFound();
            }

            var konkurs = await _context.Konkurs.FindAsync(id);
            if (konkurs == null)
            {
                return NotFound();
            }
            ViewData["SezonID"] = new SelectList(_context.Set<Sezon>(), "Id", "Id", konkurs.SezonID);
            ViewData["SkoczniaID"] = new SelectList(_context.Set<Skocznia>(), "Id", "Id", konkurs.SkoczniaID);
            return View(konkurs);
        }

        // POST: Konkurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SkoczniaID,SezonID,Data,Opis")] Konkurs konkurs)
        {
            if (id != konkurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konkurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonkursExists(konkurs.Id))
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
            ViewData["SezonID"] = new SelectList(_context.Set<Sezon>(), "Id", "Id", konkurs.SezonID);
            ViewData["SkoczniaID"] = new SelectList(_context.Set<Skocznia>(), "Id", "Id", konkurs.SkoczniaID);
            return View(konkurs);
        }

        // GET: Konkurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Konkurs == null)
            {
                return NotFound();
            }

            var konkurs = await _context.Konkurs
                .Include(k => k.Sezon)
                .Include(k => k.Skocznia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (konkurs == null)
            {
                return NotFound();
            }

            return View(konkurs);
        }

        // POST: Konkurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Konkurs == null)
            {
                return Problem("Entity set 'MvcSkokiContext.Konkurs'  is null.");
            }
            var konkurs = await _context.Konkurs.FindAsync(id);
            if (konkurs != null)
            {
                _context.Konkurs.Remove(konkurs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonkursExists(int id)
        {
          return (_context.Konkurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
