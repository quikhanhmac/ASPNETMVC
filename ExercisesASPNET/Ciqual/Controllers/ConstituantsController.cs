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
    public class ConstituantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConstituantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Constituants
        public async Task<IActionResult> Index()
        {

            return View(await _context.Constituant.OrderBy(c=>c.Nom).ToListAsync());
        }
    }
}
