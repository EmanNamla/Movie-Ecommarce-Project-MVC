using eTickets.BL.Specifications;
using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.InterfaceRepository
{
    public interface IGenericRepository<T> where T:BaseEntity 
    {
        #region Befoure Specification
       Task< IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task AddAsync(T item);
        void Update(T item);

        void Delete(T item);
        #endregion

        #region After Specification
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        #endregion
    }
}
