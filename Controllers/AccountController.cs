using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKlantLoginRepository _loginRepo;
        private readonly IKlantRepository _klantRepo;

        public AccountController(IKlantLoginRepository loginRepo, IKlantRepository klantRepo)
        {
            this._loginRepo = loginRepo;
            this._klantRepo = klantRepo;
        }

        public IActionResult Index()
        {
            //Klant kl = _loginRepo.getLogins().First().klant;
            return View("LogIn");
        }

        public async Task<IActionResult> LogInAsync(string gebruikersnaam, string wachtwoord)
        {

             string lg_psw_encrypted = Encryption.Encrypt(wachtwoord, "dst.be rules");
             KlantLogin kl = _loginRepo.getLoginByGebruikersNaam(gebruikersnaam);

             if (lg_psw_encrypted.Equals(kl.Paswoord))
             {
                 TempData["Message"] = "U bent succesvol ingelogd";
             }
             else
             {
                 TempData["LoginError"] = "Gebruikersnaam en Wachtwoord komen niet overeen";
                 return View();
             }
             
            //Verificatie komt hier
            KlantLogin klant = _loginRepo.getLoginByGebruikersNaam(gebruikersnaam);

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name,klant.Voornaam+" "+ klant.Naam  ),
                    new Claim(ClaimTypes.NameIdentifier,klant.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Administrator"),
            };

            var identityy = new ClaimsIdentity(claims, "rechten");

            var userPrincipal = new ClaimsPrincipal(new[] { identityy });

            await HttpContext.SignInAsync(userPrincipal);

            

            return RedirectToAction("Index", "Product");


            
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Product");
        }
    }
}