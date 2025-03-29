using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.Domain.Interfaces;

namespace GerenciamentoDeGastos.Application.Services
{
    public class LoginService(IUserRepository _userRepository)
    {
        public UserViewModel Login(string Login, string Password)
        {
            var user = _userRepository.Login(Login);

            if (user == null)
                throw new Exception("User not found");

            if (user.Password != Password)
                throw new Exception("Incorrect Password!");

            return new UserViewModel(user);
        }
    }
}
