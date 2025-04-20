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

namespace MvcSkoki.Controllers{
public class RekordyController : Controller
{

    private const string SessionKeyLoggedIn = "_LoggedIn";
    private readonly MvcSkokiContext _context;
    public async Task<IActionResult> Index()
        {
                    if (HttpContext.Session.GetString(SessionKeyLoggedIn) != "true")
        {
            return RedirectToAction("Logowanie", "IO");
        }
            SQLiteConnection connection = new SQLiteConnection("Data Source=MvcSkoki.Data.db;Version=3;");
            connection.Open();

            List<List<object>> wyniki = new List<List<object>>();

            using (SQLiteCommand command = new SQLiteCommand("SELECT row_number() over (ORDER BY cast(Wynik as real) DESC) as Miejsce, SK.IMIE AS Imie, SK.NAZWISKO AS Nazwisko, SK.NARODOWOSC AS Narodowosc, P.WYNIK AS Wynik, S.MIEJSCOWOSC FROM PUNKTACJA P JOIN SKOCZEK SK ON P.SkoczekID = SK.Id JOIN KONKURS K ON P.KonkursID = K.Id JOIN SKOCZNIA S ON K.SkoczniaID = S.Id ORDER BY cast(Wynik as real) DESC;", connection))
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

            ViewData["Rekordy"] = wyniki;
            return View(wyniki);
        }
}

}
