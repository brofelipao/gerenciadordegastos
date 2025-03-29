using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Entities;

namespace GerenciamentoDeGastos.Application.ViewModels
{
    public class BankAccountViewModel
    {
        public BankAccountViewModel() { }
        public BankAccountViewModel(BankAccount b)
        {
            Id = b.Id;
            BankName = b.BankName;
            Agency = b.Agency;
            Account = b.Account;
            Balance = b.Balance;
        }

        public int Id { get; set; }
        public string BankName { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }

        public BankAccount ToBankAccount()
        {
            return new BankAccount
            {
                Id = this.Id,
                BankName = this.BankName,
                Account = this.Account,
                Agency = this.Agency,
                Balance = this.Balance
            };
        }
    }
}
