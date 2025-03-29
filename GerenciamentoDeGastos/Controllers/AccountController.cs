using System.Security.Claims;
using GerenciamentoDeGastos.Application.Services;
using GerenciamentoDeGastos.Application.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GerenciamentoDeGastos.MVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private LoginService _loginService;

        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");   
        }

        [HttpPost]
        public IActionResult Login(string Login, string Password)
        {
            try
            {
                var user = _loginService.Login(Login, Password);

                Authenticate(user);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }

        private void Authenticate(UserViewModel user)
        {
            List<Claim> userClaims = new()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("LastName", user.LastName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("PersonId", user.PersonId.ToString()),
                    new Claim("UserId", user.UserId.ToString()),
                };

            var minhaIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

        }
    }
}
