using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{[Authorize]
    public class InvoiceController : Controller
    {
        private readonly IFactuurLijnRepository _factuurLijnRepository;
        private readonly IFactuurRepository _factuurRepository;
        private readonly IStringLocalizer<InvoiceController> _localizer;

        public InvoiceController(IFactuurLijnRepository factuurLijnRepo,IFactuurRepository factuurRepository, IStringLocalizer<InvoiceController> localizer)
        {
            this._factuurLijnRepository = factuurLijnRepo;
            this._factuurRepository = factuurRepository;
            this._localizer = localizer;
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

            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;

            ViewData["Taal"] = taal;
        }
    }
}