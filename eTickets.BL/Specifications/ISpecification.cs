using eTickets.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.BL.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Ciratria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderByAsyning { get; set; }

        public Expression<Func<T, object>> OrderByDesynding {get; set; }



    }
}
