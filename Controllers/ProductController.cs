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

        
        public ViewResult Index()
        {
            IEnumerable<Product> lijstProducten = new List<Product>();
            lijstProducten = _productRepo.getProducten();
            return View(lijstProducten);
        }

        [HttpPost]
        public ViewResult Index(string SearchString)
        {
            IEnumerable<Product> lijstProducten = new List<Product>();

            if (SearchString != null)
            {
                try
                {
                    lijstProducten = _productRepo.getProductenByOmschrijving(SearchString.ToUpper());
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                lijstProducten = _productRepo.getProducten();
            }

            return View(lijstProducten);
        }

        
        public IActionResult Details(long Id)
        {
            Product prod = this._productRepo.getProductById(Id);
            return View(prod);
        }

        
    }
}