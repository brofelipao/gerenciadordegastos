using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Entities;

namespace GerenciamentoDeGastos.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel() { }
        public UserViewModel(User u)
        {
            Name = u.Person.Name;
            LastName = u.Person.LastName;
            Role = "ADMIN";
            PersonId = u.Person.Id;
            UserId = u.Id;
        }
        public int PersonId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
