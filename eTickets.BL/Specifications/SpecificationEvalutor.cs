using eTickets.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eTickets.BL.Specifications
{
    public static class SpecificationEvalutor<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var Query = inputQuery;
            if(spec.Ciratria is not null)
            {
                Query=Query.Where(spec.Ciratria);
            }
            if(spec.OrderByAsyning is not null)
            {
                Query=Query.OrderBy(spec.OrderByAsyning);
            }
            if (spec.OrderByDesynding is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesynding);
            }


            Query =spec.Includes.Aggregate(Query,(currentQuery,IncludeExpression)=>
            currentQuery.Include(IncludeExpression));

            return Query;
        }

    }
}
