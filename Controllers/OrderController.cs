using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IEnumerable<OnlineBestelLijn> winkelMandje;
        private readonly IOnlineBestelLijnRepository _onlineBestelLijnRepository;
        private readonly IKlantLoginRepository _klantLoginRepository;
        private string idKlantLogin;
        private KlantLogin klantLogin;
        public OrderController(IOnlineBestelLijnRepository onlineBestelLijnRepository
            , IKlantLoginRepository klantLoginRepo)
        {
            this._onlineBestelLijnRepository = onlineBestelLijnRepository;
            this._klantLoginRepository = klantLoginRepo;

        }
        private void HaalKlantOp()
        {

            idKlantLogin = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            klantLogin = _klantLoginRepository.getLoginByLoginID(Convert.ToInt64(idKlantLogin));
        }

        public IActionResult Index()
        {
            HaalKlantOp();
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin);
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
                lijstBestellijnen = _onlineBestelLijnRepository.getOnlineBestelLijnen(klantLogin)
                    .Where(p => p.HoofdProdBestelLijnId == teVerwijderenBestelLijn.Id);
                _onlineBestelLijnRepository.verwijderOBestelLijn(teVerwijderenBestelLijn);
                foreach (OnlineBestelLijn item in lijstBestellijnen)
                {
                    _onlineBestelLijnRepository.verwijderOBestelLijn(item);
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

        public IActionResult MaakOrder()
        {


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