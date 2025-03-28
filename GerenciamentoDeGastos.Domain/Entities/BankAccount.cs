using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeGastos.Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string BankName { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Movement>? Movements { get; set; }
        public virtual Person? Person { get; set; }
    }
}
