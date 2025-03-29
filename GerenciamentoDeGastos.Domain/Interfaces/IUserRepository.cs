using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces.Base;

namespace GerenciamentoDeGastos.Domain.Interfaces
{
    public interface IUserRepository : IBaseGenericRepository<User>
    {
        User? Login(string login);
    }
}
