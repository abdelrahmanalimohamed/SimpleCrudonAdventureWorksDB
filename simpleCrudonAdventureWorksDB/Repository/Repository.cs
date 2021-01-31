using Microsoft.EntityFrameworkCore;
using simpleCrudonAdventureWorksDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simpleCrudonAdventureWorksDB.Repository
{
    public class Repository<TDbContext> : IRepository where TDbContext : DbContext
    {

        protected TDbContext DbContext;

        public Repository(TDbContext context)
        {
            DbContext = context;
        }
        public async Task CreateAsync<T>(T entity) where T : class
        {
            this.DbContext.Set<T>().Add(entity);
            _ = await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            this.DbContext.Set<T>().Remove(entity);
            _ = await this.DbContext.SaveChangesAsync();
        }

        public async Task<List<T>> SelectAll<T>() where T : class
        {
            return await this.DbContext.Set<T>().ToListAsync();
        }

        public async Task<T> SelectById<T>(long id) where T : class
        {
            return await this.DbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            this.DbContext.Set<T>().Update(entity);

            _ = await this.DbContext.SaveChangesAsync();
        }
    }
}
