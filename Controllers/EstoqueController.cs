using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sonmarket.Data;
using sonmarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sonmarket.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext database;

        public EstoqueController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemp)
        {
            database.Estoques.Add(estoqueTemp);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}
