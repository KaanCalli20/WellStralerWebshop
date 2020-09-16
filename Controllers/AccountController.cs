using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

                TempData["Message"] = "You have successfully logged in";

            }
            else
            {
                TempData["error"] = "Username and Password do not match";
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

            switch (kl.Taal)
            {
                case 1:
                    taal = "nl";
                    break;
                case 2:
                    taal = "en";
                    break;
                case 3:
                    taal = "fr";
                    break;
            }
            veranderTaal(taal);

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
            string taal = "";
            Klant klant = klantLogin.Klant;
            switch (klantLogin.Taal)
            {
                case 1:
                    taal = "nl";
                    break;
                case 2:
                    taal = "en";
                    break;
                case 3:
                    taal = "fr";
                    break;
            }
            AccountViewModel vm = new AccountViewModel(klant, taal);
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
        public IActionResult BeheerAccount(KlantLogin klantLogin, string taal, string? oudWachtwoord, string? nieuweWachtwoord, string? herhaalWachtwoord)
        {

            if (oudWachtwoord != null)
            {
                try
                {
                    string nieuweWachtwoordGehashed = "";
                    string oudWachtwoordGehashed = Encryption.Encrypt(oudWachtwoord, "dst.be rules");
                    if (!klantLogin.Paswoord.Equals(oudWachtwoordGehashed))
                    {
                        throw new ArgumentException("Uw oud wachtwoord komt niet overeen");
                    }
                    if (!nieuweWachtwoord.Equals(herhaalWachtwoord))
                    {
                        throw new ArgumentException("Uw nieuwe wachtwoord komt niet over met de herhaal wachtwoord");
                    }
                    nieuweWachtwoordGehashed = Encryption.Encrypt(nieuweWachtwoord, "dst.be rules");
                    klantLogin.Paswoord = nieuweWachtwoordGehashed;
                    _loginRepo.SaveChanges();
                    TempData["message"] = "Gegevens succesvol gewijzigd";

                }
                catch (ArgumentException ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            if (taal != null)
            {
                byte taalIndex = 0;
                try
                {
                    switch (taal)
                    {
                        case "nl":
                            taalIndex = 1;
                            break;
                        case "en":
                            taalIndex = 2;
                            break;
                        case "fr":
                            taalIndex = 3;
                            break;
                    }
                    klantLogin.Taal = taalIndex;
                    _loginRepo.SaveChanges();
                    veranderTaal(taal);
                    TempData["message"] = "Gegevens succesvol gewijzigd";
                }
                catch (ArgumentException ex)
                {
                    TempData["error"] = ex.Message;
                }
            }

            Klant klant = klantLogin.Klant;

            AccountViewModel vm = new AccountViewModel(klant, taal);
            ApplyLanguage();
            return RedirectToAction("BeheerAccount");
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
            ViewData["Information"] = _localizer["Information"];
            ViewData["Company"] = _localizer["Company"];
            ViewData["Email"] = _localizer["Email"];
            ViewData["Address"] = _localizer["Address"];
            ViewData["Change Password"] = _localizer["Change Password"];
            ViewData["Old password"] = _localizer["Old password"];
            ViewData["New password"] = _localizer["New password"];
            ViewData["Repeat password"] = _localizer["Repeat password"];
            ViewData["Language"] = _localizer["Language"];
            ViewData["Change language"] = _localizer["Change language"];
            ViewData["Delivery customers"] = _localizer["Delivery customers"];
            ViewData["Name"] = _localizer["Name"];
            ViewData["City"] = _localizer["City"];
            ViewData["Change"] = _localizer["Change"];


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

        private void veranderTaal(string culture)
        {
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(365) }
               );
        }
    }
}