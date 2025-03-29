
using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces.Base;

namespace GerenciamentoDeGastos.Domain.Interfaces
{
    public interface IBankAccountRepository : IBaseGenericRepository<BankAccount>
    {
        public List<BankAccount> GetByPersonId(int PersonId);
        Task<bool> Settle(int BankAccountId, char type, decimal Value);
    }
}
