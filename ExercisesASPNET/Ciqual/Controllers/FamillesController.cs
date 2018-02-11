using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ciqual.Data;
using Ciqual.Models;

namespace Ciqual.Controllers
{
    public class FamillesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamillesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Familles
        public async Task<IActionResult> Index(string sort)
        {
            IQueryable<Famille> familles = _context.Famille;
            if (string.IsNullOrEmpty(sort)) sort = "Nom";
            if (sort.EndsWith("_desc"))
            {
                string prop = sort.Substring(0, sort.Length - 5);
                familles = familles.OrderByDescending(e => EF.Property<object>(e, prop));
            }
            else
                familles = familles.OrderBy(e => EF.Property<object>(e, sort));
            ViewData["TriParNom"] = sort == "Nom" ? "Nom_desc" : "Nom";

            return View(await familles.ToListAsync());
        }
    }
}
