using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces.Base;

namespace GerenciamentoDeGastos.Domain.Interfaces
{
    public interface IMovementRepository : IBaseGenericRepository<Movement>
    {
        Task<IQueryable<Movement>> GetMovementsByBankAndPerson(int BankAccountId, int PersonId);
        Task<bool> IsRecurrentInvoicedThisMonth(int MovementId);
        Task<Movement> InsertChildren(Movement movementFather);
    }
}
