using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            this._productRepo = productRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> p = this._productRepo.getProducten(); 
            return View();
        }
    }
}