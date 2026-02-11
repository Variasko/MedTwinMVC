using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MedTwinMVC.DatabaseContext;
using MedTwinMVC.Models.DatabaseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MedTwinMVC.Controllers
{
    public class AuthorizationController : Controller
    {

        private DigitalTwinPatientDbtestContext _db = new DigitalTwinPatientDbtestContext();

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model, 
            string returnUrl = null
        )
        {
            if (!ModelState.IsValid)
                return null;

            var patient = _db.Patients
                .Where(p => p.Email == model.Username
                        && p.Password == model.Password)
                .FirstOrDefault();

            if (patient == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, patient.Email),
                new Claim(ClaimTypes.NameIdentifier, patient.Id.ToString()),
                new Claim(ClaimTypes.Role, "Пациент")
            };

            var claimsIdentity = new ClaimsIdentity(
                            claims, 
                            CookieAuthenticationDefaults.AuthenticationScheme
                        );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }



            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authorization");
        }
    }
}
