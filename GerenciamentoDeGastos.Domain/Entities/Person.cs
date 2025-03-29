using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeGastos.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CpjCnpj { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Movement>? Movements { get; set; }
        public virtual ICollection<BankAccount>? BankAccounts { get; set; }
    }
}
