using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nhibernate_Fluent.Models;
using Nhibernate_Fluent.Repository;

namespace Nhibernate_Fluent.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.getAll();
            return View();
        }

        
    }
}
