using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces;
using GerenciamentoDeGastos.Infra.Data.Context;
using GerenciamentoDeGastos.Infra.Data.Repositories.Base;

namespace GerenciamentoDeGastos.Infra.Data.Repositories
{
    public class BankAccountRepository : BaseGenericRepository<BankAccount, ExpensesContext>, IBankAccountRepository
    {
        public BankAccountRepository(ExpensesContext context) : base(context)
        {
        }

        public List<BankAccount> GetByPersonId(int PersonId)
        {
            return _dbSet.Where(c => c.PersonId == PersonId).ToList();
        }
    }
}
