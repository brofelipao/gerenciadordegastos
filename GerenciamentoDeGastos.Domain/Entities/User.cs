using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeGastos.Domain.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool IsActive { get; set; }
        public virtual Person? Person { get; set; }
    }
}
