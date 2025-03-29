using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.Domain.Entities;
using GerenciamentoDeGastos.Domain.Interfaces;

namespace GerenciamentoDeGastos.Application.Services
{
    public class MovementService(IMovementRepository _movementRepository, IBankAccountRepository _bankAccountRepository)
    {
        public async Task<BankAccountViewModel> GetMovementsByBankAndPerson(int BankAccountId, int PersonId)
        {
            var movements = (await _movementRepository.GetMovementsByBankAndPerson(BankAccountId, PersonId))
                .Select(c => new MovementViewModel(c)).ToList();

            var bank = await _bankAccountRepository.Get(BankAccountId);

            return new BankAccountViewModel(bank, movements);
        }

        public async Task<bool> Save(MovementViewModel movementvm, int PersonId)
        {
            var movement = movementvm.ToMovement();
            movement.PersonId = PersonId;
            // movements recurrents always stays with invoice date null
            movement.IsInvoiced = await Settle(movement) && !movement.IsRecurrent;
            movement.DateInvoiced = movement.IsRecurrent ? null : movement.DateInvoiced;

            if (movement.Id == 0)
                return await _movementRepository.Insert(movement);

            return await _movementRepository.Update(movement);
        }

        public async Task<MovementViewModel> Get(int MovementId)
        {
            return new MovementViewModel(await _movementRepository.Get(MovementId));
        }

        public async Task<bool> Settle(int MovementId, DateTime InvoicedDate)
        {
            var movement = await _movementRepository.Get(MovementId);

            movement.DateInvoiced = InvoicedDate;
            movement.IsInvoiced = await Settle(movement) && !movement.IsRecurrent;
            movement.DateInvoiced = movement.IsRecurrent ? null : movement.DateInvoiced;
            return await _movementRepository.Update(movement);
        }

        public async Task<bool> Settle(Movement movement)
        {
            if (movement.DateInvoiced.HasValue && movement.DateInvoiced <= DateTime.Today && !movement.IsRecurrent && !movement.IsInvoiced)
                return await _bankAccountRepository.Settle(movement.BankAccountId, movement.Type, movement.Amount);

            if (movement.IsRecurrent && movement.DateInvoiced.HasValue && movement.DateInvoiced <= DateTime.Today)
            {
                var alreadyInvoiced = await _movementRepository.IsRecurrentInvoicedThisMonth(movement.Id);

                if (alreadyInvoiced || movement.Id == 0)
                    return false;

                var movementChildren = new Movement
                {
                    PersonId = movement.PersonId,
                    BankAccountId = movement.BankAccountId,
                    Description = movement.Description,
                    Date = DateTime.Today,
                    Amount = movement.Amount,
                    Type = movement.Type,
                    IsRecurrent = false,
                    IsActive = true,
                    IsInvoiced = true,
                    DateInvoiced = DateTime.Today,
                    MovementIdFather = movement.Id,
                };

                if (movement.MovementChildren != null) movement.MovementChildren.Add(movementChildren);
                else movement.MovementChildren = new List<Movement> { movementChildren };

                return await _bankAccountRepository.Settle(movement.BankAccountId, movement.Type, movement.Amount);
            }

            return false;
        }
    }
}
