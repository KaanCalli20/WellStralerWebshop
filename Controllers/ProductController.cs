﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Transactions;
using WellStralerWebshop.Filters;
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
        private readonly IParameterRepository _parameterRepo;

        private readonly IStringLocalizer<ProductController> _localizer;

        public ProductController(IProductRepository productRepo, IKlantRepository klantRepo, IKlantLoginRepository klantLoginsRepo
            , IOnlineBestelLijnRepository onlineBestelLijn, IParameterRepository parameterRepo, IStringLocalizer<ProductController> localizer)
        {
            this._onlineBestelLijnRepo = onlineBestelLijn;

            this._productRepo = productRepo;
            this._klantRepo = klantRepo;
            this._klantLoginsRepo = klantLoginsRepo;
            this._parameterRepo = parameterRepo;
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
        [ServiceFilter(typeof(KlantFilter))]

        public IActionResult Details(long Id, KlantLogin klantLogin)
        {
            ApplyLanguage();
            List<Product> geselecteerdeProducten = new List<Product>();

            ProductDetailViewModel vm;
            Product prod = this._productRepo.getProductById(Id);
            if (!geselecteerdeProducten.Contains(prod))
            {
                geselecteerdeProducten.Add(prod);
            }
            vm = voegVasteProductenToe(geselecteerdeProducten, prod.gekoppeldProductenLijst(), 0, klantLogin);

            ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten);
            ViewData["PrijsNaKorting"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin);

            ViewData["Stock"] = geefStockWaarde(geselecteerdeProducten.ElementAt(0));
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);

            return View(vm);
        }

        [ServiceFilter(typeof(KlantFilter))]

        [HttpPost]
        public IActionResult Volgende(string selectedProds, List<long>? productId, string? quantity, KlantLogin klantLogin)
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal, klantLogin.Klant);

            ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
            ViewData["PrijsNaKorting"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin) * aantal;

            ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]

        public IActionResult Vorige(string selectedProds, List<long>? productId, string? quantity, KlantLogin klantLogin)
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal, klantLogin.Klant);

            ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
            ViewData["PrijsNaKorting"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin) * aantal;

            ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);
        }
        [ServiceFilter(typeof(KlantFilter))]

        public IActionResult Wijzig(string selectedProds, List<long>? productId, string? quantity, KlantLogin klantLogin)
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal, klantLogin.Klant);


            ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
            ViewData["PrijsNaKorting"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin) * aantal;

            ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;
            List<int> lijst = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                lijst.Add(i);
            }
            ViewData["aantal"] = new SelectList(lijst);
            return View("Details", vm);

        }
        [ServiceFilter(typeof(KlantFilter))]

        public IActionResult PlaatsInWinkelmand(string selectedProds, List<long>? productId, string? quantity, KlantLogin klantLogin)
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
                vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal, klantLogin.Klant);


                ViewData["NormalePrijs"] = geefPrijs(geselecteerdeProducten) * aantal;
                ViewData["PrijsNaKorting"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin) * aantal;

                /// ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

                return View("Details", vm);

            }
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;
            try
            {
                if (aantal < 1)
                {

                    if (taal == "en")
                    {
                        throw new ArgumentException("Please enter a number higher than 0");
                    }
                    else if (taal == "fr")
                    {
                        throw new ArgumentException("Veuillez saisir un nombre supérieur à 0");

                    }
                    else
                    {
                        throw new ArgumentException("Gelieve een getal hoger dan 0 te geven");
                    }
                }
            }
            catch (ArgumentException ex)
            {

                TempData["error"] = ex.Message;

                hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);
                vm = new ProductDetailViewModel(geselecteerdeProducten, hoofdProduct.gekoppeldProductenLijst(), index, aantal, klantLogin.Klant);


                ViewData["NormalePrijs"] = geefPrijsNaKorting(geselecteerdeProducten, klantLogin) * aantal;
                //TempData["PrijsNaKorting"];

                ViewData["Stock"] = geselecteerdeProducten.ElementAt(0).Stock;

                return View("Details", vm);
            }

            hoofdProduct = this._productRepo.getProductById(geselecteerdeProducten.ElementAt(0).Id);

            geselecteerdeProducten.RemoveAt(0);

            //get klant en klantLogin
            Klant klant = klantLogin.Klant;

            decimal prijs = 0;

            if (hoofdProduct.Typekorting == 1)
            {
                prijs = hoofdProduct.Prijs - hoofdProduct.Prijs * Convert.ToDecimal(klantLogin.Klant.Korting1) / 100;
            }
            else if (hoofdProduct.Typekorting == 2)
            {
                prijs = hoofdProduct.Prijs - hoofdProduct.Prijs * Convert.ToDecimal(klantLogin.Klant.Korting2) / 100;
            }
            OnlineBestelLijn onlineBestelLijn = new OnlineBestelLijn(0, klantLogin, klant, hoofdProduct, aantal, prijs, hoofdProduct.BtwPerc, DateTime.Now, 0);
            _onlineBestelLijnRepo.voegOnlineBestelLijnToe(onlineBestelLijn);
            _onlineBestelLijnRepo.SaveChanges();
            if (geselecteerdeProducten.Count() == 0) { }
            else
            {
                foreach (Product koppelProduct in geselecteerdeProducten)
                {
                    if (koppelProduct.Typekorting == 1)
                    {
                        prijs = koppelProduct.PrijsGekoppeld - koppelProduct.PrijsGekoppeld * Convert.ToDecimal(klantLogin.Klant.Korting1) / 100;
                    }
                    else if (koppelProduct.Typekorting == 2)
                    {
                        prijs = koppelProduct.PrijsGekoppeld - koppelProduct.PrijsGekoppeld * Convert.ToDecimal(klantLogin.Klant.Korting2) / 100;
                    }
                    lijstOnlineBestelLijn.Add(new OnlineBestelLijn(0, klantLogin, klant, koppelProduct, aantal, prijs, koppelProduct.BtwPerc, DateTime.Now, onlineBestelLijn.Id));
                }
                _onlineBestelLijnRepo.voegOnlineBestelLijnenToe(lijstOnlineBestelLijn);

                _onlineBestelLijnRepo.SaveChanges();
            }

            if (taal == "en")
            {
                TempData["message"] = "Succesful Add To Cart";
            }
            else if (taal == "fr")
            {
                TempData["message"] = "Produit placé avec succès dans le panier";

            }
            else
            {
                TempData["message"] = "Product succesvol geplaatst in het winkelwagen";
            }
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
            if (hoofdProduct.productKoppelingen == null || hoofdProduct.productKoppelingen.Count() == 0)
            {
                return true;
            }
            else
            {
                if (hoofdProduct.gekoppeldProductenLijst().ElementAt(index).ElementAt(0).KoppelType.Id == 3 && productId.Sum() == 0)
                {
                    var request = HttpContext.Features.Get<IRequestCultureFeature>();
                    string taal = request.RequestCulture.Culture.Name;
                    if (taal == "en")
                    {
                        throw new ArgumentException("This is a mandatory choice, please enter your choice");
                    }
                    else if (taal == "fr")
                    {
                        throw new ArgumentException("Ceci est un choix obligatoire, veuillez entrer votre choix");

                    }
                    else
                    {
                        throw new ArgumentException("Dit is een verplichte keuze, gelieve uw keuze in te geven");
                    }
                }
                else
                {
                    return true;
                }
            }



        }

        public ProductDetailViewModel voegVasteProductenToe(List<Product> geselecteerdeProducten, List<List<ProductKoppeling>> productKoppelingen, int index, KlantLogin klantLogin)
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
            vm = new ProductDetailViewModel(geselecteerdeProducten, productKoppelingen, index, 1, klantLogin.Klant);
            return vm;

        }

        private Decimal geefPrijsNaKorting(List<Product> geselecteerdeProducten, KlantLogin klantLogin)
        {
            Decimal prijs = 0;
            Product product = geselecteerdeProducten.ElementAt(0);
            if (product.Typekorting == 1)
            {
                prijs = product.Prijs - product.Prijs * Convert.ToDecimal(klantLogin.Klant.Korting1) / 100;
            }
            else if (product.Typekorting == 2)
            {
                prijs = product.Prijs - product.Prijs * Convert.ToDecimal(klantLogin.Klant.Korting2) / 100;
            }

            for (int i = 1; i < geselecteerdeProducten.Count; i++)
            {
                product = geselecteerdeProducten.ElementAt(i);
                if (product.Typekorting == 1)
                {
                    prijs = prijs + product.PrijsGekoppeld - product.PrijsGekoppeld * Convert.ToDecimal(klantLogin.Klant.Korting1) / 100;
                }
                else if (product.Typekorting == 2)
                {
                    prijs = prijs + product.PrijsGekoppeld - product.PrijsGekoppeld * Convert.ToDecimal(klantLogin.Klant.Korting2) / 100;
                }

            }

            return prijs;
        }

        private Decimal geefPrijs(List<Product> geselecteerdeProducten)
        {
            Decimal prijs = 0;
            prijs = geselecteerdeProducten.ElementAt(0).Prijs;
            for (int i = 1; i < geselecteerdeProducten.Count; i++)
            {
                prijs = prijs + geselecteerdeProducten.ElementAt(i).PrijsGekoppeld;

            }
            return prijs;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(365) }
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
            ViewData["PriceWithReduction"] = _localizer["PriceWithReduction"];
            ViewData["Order"] = _localizer["Order"];
            ViewData["Filter"] = _localizer["Filter"];
            ViewData["Search"] = _localizer["Search"];
            ViewData["Quantity"] = _localizer["Quantity"];
            ViewData["Add to cart"] = _localizer["Add to cart"];
            ViewData["Previous"] = _localizer["Previous"];
            ViewData["Change"] = _localizer["Change"];
            ViewData["Next"] = _localizer["Next"];
            ViewData["Back"] = _localizer["Back"];

            ViewData["Products"] = _localizer["Products"];
            ViewData["Orders"] = _localizer["Orders"];
            ViewData["Login"] = _localizer["Login"];
            ViewData["Logout"] = _localizer["Logout"];
            ViewData["Invoices"] = _localizer["Invoices"];
            ViewData["Cart"] = _localizer["Cart"];
            ViewData["Settings"] = _localizer["Settings"];
        }
        private string geefStockWaarde(Product product)
        {
            List<Parameters> parameters = _parameterRepo.GetParameters().Where(m => m.ParameterTable.Contains("stock")).ToList();

            int? kleinerDan = 0;
            int? groterDan = 0;
            int? gelijkAan = 0;
            int? waarde = 0;
            string response = "";
            Parameters item;
            for (int i = parameters.Count - 1; i >= 0; i--)
            {
                item = parameters.ElementAt(i);
                kleinerDan = item.ParameterKleinerDan;
                groterDan = item.ParameterGroterDan;
                gelijkAan = item.ParameterGelijkAan;
                waarde = item.ParameterWaarde;
                if (item.ParameterKleinerDan == 1)
                {
                    if (item.ParameterGelijkAan == 1)
                    {
                        response = product.Stock <= waarde ? item.ParameterBeschrijving1 : "";
                        break;
                    }
                }
                else if (item.ParameterGroterDan == 1)
                {
                    if (item.ParameterGelijkAan == 1)
                    {
                        response = product.Stock >= waarde ? item.ParameterBeschrijving1 : "";
                        break;
                    }
                }
                else if (item.ParameterGelijkAan == 1)
                {

                    response = product.Stock == waarde ? item.ParameterBeschrijving1 : "";
                    break;
                }
            }
            return response;
        }
    }
}