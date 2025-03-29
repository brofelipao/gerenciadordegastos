using System.Security.Claims;
using GerenciamentoDeGastos.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoDeGastos.MVC.Architecture
{
    public class GenericController : Controller
    {
        private Lazy<UserViewModel>? _UserViewModel;
        protected UserViewModel UserViewModel
        {
            get => (_UserViewModel ??= new Lazy<UserViewModel>(() => (User.Identity as ClaimsIdentity).ToUserViewModel())).Value;
        }
    }
}
