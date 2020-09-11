using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        private string _idKlantLogin;
        private KlantLogin _klantLogin;
        public OrderController(IOnlineBestelLijnRepository onlineBestelLijnRepository
            , IKlantLoginRepository klantLoginRepo,ITransportRepository transportRepository)
        {
            this._onlineBestelLijnRepository = onlineBestelLijnRepository;
            this._klantLoginRepository = klantLoginRepo;
            this._transportRepository = transportRepository;
        }
        private void HaalKlantOp()
        {

            _idKlantLogin = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _klantLogin = _klantLoginRepository.getLoginByLoginID(Convert.ToInt64(_idKlantLogin));
        }

        public IActionResult Index()
        {
            HaalKlantOp();
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(_klantLogin);
            totaalPrijs();
            return View(winkelMandje);
        }


        public IActionResult Remove(long id)
        {
            HaalKlantOp();
            OnlineBestelLijn teVerwijderenBestelLijn = _onlineBestelLijnRepository.getOnlineBestellijn(id);
            IEnumerable<OnlineBestelLijn> lijstBestellijnen;
            if (teVerwijderenBestelLijn.HoofdProdBestelLijnId == 0)
            {
                lijstBestellijnen = _onlineBestelLijnRepository.getOnlineBestelLijnen(_klantLogin)
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
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(_klantLogin);
            totaalPrijs();
            return View("Index", winkelMandje);
        }

        [HttpGet]
        public IActionResult MaakOrder(string id)
        {
            HaalKlantOp();

            List<Transport> lijstTransportTypes = _transportRepository.getAllTransport();
            List<Klant> leverKlanten = new List<Klant>();
            leverKlanten.Add(this._klantLogin.Klant);
            foreach (KlantKoppeling klantKoppeling in this._klantLogin.Klant.KlantKoppelingen)
            {
                leverKlanten.Add(klantKoppeling.GekoppeldKlant);
            }


            MaakOrderViewModel vm = new MaakOrderViewModel(leverKlanten, leverKlanten.ElementAt(0),"","",lijstTransportTypes,lijstTransportTypes.ElementAt(0));
           
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(this._klantLogin);
           
            
             return View(vm);
        }

        [HttpPost]
        public IActionResult MaakOrder(string leverKlant, string referentie, string opmerking, string transportType)
        {
           // OnlineBestelling bestelling = new OnlineBestelling(klantLogin.Klant,)
            return null;
        }
        public void totaalPrijs()
        {
            decimal prijs = 0;
            foreach (OnlineBestelLijn lijn in winkelMandje) 
            {
                prijs = prijs + lijn.Prijs * lijn.Aantal;
            }
            TempData["TotaalPrijsZonderKorting"] = prijs ;
        }
    }
}