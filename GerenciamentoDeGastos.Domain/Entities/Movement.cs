using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeGastos.Domain.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int BankAccountId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public char Type { get; set; }
        public bool IsRecurrent { get; set; }
        public bool IsActive { get; set; }
        public bool IsInvoiced { get; set; }
        public DateTime? DateInvoiced { get; set; }

        // Movements recurrents will create simple movements
        public int? MovementIdFather { get; set; }
        public virtual Person? Person { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        public virtual Movement? MovementFather { get; set; }
        public virtual ICollection<Movement>? MovementChildren { get; set; }
    }
}
