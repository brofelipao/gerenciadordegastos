using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Application.ViewModels;
using GerenciamentoDeGastos.Domain.Interfaces;

namespace GerenciamentoDeGastos.Application.Services
{
    public class BankAccountService(IBankAccountRepository _bankAccountRepository)
    {
        public List<BankAccountViewModel> GetBankAccountsByPersonId(int PersonId)
        {
            return _bankAccountRepository.GetByPersonId(PersonId)
                .Select(c => new BankAccountViewModel(c))
                .ToList();
        }

        public async Task<BankAccountViewModel> Get(int id)
        {
            return new BankAccountViewModel(await _bankAccountRepository.Get(id));
        }

        public async Task<bool> Save(BankAccountViewModel bavm, int PersonId)
        {
            var ba = bavm.ToBankAccount();
            ba.PersonId = PersonId;

            if (ba.Id == 0)
                return await _bankAccountRepository.Insert(ba);

            return await _bankAccountRepository.Update(ba);
        }
    }
}
