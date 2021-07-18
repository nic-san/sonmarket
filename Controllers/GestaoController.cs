using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sonmarket.Data;
using sonmarket.DTO;
using System.Linq;
using sonmarket.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace sonmarket.Controllers
{
    [Authorize]
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext database;
        public GestaoController(ApplicationDbContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categorias()
        {
            var categorias = database.Categorias.Where(cat => cat.Status == true).ToList();
            return View(categorias);
        }

        public IActionResult NovaCategoria()
        {
            return View();
        }

        public IActionResult EditarCategoria(int id)
        {
            var categoria = database.Categorias.First(cat => cat.Id == id);
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }

        public IActionResult Fornecedores()
        {
            var fornecedores = database.Fornecedores.Where(forne => forne.Status == true).ToList();
            return View(fornecedores);
        }

        public IActionResult NovoFornecedor()
        {
            return View();
        }

        public IActionResult EditarFornecedor(int id)
        {
            var fornecedor = database.Fornecedores.First(forne => forne.Id == id);
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
        }

        public IActionResult Produtos()
        {
            var produtos = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).Where(p => p.Status == true).ToList();
            return View(produtos);
        }

        public IActionResult NovoProduto()
        {
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View();
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = database.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id == id);
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCustoString = produto.PrecoDeCusto.ToString(CultureInfo.InvariantCulture.NumberFormat); 
            produtoView.PrecoDeVendaString = produto.PrecoDeVenda.ToString(CultureInfo.InvariantCulture.NumberFormat);
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;
            produtoView.Medicao = produto.Medicao;
            ViewBag.Categorias = database.Categorias.ToList();
            ViewBag.Fornecedores = database.Fornecedores.ToList();
            return View(produtoView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = database.Promocoes.Include(p => p.Produto).Where(i => i.Status == true).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = database.Promocoes.Include(p => p.Produto).First(p => p.Id == id);
            PromocaoDTO promocaoView = new PromocaoDTO();
            promocaoView.Id = promocao.Id;
            promocaoView.Nome = promocao.Nome;
            promocaoView.ProdutoID = promocao.Produto.Id;
            promocaoView.Porcentagem = promocao.Porcentagem;
            ViewBag.Produtos = database.Produtos.ToList();
            return View(promocaoView);
        }

        public IActionResult Estoque()
        {
            var listaDeEstoque = database.Estoques.Include(e => e.Produto).ToList();
            return View(listaDeEstoque);
        }

        public IActionResult NovoEstoque()
        {
            ViewBag.Produtos = database.Produtos.ToList();
            return View();
        }

        public IActionResult EditarEstoque()
        {
            return View();
        }

        public IActionResult Vendas()
        {
            var listaDeVendas = database.Vendas.ToList();
            return View(listaDeVendas);
        }

        [HttpPost]
        public IActionResult RelatorioDeVendas()
        {
            return Ok(database.Vendas.ToList());
        }
    }
}