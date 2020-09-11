using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IKlantRepository _klantRepo;
        private readonly IKlantLoginRepository _klantLoginsRepo;
        private readonly IOnlineBestelLijnRepository _onlineBestelLijn;

        public ProductController(IProductRepository productRepo, IKlantRepository klantRepo, IKlantLoginRepository klantLoginsRepo
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

            ProductDetailViewModel vm;
            Product prod = this._productRepo.getProductById(Id);
            if (!geselecteerdeProducten.Contains(prod))
            {
                geselecteerdeProducten.Add(prod);
            }
            vm = voegVasteProductenToe(geselecteerdeProducten, prod.gekoppeldProductenLijst(), 0);

            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Volgende(string selectedProds, List<long>? productId, ProductDetailViewModel vms)
        {
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            stringSelectedValues.RemoveAt(0);
            try
            {
                geselecteerdeProducten = wijzigSelectie(selectedProds, productId);
                index++;
            }
            catch (ArgumentException ex)
            {
                geselecteerdeProducten = new List<Product>();
                foreach (string stringID in stringSelectedValues)
                {
                    geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
                }
                TempData["error"] = ex.Message;
            }
            
            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index );

            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

            return View("Details", vm);
        }
        [HttpPost]
        public IActionResult Vorige(string selectedProds, List<long>? productId)
        {
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            stringSelectedValues.RemoveAt(0);
            try
            {
                geselecteerdeProducten = wijzigSelectie(selectedProds, productId);
                index--;
            }
            catch (ArgumentException ex)
            {
                geselecteerdeProducten = new List<Product>();
                foreach (string stringID in stringSelectedValues)
                {
                    geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
                }
                TempData["error"] = ex.Message;
            }
           
            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index );

            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

            return View("Details", vm);
        }

        public IActionResult Wijzig(string selectedProds, List<long>? productId)
        {
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            
            stringSelectedValues.RemoveAt(0);
            try
            {
                geselecteerdeProducten = wijzigSelectie(selectedProds, productId);
            }
            catch (ArgumentException ex)
            {
                geselecteerdeProducten = new List<Product>();
                foreach (string stringID in stringSelectedValues)
                {
                    geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
                }
                TempData["error"] = ex.Message;
            }

            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index);


            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

            return View("Details", vm);

        }

        public IActionResult PlaatsInWinkelmand(string selectedProds, List<long>? productId)
        {
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));

            List<OnlineBestelLijn> lijstOnlineBestelLijn = new List<OnlineBestelLijn>();

            stringSelectedValues.RemoveAt(0);
            try
            {
                geselecteerdeProducten = wijzigSelectie(selectedProds, productId);
            }
            catch (ArgumentException ex)
            {
                geselecteerdeProducten = new List<Product>();
                foreach (string stringID in stringSelectedValues)
                {
                    geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
                }
                TempData["error"] = ex.Message;

                hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
                vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index);


                TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
                //TempData["PrijsNaKorting"];

                TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

                return View("Details", vm);

            }
            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
            long BestellingId;
            KlantLogin klantLogin;
            Klant klant;
            Product product;
            int aantal;
            decimal prijs;
            decimal btwPerc;
            DateTime datumInbreng;
            long hoofdProdBestelLijnId;

            OnlineBestelLijn productt = new OnlineBestelLijn();



            return null;

        }

        private List<Product> wijzigSelectie(string selectedProds, List<long>? productId)
        {

            CheckVasteKeuze(selectedProds, productId);
            Product hoofdProduct;
            Product addProduct;

            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten = new List<Product>();
            List<ProductKoppeling> huidigeKoppelProductLijst;

            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            stringSelectedValues.RemoveAt(0);

            Product teVerwijderenProduct;

            foreach (string stringID in stringSelectedValues)
            {
                geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
            }

            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);

            huidigeKoppelProductLijst = hoofdProduct.gekoppeldProductenLijst().ElementAt(index);

            foreach (ProductKoppeling item in huidigeKoppelProductLijst)
            {
                teVerwijderenProduct = geselecteerdeProducten.SingleOrDefault(m => m.Id == item.GekoppeldProdId);
                if (teVerwijderenProduct != null && item.KoppelType.Id!=1)
                {
                    geselecteerdeProducten.Remove(teVerwijderenProduct);
                }
            }

            foreach (long item in productId)
            {
                if (item != 0)
                {
                    if (!geselecteerdeProducten.Select(p => p.Id).Contains(item))
                    {
                        addProduct = this._productRepo.getProductById(item);
                        geselecteerdeProducten.Add(addProduct);
                    }
                }

            }



            return geselecteerdeProducten;

        }

        public bool CheckVasteKeuze(string selectedProds, List<long>? productId)
        {
            Product hoofdProduct;
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten = new List<Product>();

            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            stringSelectedValues.RemoveAt(0);

            foreach (string stringID in stringSelectedValues)
            {
                geselecteerdeProducten.Add(_productRepo.getProductById(Convert.ToInt64(stringID)));
            }

            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);

            if (hoofdProduct.gekoppeldProductenLijst().ElementAt(index).ElementAt(0).KoppelType.Id == 3 && productId.Sum() == 0)
            {
                throw new ArgumentException("Dit is een verplichte keuze, gelieve uw keuze in te geven");
            }
            else
            {
                return true;
            }

        }

        public ProductDetailViewModel voegVasteProductenToe(List<Product> geselecteerdeProducten, List<List<ProductKoppeling>> productKoppelingen, int index)
        {
            ProductDetailViewModel vm;

            foreach (List<ProductKoppeling> lijst in productKoppelingen)
            {
                if (lijst.ElementAt(0).KoppelType.Id == 1)
                {
                    foreach (ProductKoppeling productKoppeling in lijst)
                    {
                        geselecteerdeProducten.Add(productKoppeling.GekoppeldProduct);
                    }
                }
            }
            vm = new ProductDetailViewModel(geselecteerdeProducten, productKoppelingen, index);
            return vm;

        }

        private Decimal geefPrijs(List<Product> geselecteerdeProducten)
        {
            Product productVoorPrijs;
            Decimal prijs = 0;
            //For moet vanaf 1 beginnen en de prijs van het hoofdproduct moet alvorens bijgerekend worden.
            for (int i = 0; i < geselecteerdeProducten.Count; i++)
            {
                productVoorPrijs = geselecteerdeProducten.ElementAt(i);
                prijs = prijs +  productVoorPrijs.Prijs;
            }

            return prijs;
        }


    }
}