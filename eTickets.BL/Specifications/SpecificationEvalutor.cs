using eTickets.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var Query = inputQuery;
            if(spec.Ciratria is not null)
            {
                Query=Query.Where(spec.Ciratria);
            }

            Query=spec.Includes.Aggregate(Query,(currentQuery,IncludeExpression)=>
            currentQuery.Include(IncludeExpression));

            return Query;
        }

    }
}
