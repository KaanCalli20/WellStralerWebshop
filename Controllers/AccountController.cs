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
            //Klant kl = _loginRepo.getLogins().First().klant;
            return View("LogIn");
        }

        public IActionResult LogIn(string gebruikersnaam, string wachtwoord)
        {
            //Verificatie komt hier
            
            string lg_psw_encrypted = Encryption.Encrypt(wachtwoord, "dst.be rules");
            KlantLogin kl = _loginRepo.getLoginByGebruikersNaam(gebruikersnaam);

            if (lg_psw_encrypted.Equals(kl.Paswoord))
            {
                TempData["LoginMessage"] = "U ben ingelogd";
                return View("Index");
            }
            else
            {
                TempData["LoginError"] = "Gebruikersnaam en Wachtwoord komen niet overeen";
                return View();
            }


            
            
        }
    }
}