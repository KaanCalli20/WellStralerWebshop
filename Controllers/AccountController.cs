using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKlantLoginRepository _loginRepo;
        
        public AccountController(IKlantLoginRepository loginRepo)
        {
            this._loginRepo = loginRepo;
        }

        public IActionResult Index()
        {
            Klant kl = _loginRepo.getLogins().First().klant;
            return RedirectToAction(nameof(LogIn));
        }

        public IActionResult LogIn(string gebruikersnaam, string wachtwoord)
        {
            //Verificatie komt hier

            return View();
            
        }
    }
}