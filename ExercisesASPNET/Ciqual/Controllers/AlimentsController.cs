using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ciqual.Data;
using Ciqual.Models;
using System.Data.SqlClient;
using System.Data;

namespace Ciqual.Controllers
{
    public class AlimentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlimentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aliments
        public async Task<IActionResult> Index(string FSelect)
        {
            //var vmAliment = new AlimentsVM();
            ////recuperer les familles triées par nom
            //vmAliment.Familles = await _context.Famille.OrderBy(a => a.Nom).AsNoTracking().ToListAsync();
            ////si aucune famille est selectionnée, prendre la premiere
            //if (string.IsNullOrEmpty(FSelect))
            //    FSelect = _context.Famille.FirstOrDefault().CodeFamille;
            ////recuperer les aliments de la famille triée
            //vmAliment.Aliments = await _context.Aliment.Where(a=>a.CodeFamille== FSelect).OrderBy(a => a.Nom).Include(a=>a.Composition).AsNoTracking().ToListAsync();
            //          return View(vmAliment);
            // Requête SQL optimisée : on ramène uniquement les infos nécessaires
            var vmAliment = new AlimentsVM();
            //recuperer les familles triées par nom
            vmAliment.Familles = await _context.Famille.OrderBy(a => a.Nom).AsNoTracking().ToListAsync();
            //si aucune famille est selectionnée, prendre la premiere
            if (string.IsNullOrEmpty(FSelect))
                FSelect = _context.Famille.FirstOrDefault().CodeFamille;
            vmAliment.FSelect = FSelect;
            vmAliment.Aliments = new List<Aliment>();
            string req = @"select a.Nom, count(*) NbConstiuants
from Aliment a
inner join Composition c on c.IdAliment=a.IdAliment
where a.CodeFamille=@FSelect
group by a.Nom
order by a.Nom
";
            using (var conn = (SqlConnection)_context.Database.GetDbConnection())
            {
                var cmd = new SqlCommand(req,conn);
                var param = new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@FSelect",
                    Value = FSelect
                };
                cmd.Parameters.Add(param);
                await conn.OpenAsync();

                using (var sdr = await cmd.ExecuteReaderAsync())
                {
                    while (sdr.Read())
                    {
                        var a = new Aliment();
                        //a.IdAliment=(int)sdr["IdAliment"];
                        a.Nom = (string)sdr["Nom"];
                        //a.CodeFamille = (string)sdr["CodeFamille"];
                        //a.NbConstituants = (int)sdr["NbConstituants"];
                        vmAliment.Aliments.Add(a);
                    }
                }
            }

            return View(vmAliment);

        }
        public async Task<IActionResult> ListByFirstLetter(char id, int page = 1)
        {
            //var aliments=await _context.Aliment.Where(a=>a.Nom[0]==id).OrderBy(a=>a.Nom)
            //    .Include(a=>a.CodeFamilleNavigation).AsNoTracking().ToListAsync();
            var aliments = await PageItems<Aliment>.CreateAsync(
      _context.Aliment.Where(a => a.Nom[0] == id).OrderBy(a => a.Nom)
                .Include(a => a.CodeFamilleNavigation).AsNoTracking(), page, 20);

            return View(aliments);
        }

        // GET: Aliments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment.Include(a => a.CodeFamilleNavigation)
                .Include(a => a.Composition).ThenInclude(c => c.IdConstituantNavigation)
                .SingleOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }

            return View(aliment);
        }

        // GET: Aliments/Create
        public IActionResult Create()
        {
            ViewData["CodeFamille"] = new SelectList(_context.Famille, "CodeFamille", "CodeFamille");
            return View();
        }

        // POST: Aliments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAliment,Nom,CodeFamille")] Aliment aliment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aliment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodeFamille"] = new SelectList(_context.Famille, "CodeFamille", "CodeFamille", aliment.CodeFamille);
            return View(aliment);
        }

        // GET: Aliments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment.SingleOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }
            ViewData["CodeFamille"] = new SelectList(_context.Famille, "CodeFamille", "CodeFamille", aliment.CodeFamille);
            return View(aliment);
        }

        // POST: Aliments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAliment,Nom,CodeFamille")] Aliment aliment)
        {
            if (id != aliment.IdAliment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aliment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlimentExists(aliment.IdAliment))
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
            ViewData["CodeFamille"] = new SelectList(_context.Famille, "CodeFamille", "CodeFamille", aliment.CodeFamille);
            return View(aliment);
        }

        // GET: Aliments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aliment = await _context.Aliment
                .Include(a => a.CodeFamilleNavigation)
                .SingleOrDefaultAsync(m => m.IdAliment == id);
            if (aliment == null)
            {
                return NotFound();
            }

            return View(aliment);
        }

        // POST: Aliments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aliment = await _context.Aliment.SingleOrDefaultAsync(m => m.IdAliment == id);
            _context.Aliment.Remove(aliment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlimentExists(int id)
        {
            return _context.Aliment.Any(e => e.IdAliment == id);
        }
    }
}
