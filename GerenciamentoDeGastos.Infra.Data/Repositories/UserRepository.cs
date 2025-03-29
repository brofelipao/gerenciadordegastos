using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces;
using GerenciamentoDeGastos.Infra.Data.Context;
using GerenciamentoDeGastos.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeGastos.Infra.Data.Repositories
{
    public class UserRepository : BaseGenericRepository<User, ExpensesContext>, IUserRepository
    {
        public UserRepository(ExpensesContext context) : base(context)
        {
        }

        public User? Login(string login)
        {
            return _dbSet.Include(x => x.Person).FirstOrDefault(x => x.Login == login);
        }
    }
}
