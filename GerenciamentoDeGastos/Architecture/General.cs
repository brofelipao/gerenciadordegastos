using System.Security.Claims;
using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.Domain.Entities;

namespace GerenciamentoDeGastos.MVC.Architecture
{
    public static class General
    {
        public static UserViewModel ToUserViewModel(this ClaimsIdentity identity)
        {
            return new UserViewModel
            {
                Name = identity.FindFirst(ClaimTypes.Name)?.Value,
                LastName = identity.FindFirst("LastName")?.Value,
                PersonId = Convert.ToInt32(identity.FindFirst("PersonId")?.Value),
                UserId = Convert.ToInt32(identity.FindFirst("UserId")?.Value),
            };
        }

    }
}
