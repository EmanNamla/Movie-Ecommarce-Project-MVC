using eTickets.BL.InterfaceRepository;
using eTickets.BL.Repository;
using eTickets.DAL.Contexts;
using eTickets.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.UnitOfWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext dbContext;
        private Hashtable _Repository;

        public UnitofWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            _Repository=new Hashtable();
        }
        public async Task<int> CompleteAsync()
           => await dbContext.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
        {
          await dbContext.DisposeAsync(); 
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;

            if (!_Repository.Contains(type))
            {
                var Repository=new GenericRepository<TEntity>(dbContext);
                _Repository.Add(type, Repository);
            }
            return _Repository[type] as IGenericRepository<TEntity>;
        }
    }
}
