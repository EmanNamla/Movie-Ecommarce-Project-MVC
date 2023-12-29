﻿using eTickets.BL.InterfaceRepository;
using eTickets.BL.Specifications;
using eTickets.DAL.Contexts;
using eTickets.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region Befour Specification
        public async Task<IReadOnlyList<T>> GetAllAsync()

        => await dbContext.Set<T>().ToListAsync();


        public async Task<T> GetByIdAsync(int id)
         => await dbContext.Set<T>().FindAsync(id);

        public async Task AddAsync(T item)
        { await dbContext.Set<T>().AddAsync(item); }

        public void Delete(T item)
        {
            dbContext.Set<T>().Remove(item);
        }

        public void Update(T item)
        {
            dbContext.Set<T>().Update(item);
        }
        #endregion

        #region After Specification

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), spec);
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        #endregion
    }
}
