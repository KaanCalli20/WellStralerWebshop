using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IEnumerable<OnlineBestelLijn> winkelMandje;
        private readonly IOnlineBestelLijnRepository _onlineBestelLijnRepository;
        private readonly IKlantLoginRepository _klantLoginRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly IKlantRepository _klantRepository;
        private readonly IOnlineBestellingRepository _onlineBestellingRepository;
        private readonly IBestelLijnRepository _bestelLijnRepo;
        private readonly IBestellingRepository _bestellingRepo;
        private readonly IStringLocalizer<OrderController> _localizer;


        public OrderController(IOnlineBestelLijnRepository onlineBestelLijnRepository
            , IKlantLoginRepository klantLoginRepo,ITransportRepository transportRepository,
            IKlantRepository klantRepo, IOnlineBestellingRepository onlineBestellingRepository,
            IBestelLijnRepository bestellijnRepo, IBestellingRepository bestellingRepo,IStringLocalizer<OrderController> localizer)
        {
            this._onlineBestelLijnRepository = onlineBestelLijnRepository;
            this._klantLoginRepository = klantLoginRepo;
            this._transportRepository = transportRepository;
            this._onlineBestellingRepository = onlineBestellingRepository;
            this._bestelLijnRepo = bestellijnRepo;
            this._bestellingRepo = bestellingRepo;
            this._klantRepository = klantRepo;

            this._localizer = localizer;
        }
       
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(KlantLogin klantLogin)
        {


            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin);
            ApplyLanguage();
            totaalPrijs();
            return View(winkelMandje);
        }

        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Remove(long id, KlantLogin klantLogin)
        {
            ApplyLanguage();
            OnlineBestelLijn teVerwijderenBestelLijn = _onlineBestelLijnRepository.getOnlineBestellijn(id);
            IEnumerable<OnlineBestelLijn> lijstBestellijnen;
            if (teVerwijderenBestelLijn.HoofdProdBestelLijnId == 0)
            {
                lijstBestellijnen = _onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin)
                    .Where(p => p.HoofdProdBestelLijnId == teVerwijderenBestelLijn.Id);
                _onlineBestelLijnRepository.verwijderOBestelLijn(teVerwijderenBestelLijn);
                if (lijstBestellijnen.Count() > 0) 
                {
                    foreach (OnlineBestelLijn item in lijstBestellijnen)
                    {
                        _onlineBestelLijnRepository.verwijderOBestelLijn(item);
                    }
                }
                

                _onlineBestelLijnRepository.SaveChanges();
                TempData["message"] = "Artikels succesvol verwijderd";
            }
            else
            {

            }
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin);
            totaalPrijs();
            return View("Index", winkelMandje);
        }

        [ServiceFilter(typeof(KlantFilter))]
        [HttpGet]
        public IActionResult MaakOrder(string id,KlantLogin klantLogin)
        {
            ApplyLanguage();
            List<Transport> lijstTransportTypes = _transportRepository.getAllTransport();
            List<Klant> leverKlanten= GetLijstLeverklanten(klantLogin);

            MaakOrderViewModel vm = new MaakOrderViewModel(leverKlanten, leverKlanten.ElementAt(0),"","",lijstTransportTypes,lijstTransportTypes.ElementAt(0));           
            
             return View(vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult MaakOrder( string referentie, string opmerking, short transportTypeId, long leverKlantId, KlantLogin klantLogin)
        {
            ApplyLanguage();
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;
            try
            {
                if (leverKlantId == 0)
                {
                    if (taal == "en")
                    {
                        throw new ArgumentException("Please choose a delivery address");
                    }
                    else if (taal == "fr")
                    {
                        throw new ArgumentException("Veuillez choisir une adresse de livraison");
                    }
                    else
                    {
                        throw new ArgumentException("Gelieve een leveradres te kiezen");
                    }
                }
                if (transportTypeId == 0)
                {
                    if (taal == "en")
                    {
                        throw new ArgumentException("Please choose a transport Type");
                    }
                    else if (taal == "fr")
                    {
                        throw new ArgumentException("Veuillez choisir un type de transport");
                    }
                    else
                    {
                        throw new ArgumentException("Gelieve een transportType te kiezen");
                    }
                }
                Klant leverKlant = this._klantRepository.getKlant(leverKlantId);
                Transport transport = this._transportRepository.getTransportById(transportTypeId);
                List<OnlineBestelLijn> lijstBestelLijnen= this._onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin);
                OnlineBestelling bestelling = new OnlineBestelling(klantLogin.Klant, leverKlant, referentie, opmerking, transport, klantLogin, lijstBestelLijnen);
                this._onlineBestellingRepository.voegOnlineBestellingToe(bestelling);
                this._onlineBestellingRepository.SaveChanges();
                TempData["message"] = "Bestelling succesvol geplaatst";
                if (taal == "en")
                {
                    TempData["message"] = "Order placed successfully";
                }
                else if (taal == "fr")
                {
                    TempData["message"] = "Commande passée avec succès";

                }
                else
                {
                    TempData["message"] = "Bestelling succesvol geplaatst";
                }
                return RedirectToAction("Index", "Product");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                List<Transport> lijstTransportTypes = _transportRepository.getAllTransport();
                List<Klant> leverKlanten = GetLijstLeverklanten(klantLogin);
                MaakOrderViewModel vm = new MaakOrderViewModel(leverKlanten,leverKlanten.ElementAt(0),referentie,opmerking,lijstTransportTypes,lijstTransportTypes.ElementAt(0));
                return View("MaakOrder",vm);
            }
        }
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult GeefOrders(KlantLogin klantLogin)
        {
            ApplyLanguage();
            List<Bestelling> bestellingen = _bestellingRepo.getBestellingen(klantLogin);
            IEnumerable<OnlineBestelling> onlinebestellingen = _onlineBestellingRepository.getOnlineBestellingen(klantLogin);
            BestellingViewModel vm = new BestellingViewModel(null,onlinebestellingen);

            return View(vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult GeefOrders(KlantLogin klantLogin, string? productNaam,DateTime? vanDatum,DateTime? totDatum
            ,string? leverAdres,byte? geleverd)
        {
            if (productNaam == null && vanDatum == null && totDatum == null  &&
            leverAdres == null && geleverd == null){
                return RedirectToAction("GeefOrders");
            }
            List<Bestelling> bestellingen = _bestellingRepo.getBestellingenByFilter(klantLogin,productNaam, vanDatum, totDatum, leverAdres,geleverd) ;
            IEnumerable<OnlineBestelling> onlinebestellingen = _onlineBestellingRepository.getOnlineBestellingen(klantLogin);
            BestellingViewModel vm = new BestellingViewModel(bestellingen,onlinebestellingen);
            ApplyLanguage();
            return View(vm);
        }

        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult GeefOnlineOrderDetails(long id,KlantLogin klantLogin)
        {
            OnlineBestelling onlineBestelling= _onlineBestellingRepository.getOnlineBestellingById(id,klantLogin);
            ApplyLanguage();
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;
            OnlineBestellingDetailViewModel vm = new OnlineBestellingDetailViewModel(onlineBestelling, taal);
            return View(vm);
        }
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult RemoveBestelLijn(long id, KlantLogin klantLogin)
        {
            ApplyLanguage();
            OnlineBestelLijn teVerwijderenBestelLijn = _onlineBestelLijnRepository.getOnlineBestellijn(id);
            long onlineBestellingId = teVerwijderenBestelLijn.BestellingId;
            OnlineBestelling onlineBestelling = _onlineBestellingRepository.getOnlineBestellingById(onlineBestellingId,klantLogin);

            List<OnlineBestelLijn> lijstBestellijnen;

            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;
            if (teVerwijderenBestelLijn.HoofdProdBestelLijnId == 0)
            {
                
                lijstBestellijnen = onlineBestelling.OnlineBesltelLijnen.Where(m=>m.HoofdProdBestelLijnId==teVerwijderenBestelLijn.Id).ToList();
                _onlineBestelLijnRepository.verwijderOBestelLijn(teVerwijderenBestelLijn);
                if (lijstBestellijnen.Count() > 0)
                {
                    foreach (OnlineBestelLijn item in lijstBestellijnen)
                    {
                        _onlineBestelLijnRepository.verwijderOBestelLijn(item);
                    }
                }
                _onlineBestelLijnRepository.SaveChanges();
                if (onlineBestelling.OnlineBesltelLijnen.Count < 1 || onlineBestelling.OnlineBesltelLijnen ==null)
                {
                    _onlineBestellingRepository.verwijderOnlineBestelling(onlineBestelling);
                    TempData["message"] = "";
                    _onlineBestelLijnRepository.SaveChanges();
                    if (taal == "en")
                    {
                        TempData["message"] = "Order deleted successfully";
                    }
                    else if (taal == "fr")
                    {
                        TempData["message"] = "Commande supprimée avec succès";

                    }
                    else
                    {
                        TempData["message"] = "Bestelling succesvol verwijderd";
                    }
                    return RedirectToAction("GeefOrders");
                } 
            }
            else
            {

            }
            if (taal == "en")
            {
                TempData["message"] = "OrderLine deleted successfully";
            }
            else if (taal == "fr")
            {
                TempData["message"] = "Ligne de commande supprimée avec succès";

            }
            else
            {
                TempData["message"] = "Bestellings lijn succesvol verwijderdt";
            }
            return RedirectToAction("GeefOnlineOrderDetails", new { id = onlineBestellingId } );
        }
        public void totaalPrijs()
        {
            decimal prijs = 0;
            foreach (OnlineBestelLijn lijn in winkelMandje) 
            {
                prijs = prijs + lijn.Prijs * lijn.Aantal;
            }
            ViewData["TotaalPrijsZonderKorting"] = prijs ;
        }
        public List<Klant> GetLijstLeverklanten(KlantLogin klantLogin)
        {
            List<Klant> leverKlanten = new List<Klant>();
            leverKlanten.Add(klantLogin.Klant);
            foreach (KlantKoppeling klantKoppeling in klantLogin.Klant.KlantKoppelingen)
            {
                leverKlanten.Add(klantKoppeling.GekoppeldKlant);
            }
            return leverKlanten;
        }

        private void ApplyLanguage()
        {
            ViewData["Description"] = _localizer["Description"];
            ViewData["Id"] = _localizer["Id"];
            ViewData["Price"] = _localizer["Price"];
            ViewData["Order"] = _localizer["Order"];
            ViewData["Amount"] = _localizer["Amount"];
            ViewData["Delete"] = _localizer["Delete"];
            ViewData["Total_Amount_Without_Reduction"] = _localizer["Total Amount Without Reduction"];
            ViewData["Finalize_Order"] = _localizer["Finalize Order"];
            ViewData["Euro"] = _localizer["Euro"];
            ViewData["Cart"] = _localizer["Cart"];

            ViewData["MakeOrder"] = _localizer["MakeOrder"];
            ViewData["Reference"] = _localizer["Reference"];
            ViewData["Remark"] = _localizer["Remark"];
            ViewData["Delivery adres"] = _localizer["Delivery adres"];
            ViewData["Put something"] = _localizer["Put something"];
            ViewData["DeliveryType"] = _localizer["DeliveryType"];
            ViewData["Put Order"] = _localizer["Put Order"];

            ViewData["Products"] = _localizer["Products"];
            ViewData["Orders"] = _localizer["Orders"];
            ViewData["Login"] = _localizer["Login"];
            ViewData["Logout"] = _localizer["Logout"];
            ViewData["Invoices"] = _localizer["Invoices"];
            ViewData["Cart"] = _localizer["Cart"];
            ViewData["Settings"] = _localizer["Settings"];

            ViewData["OrderID"] = _localizer["OrderID"];
            ViewData["Date"] = _localizer["Date"];
            ViewData["Delivered To"] = _localizer["Delivered To"];
            ViewData["Total amount"] = _localizer["Total amount"];
            ViewData["Detail"] = _localizer["Detail"];
            ViewData["Orders not yet processed"] = _localizer["Orders not yet processed"];
            ViewData["View Details"] = _localizer["View Details"];
            ViewData["Product"] =_localizer["Product"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["Number"] = _localizer["Number"];
            ViewData["Price per"] = _localizer["Price per"];
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;

            ViewData["Taal"] = taal;
        }
    }
}