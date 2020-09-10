using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    public class OrderController : Controller
    {
        private IEnumerable<OnlineBestelLijn> winkelMandje;
        private IOnlineBestelLijnRepository _onlineBestelLijnRepository;

        public OrderController(IOnlineBestelLijnRepository onlineBestelLijnRepository)
        {
            this._onlineBestelLijnRepository = onlineBestelLijnRepository;
            winkelMandje = this._onlineBestelLijnRepository.getOnlineBestelLijnen();
        }

        public IActionResult Index()
        {
            
            
            return View(winkelMandje);
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            /*
            OnlineBestelLijn ol = winkelMandje.Where(ob => ob.Id.Equals(id)).First();
            ICollection<OnlineBestelLijn> temp = new List<OnlineBestelLijn>((ICollection<OnlineBestelLijn>)winkelMandje);
            temp.Remove(ol);
            
            winkelMandje = temp;
            return RedirectToAction(nameof(Index));*/
            return View();
        }
    }
}