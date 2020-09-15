using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

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
        public IActionResult Index()
        {
            List<BestelLijn> bestelLijnen = _bestelLijnRepo.getBestelLijnen();
            List<Bestelling> bestellingen = _bestellingRepo.getBestellingen();


            return View();
        }
    }
}