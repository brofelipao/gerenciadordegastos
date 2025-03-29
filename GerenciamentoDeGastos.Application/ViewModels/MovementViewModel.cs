using GerenciamentoDeGastos.Domain.Entities;

namespace GerenciamentoDeGastos.Application.ViewModels
{
    public class MovementViewModel
    {
        public MovementViewModel()
        {
            
        }
        public MovementViewModel(Movement m) 
        {
            Id = m.Id;
            BankAccountId = m.BankAccountId;
            PersonId = m.PersonId;
            Description = m.Description;
            Date = m.Date;
            Amount = m.Amount;
            Type = m.Type;
            TypeText = m.Type == 'R' ? "Receipt" : "Cost";
            IsRecurrent = m.IsRecurrent;
            DateInvoiced = m.DateInvoiced;
            IsActive = m.IsActive;
            IsInvoiced = m.IsInvoiced;
        }

        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int PersonId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public decimal Amount { get; set; }
        public char Type { get; set; }
        public string TypeText { get; set; }
        public bool IsRecurrent { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool IsInvoiced { get; set; } = false;
        public DateTime? DateInvoiced { get; set; }

        public Movement ToMovement()
        {
            return new Movement
            {
                Id = Id,
                BankAccountId = BankAccountId,
                Description = Description,
                Date = Date,
                Amount = Amount,
                Type = Type,
                IsRecurrent = IsRecurrent,
                DateInvoiced = DateInvoiced,
                PersonId = PersonId,
                IsActive = IsActive,
                IsInvoiced = IsInvoiced
            };
        }
    }
}
