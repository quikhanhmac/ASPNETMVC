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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Famille.ToListAsync());
        }
    }
}
