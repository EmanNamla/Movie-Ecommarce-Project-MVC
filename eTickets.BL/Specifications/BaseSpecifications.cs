using eTickets.DAL.Models;
using System.Linq.Expressions;

namespace eTickets.BL.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Ciratria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsyning { get ; set ; }
        public Expression<Func<T, object>> OrderByDesynding { get; set ; }

        public BaseSpecifications()
        {

        }

        public void OrderByAsyn(Expression<Func<T, object>> OrderByAsyningExpression)
        {
            OrderByAsyning=OrderByAsyningExpression;
        }

        public void OrderByDsyn(Expression<Func<T, object>> OrderByDsyningExpression)
        {
            OrderByDesynding = OrderByDsyningExpression;
        }

        public BaseSpecifications(Expression<Func<T, bool>> ciratria)
        {
            Ciratria = ciratria;
        }
    }
}
