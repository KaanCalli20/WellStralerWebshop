using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Transactions;
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
        private readonly IOnlineBestelLijnRepository _onlineBestelLijnRepo;
        private readonly IStringLocalizer<ProductController> _localizer;

        public ProductController(IProductRepository productRepo, IKlantRepository klantRepo, IKlantLoginRepository klantLoginsRepo
            , IOnlineBestelLijnRepository onlineBestelLijn, IStringLocalizer<ProductController> localizer)
        {
            this._onlineBestelLijnRepo = onlineBestelLijn;

            this._productRepo = productRepo;
            this._klantRepo = klantRepo;
            this._klantLoginsRepo = klantLoginsRepo;

            this._localizer = localizer;
            //Test commit
        }

        public ViewResult Index()
        {
            ApplyLanguage();
            IEnumerable<Product> lijstProducten = new List<Product>();
            lijstProducten = _productRepo.getProducten();

            return View(lijstProducten);
        }

        [HttpPost]
        public ViewResult Index(string SearchString)
        {
            ApplyLanguage();
            IEnumerable<Product> lijstProducten = new List<Product>();
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;


            if (SearchString != null)
            {
                try
                {
                    lijstProducten = _productRepo.getProductenByTaalOmschrijving(taal, SearchString.ToUpper());
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
            ApplyLanguage();
            List<Product> geselecteerdeProducten = new List<Product>();

            ProductDetailViewModel vm;
            Product prod = this._productRepo.getProductById(Id);
            if (!geselecteerdeProducten.Contains(prod))
            {
                geselecteerdeProducten.Add(prod);
            }
            vm = voegVasteProductenToe(geselecteerdeProducten, prod.gekoppeldProductenLijst(), 0);

            ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
                for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Volgende(string selectedProds, List<long>? productId,  string? quantity)
        {
            ApplyLanguage();
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            int aantal = Convert.ToInt32(quantity);
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index ,aantal);

            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten)*aantal;
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);
        }
        [HttpPost]
        public IActionResult Vorige(string selectedProds, List<long>? productId, string? quantity)
        {
            ApplyLanguage();
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            int aantal = Convert.ToInt32(quantity);
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index ,aantal);

            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);
        }

        public IActionResult Wijzig(string selectedProds, List<long>? productId, string? quantity)
        {
            ApplyLanguage();
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            int aantal = Convert.ToInt32(quantity);
            
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal) ;


            TempData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
            //TempData["PrijsNaKorting"];

            TempData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);

        }

        public IActionResult PlaatsInWinkelmand(string selectedProds, List<long>? productId, string? quantity)
        {
            List<string> stringSelectedValues = selectedProds.Split(",").ToList();
            List<Product> geselecteerdeProducten;
            Product hoofdProduct;
            ProductDetailViewModel vm;
            int index = Convert.ToInt32(stringSelectedValues.ElementAt(0));
            int aantal = Convert.ToInt32(quantity);
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
                vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index,aantal);


                ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
                //TempData["PrijsNaKorting"];

                ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

                return View("Details", vm);

            }
            try
            {
                if (aantal < 1)
                {
                    throw new ArgumentException("Gelieve een getal hoger dan 0 te geven");
                }
            }
            catch(ArgumentException ex)
            {
               
                TempData["error"] = ex.Message;

                hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
                vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal);


                ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten)*aantal;
                //TempData["PrijsNaKorting"];

                ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

                return View("Details", vm);
            }
            
            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);

            geselecteerdeProducten.RemoveAt(0);

            //get klant en klantLogin
            string idKlantLogin = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            KlantLogin klantLogin = _klantLoginsRepo.getLoginByLoginID(Convert.ToInt64(idKlantLogin));
            Klant klant = klantLogin.Klant;

            OnlineBestelLijn onlineBestelLijn = new OnlineBestelLijn(0,klantLogin,klant,hoofdProduct,aantal,hoofdProduct.Prijs,hoofdProduct.BtwPerc,DateTime.Now,0);
            _onlineBestelLijnRepo.voegOnlineBestelLijnToe(onlineBestelLijn);
            _onlineBestelLijnRepo.SaveChanges();
            if (geselecteerdeProducten.Count()==0) { }
            else{
                foreach (Product koppelProduct in geselecteerdeProducten)
                {
                    lijstOnlineBestelLijn.Add(new OnlineBestelLijn(0, klantLogin, klant, koppelProduct, aantal, koppelProduct.Prijs, koppelProduct.BtwPerc, DateTime.Now, onlineBestelLijn.Id));
                }
                _onlineBestelLijnRepo.voegOnlineBestelLijnenToe(lijstOnlineBestelLijn);

                _onlineBestelLijnRepo.SaveChanges();
            }
            TempData["message"] = "Product succesvol geplaatst in het winkelmand";
            return RedirectToAction("Index", "Order");

        }
        //-------------------------------------------------------Methoden----------------------------------------
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

            if (hoofdProduct.productKoppelingen == null || hoofdProduct.productKoppelingen.Count() == 0)
            {

            }
            else
            {

                huidigeKoppelProductLijst = hoofdProduct.gekoppeldProductenLijst().ElementAt(index);

                foreach (ProductKoppeling item in huidigeKoppelProductLijst)
                {
                    teVerwijderenProduct = geselecteerdeProducten.SingleOrDefault(m => m.Id == item.GekoppeldProdId);
                    if (teVerwijderenProduct != null && item.KoppelType.Id != 1)
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
            if (hoofdProduct.productKoppelingen == null||hoofdProduct.productKoppelingen.Count() ==0)
            {
                return true;
            }
            else
            {
                if (hoofdProduct.gekoppeldProductenLijst().ElementAt(index).ElementAt(0).KoppelType.Id == 3 && productId.Sum() == 0)
                {
                    throw new ArgumentException("Dit is een verplichte keuze, gelieve uw keuze in te geven");
                }
                else
                {
                    return true;
                }
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, productKoppelingen, index,1);
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) }
                );

            return LocalRedirect(returnUrl);
        }

        private void ApplyLanguage()
        {
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;

            ViewData["Taal"] = taal;

            ViewData["Description"] = _localizer["Description"];
            ViewData["Id"] = _localizer["Id"];
            ViewData["Price"] = _localizer["Price"];
            ViewData["Order"] = _localizer["Order"];
            ViewData["Filter"] = _localizer["Filter"];
            ViewData["Search"] = _localizer["Search"];

            ViewData["Products"] = _localizer["Products"];
            ViewData["Orders"] = _localizer["Orders"];
            ViewData["Login"] = _localizer["Login"];
            ViewData["Logout"] = _localizer["Logout"];
 
        }
    }
}