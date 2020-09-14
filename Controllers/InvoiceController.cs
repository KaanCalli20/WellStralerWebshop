using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IFactuurLijnRepository _factuurLijnRepository;
        private readonly IFactuurRepository _factuurRepository;
        public InvoiceController(IFactuurLijnRepository factuurLijnRepo,IFactuurRepository factuurRepository)
        {
            this._factuurLijnRepository = factuurLijnRepo;
            this._factuurRepository = factuurRepository;
        }
        public IActionResult Index()
        {
            List<FactuurLijn> factuurlijn = _factuurLijnRepository.getFactuurLijnen();
            List<Factuur> facturen = _factuurRepository.getFacturen();


            return View();
        }
    }
}