using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoDeGastos.Domain.Interfaces.Base;
using GerenciamentoDeGastos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeGastos.Infra.Data.Repositories.Base
{
    public class BaseGenericRepository<T, C> : IBaseGenericRepository<T> where T : class, new() where C : ExpensesContext
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly ExpensesContext _context;

        public BaseGenericRepository(ExpensesContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IQueryable> Get() => _dbSet;
        public async Task<T> Get(int id) => await _dbSet.FindAsync(id);

        public async Task<bool> Insert(T entitiy)
        {
            await _dbSet.AddAsync(entitiy);

            return await Save();
        }

        public async Task<bool> Insert(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);

            return await Save();
        }

        public async Task<bool> Update(T entitiy)
        {
            _dbSet.Update(entitiy);

            return await Save();
        }

        public async Task<bool> Update(List<T> entities)
        {
            _dbSet.UpdateRange(entities);

            return await Save();
        }

        protected async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Remove(T entity)
        {
            _dbSet.Remove(entity);  // Remove a entitiy do DbSet correspondente
            return await Save();   // Salvar as alterações no banco
        }
    }
}
