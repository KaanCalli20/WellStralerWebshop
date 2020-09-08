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
        public ProductController(IProductRepository productRepo,IKlantRepository klantRepo, IKlantLoginRepository klantLoginsRepo)
        {
            
            this._productRepo = productRepo;
            this._klantRepo = klantRepo;
            this._klantLoginsRepo = klantLoginsRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<KlantLogin> p = this._klantLoginsRepo.getLogins(); 
            return View();
        }
    }
}