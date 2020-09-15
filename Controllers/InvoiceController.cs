using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{[Authorize]
    public class InvoiceController : Controller
    {
        private readonly IFactuurLijnRepository _factuurLijnRepository;
        private readonly IFactuurRepository _factuurRepository;
        public InvoiceController(IFactuurLijnRepository factuurLijnRepo,IFactuurRepository factuurRepository)
        {
            this._factuurLijnRepository = factuurLijnRepo;
            this._factuurRepository = factuurRepository;
        }
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(KlantLogin klantLogin)
        {
            //List<FactuurLijn> factuurlijn = _factuurLijnRepository.getFactuurLijnen();
            List<Factuur> facturen = _factuurRepository.getFacturen(klantLogin);
            FactuurViewModel vm = new FactuurViewModel(facturen);
            decimal prijs = 0;
            
            return View(vm);
        }

        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Index(string? productNaam, long? factuurNr, DateTime? vanDatum,DateTime? totDatum
            , long? zendNotaNr, string? serienummer, KlantLogin klantLogin)
        {
            //List<FactuurLijn> factuurlijn = _factuurLijnRepository.getFactuurLijnen();
            List<Factuur> facturen = _factuurRepository.getGefilterdeFactuur(productNaam, factuurNr, vanDatum, totDatum, zendNotaNr, serienummer,klantLogin);
            FactuurViewModel vm = new FactuurViewModel(facturen);

            vm.ProductNaam = productNaam;
            vm.FactuurNr = factuurNr;
            vm.VanDatum =vanDatum;
            vm.TotDatum = totDatum;
            vm.ZendNotaNr = zendNotaNr;
            vm.Serienummer = serienummer;
            return View(vm);
        }
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Details(int id, KlantLogin klantLogin)
        {
            Factuur factuur =_factuurRepository.getFactuur(id, klantLogin);

            FactuurDetailViewModel vm = new FactuurDetailViewModel(factuur);
            return View(vm);
        }
    }
}