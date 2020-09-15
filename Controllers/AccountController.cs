using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using WellStralerWebshop.Filters;
using WellStralerWebshop.Models.Domain;
using WellStralerWebshop.Models.ViewModels;

namespace WellStralerWebshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKlantLoginRepository _loginRepo;
        private readonly IKlantRepository _klantRepo;
        private readonly IStringLocalizer<AccountController> _localizer;
        public AccountController(IKlantLoginRepository loginRepo, IKlantRepository klantRepo, IStringLocalizer<AccountController> localizer)
        {
            this._loginRepo = loginRepo;
            this._klantRepo = klantRepo;
            this._localizer = localizer;
        }

        public IActionResult Index()
        {
            ApplyLanguage();
            //Klant kl = _loginRepo.getLogins().First().klant;
            return View("LogIn");
        }

        public async Task<IActionResult> LogInAsync(string gebruikersnaam, string wachtwoord)
        {
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;
            string lg_psw_encrypted = Encryption.Encrypt(wachtwoord, "dst.be rules");
             KlantLogin kl = _loginRepo.getLoginByGebruikersNaam(gebruikersnaam);
            ApplyLanguage();
            
            if (lg_psw_encrypted.Equals(kl.Paswoord))
             {
                 TempData["Message"] = "U bent succesvol ingelogd";
                if (taal == "en")
                {
                    TempData["Message"] = "U bent succesvol ingelogd";
                }
                else if (taal == "fr")
                {
                    TempData["Message"] = "U bent succesvol ingelogd";
                }
                else
                {
                    TempData["Message"] = "U bent succesvol ingelogd";
                }
            }
             else
             {

                 TempData["LoginError"] = "Username and Password do not match";
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
        [Authorize]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult BeheerAccount(KlantLogin klantLogin)
        {
            Klant klant = klantLogin.Klant;

            AccountViewModel vm = new AccountViewModel(klant);
            List<string> talen = new List<string>();
            talen.Add("nl");
            talen.Add("en");
            talen.Add("fr");
            ViewData["talen"] = new SelectList(talen);
            ApplyLanguage();
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult BeheerAccount(KlantLogin klantLogin,string taal)
        {
            Klant klant = klantLogin.Klant;

            AccountViewModel vm = new AccountViewModel(klant);
            ApplyLanguage();
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

            

            ViewData["Products"] = _localizer["Products"];
            ViewData["Orders"] = _localizer["Orders"];
            ViewData["Login"] = _localizer["Login"];
            ViewData["Logout"] = _localizer["Logout"];
            ViewData["Invoices"] = _localizer["Invoices"];
            ViewData["Cart"] = _localizer["Cart"];
            var request = HttpContext.Features.Get<IRequestCultureFeature>();
            string taal = request.RequestCulture.Culture.Name;

            ViewData["Taal"] = taal;
        }
    }
}