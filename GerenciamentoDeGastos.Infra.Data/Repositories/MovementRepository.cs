using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces;
using GerenciamentoDeGastos.Infra.Data.Context;
using GerenciamentoDeGastos.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeGastos.Infra.Data.Repositories
{
    public class MovementRepository : BaseGenericRepository<Movement, ExpensesContext>, IMovementRepository
    {
        public MovementRepository(ExpensesContext context) : base(context)
        {
        }

        public async Task<IQueryable<Movement>> GetMovementsByBankAndPerson(int BankAccountId, int PersonId)
        {
            return _dbSet.Where(c => c.PersonId == PersonId && c.BankAccountId == BankAccountId && c.IsActive);
        }

        public async Task<bool> IsRecurrentInvoicedThisMonth(int MovementId)
        {
            return _dbSet.Any(c => c.MovementIdFather == MovementId && c.DateInvoiced.HasValue && c.DateInvoiced.Value.Month == DateTime.Today.Month);
        }

        public async Task<Movement> InsertChildren(Movement movementFather)
        {
            var movementChildren = new Movement
            {
                PersonId = movementFather.PersonId,
                BankAccountId = movementFather.BankAccountId,
                Description = movementFather.Description,
                Date = DateTime.Today,
                Amount = movementFather.Amount,
                Type = movementFather.Type,
                IsRecurrent = false,
                IsActive = true,
                IsInvoiced = true,
                DateInvoiced = DateTime.Today,
                MovementIdFather = movementFather.Id,
            };

            await Insert(movementChildren);

            return movementChildren;
        }
    }
}
