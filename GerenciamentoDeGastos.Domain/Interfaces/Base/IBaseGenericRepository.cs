using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeGastos.Domain.Interfaces.Base
{
    public interface IBaseGenericRepository<T>
    {
        Task<IQueryable> Get();
        Task<T> Get(int id);

        Task<bool> Insert(T entitiy);

        Task<bool> Insert(List<T> entities);

        Task<bool> Update(T entitiy);

        Task<bool> Update(List<T> entities);

        Task<bool> Remove(T entity);
    }
}
