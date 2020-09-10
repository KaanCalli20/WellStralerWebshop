using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IKlantRepository _klantRepo;
        private readonly IKlantLoginRepository _klantLoginsRepo;
        private readonly IOnlineBestelLijnRepository _onlineBestelLijn;
        
        private int index = 0;
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
            List<Product> geselecteerdeProducten = new List<Product>();
            index = 0;
            geselecteerdeProducten = new List<Product>();
            ProductDetailViewModel vm;
            Product prod = this._productRepo.getProductById(Id);
            if (!geselecteerdeProducten.Contains(prod))
            {
                geselecteerdeProducten.Add(prod);
            }
            ViewData["geselecteerdeProducten"] = geselecteerdeProducten;
            Product prod2 = this._productRepo.getProductById(2741);
            geselecteerdeProducten.Add(prod2);
            
            vm = new ProductDetailViewModel(geselecteerdeProducten, prod.lijstBijProducten(),0) ;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Volgende(string selectedProds, List<long>? productId, ProductDetailViewModel vms)
        {
            Request.Scheme.Count();
            Product hoofdProduct;
            Product addProduct;
            ProductDetailViewModel vm;
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten = new List<Product>();

            foreach (string stringID in stringSelectedValues)
            {
                geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
            }
            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
            foreach (long item in productId)
	        {
                if (geselecteerdeProducten.FirstOrDefault(p => p.Id == item) == null)
                {
                    addProduct = this._productRepo.getProductById(item);
                    geselecteerdeProducten.Add(addProduct);
                    hoofdProduct.productKoppelingen.IndexOf();
                }
            }
           
            
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.lijstBijProducten(), index+1);


            return View("Details",vm);
        }
        [HttpPost]
        public IActionResult Vorige(List<long>? productId, List<long> selectedProds)
        {

            Console.WriteLine();

            return View("Details");
        }




    }
}