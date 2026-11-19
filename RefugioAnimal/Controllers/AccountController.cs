using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RefugioAnimal.Web.Models.Account;
using RefugioAnimal.Web.Models.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RefugioAnimal.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View(new RegistroViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registro(
            RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new ApplicationUser
            {
                UserName = modelo.Correo,
                Email = modelo.Correo
            };

            var resultado = await UserManager.CreateAsync(
                usuario,
                modelo.Password);

            if (resultado.Succeeded)
            {
                await IniciarSesionAsync(usuario, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(modelo);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(
            LoginViewModel modelo,
            string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = await UserManager.FindAsync(
                modelo.Correo,
                modelo.Password);

            if (usuario == null)
            {
                ModelState.AddModelError(
                    "",
                    "Correo o contraseña inválidos.");

                return View(modelo);
            }

            await IniciarSesionAsync(
                usuario,
                modelo.Recordarme);

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        private async Task IniciarSesionAsync(
            ApplicationUser usuario,
            bool isPersistent)
        {
            AuthenticationManager.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie);

            var identidad =
                await usuario.GenerateUserIdentityAsync(UserManager);

            AuthenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                },
                identidad);
        }
    }
}