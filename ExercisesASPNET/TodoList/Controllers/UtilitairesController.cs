using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class UtilitairesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AjouterJours(Calcul calcul)
        {
            //ModelState.Remove("Result");
            //calcul.Result = calcul.DateDeb.AddDays(calcul.Nbj);
            if (calcul.Operateur == "Addition")
            {
                ViewBag.Result = calcul.DateDeb.AddDays(calcul.Nbj);
            }
            else ViewBag.Result = calcul.DateDeb.AddDays(-calcul.Nbj);
            return View("Index", calcul);
        }
    }
}