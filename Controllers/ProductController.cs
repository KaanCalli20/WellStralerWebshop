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
        private readonly IKlantRepository _klantRepo;
        private readonly IKlantLoginRepository _klantLoginsRepo;
        private readonly IOnlineBestelLijnRepository _onlineBestelLijn;
        public ProductController(IProductRepository productRepo,IKlantRepository klantRepo, IKlantLoginRepository klantLoginsRepo
            , IOnlineBestelLijnRepository onlineBestelLijn)
        {
            this._onlineBestelLijn = onlineBestelLijn;
            
            this._productRepo = productRepo;
            this._klantRepo = klantRepo;
            this._klantLoginsRepo = klantLoginsRepo;
        }

        public IActionResult Index()
        {
            //IEnumerable<OnlineBestelLijn> p = this._onlineBestelLijn.getOnlineBestelLijnen(); 
            IEnumerable<Product> lijstProducten = this._productRepo.getProducten();
            return View(lijstProducten);
        }

        
        public IActionResult Details(long Id)
        {
            Product prod = this._productRepo.getProductById(Id);
            return View(prod);
        }

        public IEnumerable<ProductKoppeling> getGekoppeldeProd(long Id)
        {
            IEnumerable<ProductKoppeling> gekopeldeProd = new List<>();
            return null;
        }
    }
}