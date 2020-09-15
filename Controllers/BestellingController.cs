using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{
    public class BestellingController : Controller
    {
        private readonly IBestelLijnRepository _bestelLijnRepo;
        private readonly IBestellingRepository _bestellingRepo;
        public BestellingController(IBestelLijnRepository bestellijnRepo, IBestellingRepository bestellingRepo)
        {
            this._bestelLijnRepo = bestellijnRepo;
            this._bestellingRepo = bestellingRepo;
        }
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(KlantLogin klantLogin)
        {
            List<Bestelling> bestellingen = _bestellingRepo.getBestellingen(klantLogin);
            BestellingViewModel vm = new BestellingViewModel(bestellingen);
           

            return View(vm);
        }

        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Details(int id, KlantLogin klantLogin)
        {
            Bestelling bestelling =_bestellingRepo.getBestellingById(id,klantLogin);
            BestellingDetailViewModel vm = new BestellingDetailViewModel(bestelling);
            return View(vm);
        }
    }
}