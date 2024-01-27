using eTickets.BL.InterfaceRepository;
using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.UnitOfWork
{
    public interface IUnitofWork:IAsyncDisposable
    {
        public IGenericRepository<TEntity> Repository<TEntity>()where TEntity:class;

         Task<int> CompleteAsync();


    }
}
